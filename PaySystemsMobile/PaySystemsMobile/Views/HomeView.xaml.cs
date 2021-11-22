using PaySystemsMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaySystemsMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : Shell
    {
        HomeViewModel vm = new HomeViewModel();
        public HomeView()
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}