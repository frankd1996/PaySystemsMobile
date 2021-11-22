using System;
using System.Collections.Generic;
using System.Text;

namespace PaySystemsMobile.Models
{
    public class LoginHttpResponseModel
    {
        public string Token { get; set; }
        public string Login { get; set; }
        public string Errors { get; set; }
    }
}
