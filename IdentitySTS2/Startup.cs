using System;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using IdentitySTS2.IdMgr;
using Owin;
using Thinktecture.IdentityManager;
using Thinktecture.IdentityManager.Configuration;
using Thinktecture.IdentityServer.Core.Configuration;
using IdentitySTS2.AspId;
using Thinktecture.IdentityServer.Core.Logging;


namespace IdentitySTS2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            app.Map("/admin", adminApp =>
            {
                var factory = new IdentityManagerServiceFactory();
                factory.ConfigureSimpleIdentityManagerService("Context");
                //factory.ConfigureCustomIdentityManagerServiceWithIntKeys("AspId_CustomPK");

                adminApp.UseIdentityManager(new IdentityManagerOptions()
                {
                    Factory = factory
                });
            });

            var idSvrFactory = Factory.Configure();
            idSvrFactory.ConfigureUserService("Context");
            //idSvrFactory.ConfigureCustomUserService("AspId_CustomPK");

            //var options = new IdentityServerOptions
            //{
            //    SiteName = "Thinktecture IdentityServer3 - UserService-AspNetIdentity",
            //    SigningCertificate = LoadCertificate(),
            //    Factory = idSvrFactory,
            //    CorsPolicy = CorsPolicy.AllowAll,
            //    AuthenticationOptions = new AuthenticationOptions
            //    {
                   
            //    }
            //};

            //app.UseIdentityServer(options);

            app.Map("/identity", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Identity IdentityServer",
                    SigningCertificate = LoadCertificate(),
                    CorsPolicy = CorsPolicy.AllowAll,
                    Factory = idSvrFactory
                });
            });
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\bin\Cert\demo.pfx", AppDomain.CurrentDomain.BaseDirectory), "password");
        }   
    }
}