using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;

namespace PDWPAuth.Models
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public int? pid { get; set; }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? null;
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? null;
            }
            private set
            {
                _userManager = value;
            }
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); //   
            pid = Convert.ToInt32(context.Parameters.Where(f => f.Key == "pid").Select(f => f.Value).SingleOrDefault()[0]);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            UserManager = ManagerModel.AppUser;
            SignInManager = ManagerModel.SigninUser;

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //var owin = new OwinContext();
            //_userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(owin.Get<ApplicationDbContext>()));
            //_signInManager = new ApplicationSignInManager(owin.GetUserManager<ApplicationUserManager>(), owin.Authentication);


            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new ApplicationUserManager(store);

            var user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                //context.Rejected();
                return;
            }
            identity = await userManager.CreateIdentityAsync(user, context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("Username", context.UserName));
            identity.AddClaim(new Claim("SecurityStamp", user.SecurityStamp));
            var props = new AuthenticationProperties(new Dictionary<string, string> { { "userdisplayname", context.UserName } });
            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }
    }
}