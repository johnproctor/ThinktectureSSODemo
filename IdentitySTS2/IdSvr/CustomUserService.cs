using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thinktecture.IdentityServer.AspNetIdentity;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Services;

namespace IdentitySTS2.IdMgr
{
    //public static class CustomUserServiceExtensions
    //{
    //    public static void ConfigureCustomUserService(this IdentityServerServiceFactory factory, string connString)
    //    {
    //        factory.UserService = new Registration<IUserService, CustomUserService>();
    //        factory.Register(new Registration<CustomUserManager>());
    //        factory.Register(new Registration<CustomUserStore>());
    //        factory.Register(new Registration<CustomContext>(resolver => new CustomContext(connString)));
    //    }
    //}

    //public class CustomUserService : AspNetIdentityUserService<CustomUser, int>
    //{
    //    public CustomUserService(CustomUserManager userMgr)
    //        : base(userMgr)
    //    {
    //    }
    //}
}