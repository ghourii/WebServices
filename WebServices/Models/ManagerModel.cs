using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models
{
    public static class ManagerModel
    {
        public static ApplicationUserManager UserManager { get; set; }
        public static ApplicationSignInManager SignInManager { get; set; }
    }
}