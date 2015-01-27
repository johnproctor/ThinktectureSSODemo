using System;
using System.Data.Entity;
using Owin;
using Thinktecture.IdentityManager;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Models;
using Thinktecture.IdentityServer.AspNetIdentity;
using System.Security.Cryptography.X509Certificates;
﻿using IdentitySTS.Config;
﻿using Microsoft.Owin;
using Owin;
using Thinktecture.IdentityManager.Configuration;

[assembly: OwinStartupAttribute(typeof(IdentitySTS.Startup))]
namespace IdentitySTS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {

            Database.SetInitializer(new CreateDatabaseIfNotExists<IdentitySTSContext>());

            appBuilder.Map("/admin", adminApp =>
            {
                var factory = new IdentityManagerServiceFactory();

                factory.ConfigureSimpleIdentityManagerService("IdentitySTS");
                //factory.ConfigureCustomIdentityManagerServiceWithIntKeys("AspId_CustomPK");

                adminApp.UseIdentityManager(new IdentityManagerOptions()
                {
                    Factory = factory
               
                });
            });


            var options = new IdentityServerOptions
            {
                IssuerUri = "https://localhost:44305",
                SiteName = "Thinktecture IdentityServer v3 - UserService-AspNetIdentity",
                RequireSsl = true,

                SigningCertificate = LoadCertificate(),
                Factory = Factory.Configure("IdentitySTS"),
                CorsPolicy = CorsPolicy.AllowAll,
               
                //AuthenticationOptions = new AuthenticationOptions()
                //{
                    
                //}
               
   
            };

            options.DiagnosticsOptions.EnableHttpLogging = true;

            appBuilder.UseIdentityServer(options);
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\bin\Cert\demo.pfx", AppDomain.CurrentDomain.BaseDirectory), "password");
        }
    }
}
