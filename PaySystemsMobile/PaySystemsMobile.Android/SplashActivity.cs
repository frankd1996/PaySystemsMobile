using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemsMobile.Droid
{
    [Activity(Label = "PaySystems", Theme ="@style/SplashTheme", Icon = "@mipmap/icono_aplicacion",
        MainLauncher =true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize 
        | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout 
        | ConfigChanges.SmallestScreenSize)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);           
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            // Create your application here
        }       
    }
}