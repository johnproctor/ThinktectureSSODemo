using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOnDemo
{
    [assembly: OwinStartup(typeof(SingleSignOnDemo.Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:44303/identity",
                ClientId = "mvc",
                RedirectUri = "https://localhost:44304/",
                ResponseType = "id_token",

                SignInAsAuthenticationType = "Cookies"
            });
        }
    }
}