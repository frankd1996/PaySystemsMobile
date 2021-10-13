using System;
using System.Collections.Generic;
using System.Text;

namespace PaySystemsMobile.AppConstant
{
    public class Constants
    {
		public static string AppName = "OAuthNativeFlow";

		// OAuth
		// For Google login, configure at https://console.developers.google.com/
		public static string iOSClientId = "684814794541-rois7f998ocq459spnfjnenm7jonus4r.apps.googleusercontent.com";
		public static string AndroidClientId = "684814794541-pp09338590np909g3ni734j52mfpfjq0.apps.googleusercontent.com";

		// These values do not need changing
		public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
		public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
		public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
		public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

		// Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
		public static string iOSRedirectUrl = "com.googleusercontent.apps.684814794541-rois7f998ocq459spnfjnenm7jonus4r:/oauth2redirect";
		public static string AndroidRedirectUrl = "com.googleusercontent.apps.684814794541-pp09338590np909g3ni734j52mfpfjq0:/oauth2redirect";
	}
}
