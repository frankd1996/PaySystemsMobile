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
    public partial class RegistroView : ContentPage
    {
        RegistroViewModel vm { get; set; }
        bool AceptaTerminos { get; set; }
        public RegistroView()
        {
            InitializeComponent();
            vm = new RegistroViewModel(Navigation, new FVModelValidator());
            this.BindingContext = vm;
            AceptaTerminos = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.ErrorsChanged += Vm_ErrorsChanged;
            BotonCrearCuenta.IsEnabled = false;
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
                case nameof(vm.NombreCompleto):
                    NombreCompleto.LeadingIconTintColor = propErrors ? Color.FromHex("#B00020") : Color.DarkGray;
                    NombreCompleto.HelperText = propErrors ? propText : "";
                    NombreCompleto.HelperTextColor = propErrors ? Color.FromHex("#B00020") : Color.Transparent;                    
                    break;
                case nameof(vm.Email):
                    Email.LeadingIconTintColor = propErrors ? Color.FromHex("#B00020") : Color.DarkGray;
                    Email.HelperText = propErrors ? propText : "";
                    Email.HelperTextColor = propErrors ? Color.FromHex("#B00020") : Color.Transparent;                    
                    break;
                case nameof(vm.Contraseña):
                    Contraseña.LeadingIconTintColor = propErrors ? Color.FromHex("#B00020") : Color.DarkGray;
                    Contraseña.HelperText = propErrors ? propText : "";
                    Contraseña.HelperTextColor = propErrors ? Color.FromHex("#B00020") : Color.Transparent;
                    break;
                case nameof(vm.Telefono):
                    Telefono.LeadingIconTintColor = propErrors ? Color.FromHex("#B00020") : Color.DarkGray;
                    Telefono.HelperText = propErrors ? propText : "";
                    Telefono.HelperTextColor = propErrors ? Color.FromHex("#B00020") : Color.Transparent;
                    break;
                case nameof(vm.NombreUsuario):
                    NombreUsuario.LeadingIconTintColor = propErrors ? Color.FromHex("#B00020") : Color.DarkGray;
                    NombreUsuario.HelperText = propErrors ? propText : "";
                    NombreUsuario.HelperTextColor = propErrors ? Color.FromHex("#B00020") : Color.Transparent;
                    break;

                default:
                    break;
            }

            if (vm.HasErrors || string.IsNullOrEmpty(vm.NombreCompleto) || string.IsNullOrEmpty(vm.Email)
                || string.IsNullOrEmpty(vm.Contraseña) || string.IsNullOrEmpty(vm.Telefono)
                || string.IsNullOrEmpty(vm.NombreUsuario) || CheckBoxTerminos.IsSelected == false)
            {
                BotonCrearCuenta.IsEnabled = false;
            }
            else
            {
                BotonCrearCuenta.IsEnabled = true;
            }
        }

        private void CheckBoxTerminos_SelectedChanged(object sender, XF.Material.Forms.UI.SelectedChangedEventArgs e)
        {
            Vm_ErrorsChanged(new object(), new DataErrorsChangedEventArgs(""));            
        }
    }
}