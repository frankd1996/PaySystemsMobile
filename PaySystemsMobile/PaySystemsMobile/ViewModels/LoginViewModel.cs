using FluentValidation;
using Newtonsoft.Json;
using PaySystemsMobile.Helpers;
using PaySystemsMobile.Models;
using PaySystemsMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace PaySystemsMobile.ViewModels
{
    public class LoginViewModel: BaseValidationViewModel
    {
        public LoginModel ValidationContext = new LoginModel();
        public INavigation Navigation { get; set; }
        public ICommand IniciarSesionCommand { get; set; }
        public ICommand NavegaraPresentacionCommand { get; set; }
        private string _email;
        private string _password;
        
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

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                ValidationContext.Password = value;
                Validate(ValidationContext);
                OnPropertyChanged();
            }
        }
        public LoginViewModel(INavigation navigation, IValidator validator):base(validator)
        {
            Navigation = navigation;
            NavegaraPresentacionCommand = new Command(async () => await Navigation.PopAsync());
            IniciarSesionCommand = new Command(async () => await Login());
        }

        public async Task Login()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Iniciando sesión");
            try
            {
                LoginModel usuarioLogin = new LoginModel()
                {
                    Email = this.Email,
                    Password = this.Password
                };                

                //petición post para inicio de sesión
                string endPoint = "/api/auth/login";// /api/auth/register
                var serviceHttp = new HttpHelper<LoginModel>();
                var HttpResponse = await serviceHttp.PostRestServiceDataAsync(endPoint, usuarioLogin);

                if (HttpResponse.IsSuccessStatusCode)
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
                await App.Current.MainPage.DisplayAlert("Error", "Error inesperado al inciar sesión", "Ok");
            }
        }
    }
}
