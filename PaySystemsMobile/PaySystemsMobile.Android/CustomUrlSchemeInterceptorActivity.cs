using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PaySystemsMobile.AuthHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaySystemsMobile.Droid
{
	[Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop,
		MainLauncher = false)]
	[IntentFilter(
	 new[] { Intent.ActionView },
	 Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
	 DataSchemes = new[] { "com.googleusercontent.apps.684814794541-pp09338590np909g3ni734j52mfpfjq0" },
	 DataPath = "/oauth2redirect")]
	public class CustomUrlSchemeInterceptorActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Convert Android.Net.Url to Uri
			var uri = new Uri(Intent.Data.ToString());

			// Load redirectUrl page
			AuthenticationState.Authenticator.OnPageLoading(uri);
			Intent intent = new Intent(Application.Context, typeof(MainActivity));
			intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
			StartActivity(intent);
			this.Finish();					
		}
	}
}