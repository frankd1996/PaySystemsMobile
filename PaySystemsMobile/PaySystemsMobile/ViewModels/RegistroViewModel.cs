using PaySystemsMobile.Services.Authentication;
using PaySystemsMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace PaySystemsMobile.ViewModels
{
    public class RegistroViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand NavegaraPresentacionCommand { get; set; }
        public ICommand Login { get; set; }
        public IAuthentication Iauthentication { get; set; }
        public ICommand IRegistroUsuario { get; set; }

        public ICommand TerminosCommand { get; set; }
        public ICommand PoliticasCommand { get; set; }
        public ICommand NavegaraIniciarsesionCommand { get; set; }
        public RegistroViewModel(INavigation navigation)
        {
            Navigation = navigation;
            NavegaraPresentacionCommand = new Command(async() => await Navigation.PopAsync());
            Login = new Command(() => LoginGoogle_Clicked());
            TerminosCommand = new Command(async() => await MostrarTerminos());
            PoliticasCommand = new Command(async() => await MostrarPoliticas());
            NavegaraIniciarsesionCommand = new Command(async() => await Navigation.PopAsync());

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
    }
}
