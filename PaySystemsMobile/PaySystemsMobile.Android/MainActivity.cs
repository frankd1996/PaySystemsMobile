using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Net;

namespace PaySystemsMobile.Droid
{
    [Activity(Label = "PaySystems", Icon = "@mipmap/icono_aplicacion", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState);
            global::Xamarin.Auth.CustomTabsConfiguration.CustomTabsClosingMessage = null;
            XF.Material.Droid.Material.Init(this, savedInstanceState);
            ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true; //solucionamos error de certificacion de SSL en Android (URL remota con Conveyor)
            LoadApplication(new App());            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);//no se si es necesario para error de SSL en Android (URL remota con Conveyor)

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// Hacemos que funcione el botón de atrás e Android para descartar un material dialog
        /// </summary>
        public override void OnBackPressed()
        {
            XF.Material.Droid.Material.HandleBackButton(base.OnBackPressed);

            //No need to call  Rg.Plugins.Popup.Popup.SendBackPressed();
        }

    }
}