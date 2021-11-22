using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PaySystemsMobile.ViewModels
{
    public class StreamingViewModel
    {
        public ICommand ComprarCommand { get; set; }

        public StreamingViewModel()
        {
            ComprarCommand = new Command(() => App.Current.MainPage.DisplayAlert("Compra", "¡Gracias por su compra!", "Ok"));
        }
    }
}
