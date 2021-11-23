# PaySystemsMobile

PaySystemsMobile is a project built with the Xamarin Forms framework (Android Project), which consists of a virtual store that offers streaming services and online purchases paying with your local currency. This project has served as the basis for starting the development of my personal ServiWallet project, which will offer these services and many more! 

## ¡Welcome to PaySystemsMobile!

Click on the image to see on Youtube:

[![Alt text](https://img.youtube.com/vi/OaVvTxtXDYQ/0.jpg)](https://www.youtube.com/watch?v=OaVvTxtXDYQ)

PaySystemsMobile uses as a backend a .net Core web api with .Net 5, which is in production. You can visit the source code for PaySystemsApi here: https://github.com/frankd1996/PaySystemsAPI. In this Api we implement authorization and authentication with Microsoft.AspnetCore.Identity (JWT)
Here is what I consider to be the most important implementations that you will find in this project:

-MVVM design pattern, for an adequate decoupling between views and business logic.

-Fluent Validation: this library allows form validations very quickly and easily. It is available for most of the .NET frameworks 

-XF.Material - This is a fantastic nuget package with a flawless implementation of the Material Design philosophy for building nice interfaces.

-Xamarin Community Toolkit (xct): this nuget package is a powerful tool to solve common problems and to shorten development times, such as converting any event of a UI control into commands.

-SplashScreen: typical presentation screen of most applications when launching them.

-Http requests with generics: This implementation seems fundamental to me for the reuse of the HttHelper class in a quick and easy way

-Optimal handling of images: in the Android project you will be able to see that the images used are optimized for any device on which the application is used, considering screen densities (an interesting feature of Xamarin Forms).

-Xamarin Essentials (Secure Storage): with secure storage we store locally and encrypted key-value objects. In this case we save the Login Token (JWT) and a Boolean variable that indicates if the session is active.

-Xamarin Forms Shell: Shell is an amazing UI control that enables fast implementation of Flyout (side menu) and TabBars. A fundamental tool for agile development

-Fonts and Icons: we use Google Font icons and Google Fonts

-Dependency injection: we make use of this Inversion of Control technique for the implementation of social authentication with Google (still under development). Dependency injection allows for optimal decoupling between the authentication provider and the implementation.

Note: Remember that this app works with a backend that is in production and I will be constantly improving it. If you are testing the app and can't sign up, visit PaySystemsApi on my Github and clone it. For easy localhost communication, add the Conveyor by Keyoti extension in the api:

-How to install Conveyor by Keyoti in VS Community: https://www.youtube.com/watch?v=zCXJlab-yDc&feature=emb_imp_woyt
Add these lines of code to the Android project (MainActivity) After installing conveyor in the api:

``` public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
           ...
            ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true; 
            ...           
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            ...
        }```


