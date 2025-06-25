using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.Auth
{
    public class LoginModel
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
    }
}