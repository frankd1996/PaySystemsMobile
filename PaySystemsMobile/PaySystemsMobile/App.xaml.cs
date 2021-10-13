using PaySystemsMobile.Services.Authentication;
using PaySystemsMobile.ViewModels;
using PaySystemsMobile.Views;
using System;
using System.Collections;
using System.Collections.Generic;
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
            MainPage = new MaterialNavigationPage(new PresentacionView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
