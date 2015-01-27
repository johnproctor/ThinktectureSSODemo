using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace IdentitySingleSignOnDemo
{
    [assembly: OwinStartup(typeof(IdentitySingleSignOnDemo.Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                //Authority = "https://localhost:44307/identity",
                //ClientId = "mvc",
                //RedirectUri = "https://localhost:44306/",
                //ResponseType = "id_token token",
                //Scope = "openid email profile",
                //SignInAsAuthenticationType = "Cookies",
                //ClientSecret = "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols="

                ClientId = "implicientclients",
                Authority = "https://localhost:44307/identity",
                RedirectUri = "https://localhost:44306/",
                ResponseType = "id_token",
                Scope = "openid email",

                SignInAsAuthenticationType = "Cookies",
            });
        }
    }
}