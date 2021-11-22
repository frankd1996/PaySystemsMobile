using PaySystemsMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Dialogs;

namespace PaySystemsMobile.ViewModels
{
    public class HomeViewModel
    {
        public ICommand SalirCommand { get; set; }

        public HomeViewModel()
        {
            SalirCommand = new Command(async () => await CerrarSesion());
        }

        public async Task CerrarSesion()
        {
            SecureStorage.Remove("identity_token");
            SecureStorage.Remove("identity_login");

            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Cerrando sesión"))
            {
                await Task.Delay(2000); // Represents a task that is running.
            }

            App.Current.MainPage = new MaterialNavigationPage(new PresentacionView());
        }
    }
}
