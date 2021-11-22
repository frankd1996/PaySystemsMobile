using FluentValidation;
using PaySystemsMobile.Helpers;
using PaySystemsMobile.Models;
using PaySystemsMobile.Services.Authentication;
using PaySystemsMobile.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace PaySystemsMobile.ViewModels
{
    public class RegistroViewModel : BaseValidationViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand NavegaraPresentacionCommand { get; set; }
        public ICommand Login { get; set; }        
        public IAuthentication Iauthentication { get; set; }
        public ICommand TerminosCommand { get; set; }
        public ICommand PoliticasCommand { get; set; }
        public ICommand NavegaraIniciarsesionCommand { get; set; }
        public ICommand CrearCuentaCommand { get; set; }
        public UsuarioModel ValidationContext = new UsuarioModel();
        private string _nombreCompleto;
        private string _email;
        private string _contraseña;
        private string _telefono;
        private string _nombreUsuario;

        public string NombreCompleto
        {
            get => _nombreCompleto;
            set
            {
                _nombreCompleto = value;
                ValidationContext.NombreCompleto = value;
                Validate(ValidationContext);
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                ValidationContext.Email = value;
                Validate(ValidationContext);
                OnPropertyChanged();
            }
        }

        public string Contraseña
        {
            get { return _contraseña; }
            set 
            { 
                _contraseña = value;
                ValidationContext.Contraseña = value;
                Validate(ValidationContext);
                OnPropertyChanged();
            }
        }

        public string Telefono
        {
            get { return _telefono; }
            set
            {
                _telefono = value;
                ValidationContext.Telefono = value;
                Validate(ValidationContext);
                OnPropertyChanged();
            }
        }

        public string NombreUsuario
        {
            get { return _nombreUsuario; }
            set
            {
                _nombreUsuario = value;
                ValidationContext.NombreUsuario = value;
                Validate(ValidationContext);
                OnPropertyChanged();
            }
        }

        //FVModelValidator hereda la interfaz IValidator 
        public RegistroViewModel(INavigation navigation, IValidator validator) : base(validator)
        {
            Navigation = navigation;
            NavegaraPresentacionCommand = new Command(async() => await Navigation.PopAsync());
            Login = new Command(() => LoginGoogle_Clicked());
            TerminosCommand = new Command(async() => await MostrarTerminos());
            PoliticasCommand = new Command(async() => await MostrarPoliticas());
            NavegaraIniciarsesionCommand = new Command(async() => await Navigation.PushAsync(new LoginView()));
            CrearCuentaCommand = new Command(async () => await CrearCuenta());            

            //Hacemos la inyección de dependencias en el constructor
            //de forma predeterminada se establece en DependencyFetchTarget.GlobalInstance (singleton)
            // se uede configurar en DependencyFetchTarget.NewInstance (transient). Si usamos NewInstance 
            //Tenemos que usar bloques using para borrar las instancias (ver coleccionde de edge)
            Iauthentication = DependencyService.Get<IAuthentication>(DependencyFetchTarget.GlobalInstance);
        }        
        
        private void LoginGoogle_Clicked()
        {
            Iauthentication.GoogleLogin();
        }

        private async Task MostrarTerminos()
        {
            await MaterialDialog.Instance.AlertAsync(message: "Términos",
                                                    title: "Términos de servicio",
                                                    acknowledgementText: "OK");
        }

        private async Task MostrarPoliticas()
        {
            await MaterialDialog.Instance.AlertAsync(message: "Políticas",
                                                    title: "Políticas de privacidad",
                                                    acknowledgementText: "OK");
        }

        private async Task CrearCuenta()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Registrando usuario");
            try
            {
                UsuarioModel usuarioRegister = new UsuarioModel()
                {
                    NombreCompleto = this.NombreCompleto,
                    Email = this.Email,
                    Contraseña = this.Contraseña,
                    Telefono = this.Telefono,
                    NombreUsuario = this.NombreUsuario
                };                

                //peticion post para registro de usuarios nuevos 
                string endPoint = "/api/auth/register";
                var serviceHttp = new HttpHelper<UsuarioModel>();
                var HttpResponse = await serviceHttp.PostRestServiceDataAsync(endPoint, usuarioRegister);
                if(HttpResponse.IsSuccessStatusCode)
                {
                    var jsonResult = await HttpResponse.Content.ReadAsStringAsync();
                    var loginModel = JsonConvert.DeserializeObject<LoginHttpResponseModel>(jsonResult);
                    var Token = loginModel.Token;
                    var Login = loginModel.Login;

                    //guardamos el token y los datos de login del usuario con Xamarin Secure Storage
                    await SecureStorage.SetAsync("identity_token", Token);
                    await SecureStorage.SetAsync("identity_login", Login);

                    //await SecureStorage.SetAsync("identity_email", usuarioLogin.Email);
                    //await SecureStorage.SetAsync("identity_password", usuarioLogin.Password);
                    await loadingDialog.DismissAsync();
                    App.Current.MainPage = new HomeView();
                    //await App.Current.MainPage.DisplayAlert("Registro", "Registro exitoso", "Ok");
                }
                else
                {
                    await loadingDialog.DismissAsync();
                    await App.Current.MainPage.DisplayAlert("Error", "El usuario ya está registrado", "Ok");
                }
            }
            catch (Exception e)
            {
                await loadingDialog.DismissAsync();
                await App.Current.MainPage.DisplayAlert("Error", "no se ha podido iniciar sesión", "Ok");
            }
        }
    }
}
