using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using REST.Providers;
using REST.Models;

namespace REST
{
    /// <summary>
    /// Startup class
    /// </summary>
    /// <remarks></remarks>
    public partial class Startup
    {
        static Startup()
        {
            PublicClientId = "self";
            UserManagerFactory = () => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }
        /// <summary>
        /// Gets or sets OAuthOptions.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        /// <summary>
        /// Client Id
        /// </summary>
        public static string PublicClientId { get; private set; }
        public static Func<UserManager<ApplicationUser>> UserManagerFactory { get; set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// <summary>
        /// Configure Authentication process(use cookie?, token?), set auth via i.e Facebook
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            
            // Configure the application for OAuth based flow

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "478916338953318",
            //    appSecret: "92c166393ce39d78ece1beda278853e5");
            
            var googleProvider = new GoogleOAuth2AuthenticationProvider
            {
                OnAuthenticated = (context) =>
                {
                    context.Identity.AddClaim(new Claim(ClaimTypes.Email, context.Email));
                    return Task.FromResult(0);
                },
            };
            var googleOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "233205194812-a9gb3u4i4didt390u6k1l2sfu93mdvpv.apps.googleusercontent.com",
                ClientSecret = "F1IPtcZk16yTcLqgw56m4A4E",
                Provider = googleProvider,
                //CallbackPath = new PathString("/api/Account/ManageInfo"),
            };
            googleOptions.Scope.Add("email");
            app.UseGoogleAuthentication(googleOptions);

            /*using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                const string email = "godfryd2@gmail.com";
                var existingUser = um.FindByEmail(email);
                if (existingUser == null)
                {
                    um.Create(new ApplicationUser
                    {
                        Email = email,
                        EmailConfirmed = true,
                        UserName = email,
                        LockoutEnabled = false,
                    },
                    "Test1#");
                }
            }*/
        }
    }
}
