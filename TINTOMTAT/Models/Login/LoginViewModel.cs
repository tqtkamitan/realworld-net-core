using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.Login
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RedirecUrl { get; set; }
    }
}