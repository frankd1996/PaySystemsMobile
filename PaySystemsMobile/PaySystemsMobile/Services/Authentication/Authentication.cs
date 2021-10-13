using Newtonsoft.Json;
using PaySystemsMobile.AuthHelpers;
using PaySystemsMobile.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

[assembly: Dependency(typeof(Authentication))]
namespace PaySystemsMobile.Services.Authentication
{
    public class Authentication : IAuthentication
    {
        private Account account;

		public async Task GoogleLogin()
        {
			string clientId = null;
			string redirectUri = null;

			//Definimos las url de aplicación cliente y redirección
			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					clientId = AppConstant.Constants.iOSClientId;
					redirectUri = AppConstant.Constants.iOSRedirectUrl;
					break;

				case Device.Android:
					clientId = AppConstant.Constants.AndroidClientId;
					redirectUri = AppConstant.Constants.AndroidRedirectUrl;
					break;
			}

			//account = store.FindAccountsForService(AppConstant.Constants.AppName).FirstOrDefault();
			//account = SecureStorageAccountStore.Create().FindAccountsForService(AppConstant.Constants.AppName).FirstOrDefault();
			account = (await SecureStorageAccountStore.FindAccountsForServiceAsync(AppConstant.Constants.AppName)).FirstOrDefault();

			//aqui se inicia la vista de login con google (CustomOrlSchemeInterceptorActivity)
			//y se crea el objeto con la información de la autenticación
			var authenticator = new OAuth2Authenticator(
				clientId,
				null,
				AppConstant.Constants.Scope,
				new Uri(AppConstant.Constants.AuthorizeUrl),
				new Uri(redirectUri),
				new Uri(AppConstant.Constants.AccessTokenUrl),
				null,
				true);

			//Asociamos a los eventos correspondientes según el estado de finalización
			authenticator.Completed += OnAuthCompleted;
			authenticator.Error += OnAuthError;

			//No sé para qué se hace esto
			AuthenticationState.Authenticator = authenticator;

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);
		}

        public async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            OAuth2Authenticator authenticator = sender as OAuth2Authenticator;
			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			User user = null;

			if (e.IsAuthenticated)
			{
				// If the user is authenticated, request their basic user data from Google
				// UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
				var request = new OAuth2Request("GET", new Uri(AppConstant.Constants.UserInfoUrl), null, e.Account);
				var response =  await request.GetResponseAsync();
				if (response != null)
				{
					// Deserialize the data and store it in the account store
					// The users email address will be used to identify data in SimpleDB
					string userJson = await response.GetResponseTextAsync();
					user = JsonConvert.DeserializeObject<User>(userJson);
				}

				if (user != null)
				{
					//App.Current.MainPage = new NavigationPage(new LoginView());
					//App.Current.MainPage.Navigation.PopAsync();
				}

				//await store.SaveAsync(account = e.Account, AppConstant.Constants.AppName);
				//await DisplayAlert("Email address", user.Email, "OK");
			}
		}

        public void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            OAuth2Authenticator authenticator = sender as OAuth2Authenticator;
			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}
			Debug.WriteLine("Authentication error: " + e.Message);
		}
	}

	public class SecureStorageAccountStore
	{
		public static async Task SaveAsync(Account account, string serviceId)
		{
			// Find existing accounts for the service
			var accounts = await FindAccountsForServiceAsync(serviceId);

			// Remove existing account with Id if exists
			accounts.RemoveAll(a => a.Username == account.Username);

			// Add account we are saving
			accounts.Add(account);

			// Serialize all the accounts to javascript
			var json = JsonConvert.SerializeObject(accounts);

			// Securely save the accounts for the given service
			await SecureStorage.SetAsync(serviceId, json);
		}

		public static async Task<List<Account>> FindAccountsForServiceAsync(string serviceId)
		{
			// Get the json for accounts for the service
			var json = await SecureStorage.GetAsync(serviceId);

			try
			{
				// Try to return deserialized list of accounts
				return JsonConvert.DeserializeObject<List<Account>>(json);
			}
			catch { }

			// If this fails, return an empty list
			return new List<Account>();
		}

		public static async Task MigrateAllAccountsAsync(string serviceId, IEnumerable<Account> accountStoreAccounts)
		{
			var wasMigrated = await SecureStorage.GetAsync($"XamarinAuthAccountStoreMigrated{serviceId}");

			if (wasMigrated == "1")
				return;

			await SecureStorage.SetAsync($"XamarinAuthAccountStoreMigrated{serviceId}", "1");

			// Just in case, look at existing 'new' accounts
			var accounts = await FindAccountsForServiceAsync(serviceId);

			foreach (var account in accountStoreAccounts)
			{

				// Check if the new storage already has this account
				// We don't want to overwrite it if it does
				if (accounts.Any(a => a.Username == account.Username))
					continue;

				// Add the account we are migrating
				accounts.Add(account);
			}

			// Serialize all the accounts to javascript
			var json = JsonConvert.SerializeObject(accounts);

			// Securely save the accounts for the given service
			await SecureStorage.SetAsync(serviceId, json);
		}
	}

}
