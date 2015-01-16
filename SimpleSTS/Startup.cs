using Microsoft.Owin;
using Owin;
using SimpleSTS.STSSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Models;

namespace SimpleSTS
{
    [assembly: OwinStartup(typeof(SimpleSTS.Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idsrvApp =>
                {
                    idsrvApp.UseIdentityServer(new IdentityServerOptions
                    {
                        SiteName = "Embedded IdentityServer",
                        SigningCertificate = LoadCertificate(),

                        Factory = InMemoryFactory.Create(
                            users: Users.Get(),
                            clients: Clients.Get(),
                            scopes: StandardScopes.All)
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
