using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDWPAuth.Models
{
    public class ManagerModel
    {
        public static ApplicationUserManager AppUser { get; set; }
        public static ApplicationSignInManager SigninUser { get; set; }
    }
}