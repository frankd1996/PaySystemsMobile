using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace PaySystemsMobile.Services.Authentication
{
    public interface IAuthentication
    {       
        Task GoogleLogin();
    }
}
