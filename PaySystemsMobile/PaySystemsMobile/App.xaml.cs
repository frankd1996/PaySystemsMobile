using PaySystemsMobile.Services.Authentication;
using PaySystemsMobile.ViewModels;
using PaySystemsMobile.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms;
using XF.Material.Forms.UI;

[assembly: ExportFont("MaterialIcons-Regular.ttf", Alias = "Material")]
[assembly: ExportFont("GoogleIcon.ttf", Alias = "GoogleIcon")]
namespace PaySystemsMobile
{    
    public partial class App : Application
    {
        public App()
        {
            XF.Material.Forms.Material.Init(this);
            InitializeComponent();
            Material.Use("Material.Configuration");
            //MainPage =  new HomeView();
            MainPage = new MaterialNavigationPage(new PresentacionView());
        }

        protected override void OnStart()
        {
            VerificarTokenOnStart();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            //VerificarTokenOnResume();
        }

        public async Task VerificarTokenOnStart()
        {
            var Token = await SecureStorage.GetAsync("identity_token");
            var Login = await SecureStorage.GetAsync("identity_login");            

            if (Token == null || Login == null)
            {
                App.Current.MainPage = new MaterialNavigationPage(new PresentacionView());
            }
            else
            {
                App.Current.MainPage = new HomeView();
            }
        }

        //public async Task VerificarTokenOnResume()
        //{
        //    var Token = await SecureStorage.GetAsync("identity_token");
        //    var Login = await SecureStorage.GetAsync("identity_login");

        //    if ((Token == null || Login == null) && App.Current.MainPage != )
        //    {
        //        App.Current.MainPage = new MaterialNavigationPage(new PresentacionView());                
        //    }
        //}
    }
}
