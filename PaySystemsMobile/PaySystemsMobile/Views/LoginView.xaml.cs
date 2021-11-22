using PaySystemsMobile.Validators;
using PaySystemsMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaySystemsMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginViewModel vm { get; set; }
        public LoginView()
        {
            InitializeComponent();
            vm = new LoginViewModel(Navigation, new LoginValidator());
            this.BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.ErrorsChanged += Vm_ErrorsChanged;
            botonIniciarSesion.IsEnabled = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.ErrorsChanged -= Vm_ErrorsChanged;
        }

        private void Vm_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            var propErrors =
                (vm.GetErrors(e.PropertyName) as List<string>)?.Any() == true;

            var messages = (vm.GetErrors(e.PropertyName) as List<string>);
            var propText = string.Join("\n", messages);

            switch (e.PropertyName)
            {                
                case nameof(vm.Email):
                    Email.LeadingIconTintColor = propErrors ? Color.FromHex("#B00020") : Color.DarkGray;
                    Email.HelperText = propErrors ? propText : "";
                    Email.HelperTextColor = propErrors ? Color.FromHex("#B00020") : Color.Transparent;
                    break;
                case nameof(vm.Password):
                    Password.LeadingIconTintColor = propErrors ? Color.FromHex("#B00020") : Color.DarkGray;
                    Password.HelperText = propErrors ? propText : "";
                    Password.HelperTextColor = propErrors ? Color.FromHex("#B00020") : Color.Transparent;
                    break;              
                default:
                    break;
            }

            if (vm.HasErrors || string.IsNullOrEmpty(vm.Email) || string.IsNullOrEmpty(vm.Password))
            {
                botonIniciarSesion.IsEnabled = false;
            }
            else
            {
                botonIniciarSesion.IsEnabled = true;
            }
        }
    }
}