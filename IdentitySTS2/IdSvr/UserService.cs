using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentitySTS2.AspId;
using Microsoft.AspNet.Identity.EntityFramework;
using Thinktecture.IdentityServer.AspNetIdentity;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Services;
namespace IdentitySTS2.IdMgr
{
    public static class UserServiceExtensions
    {
        public static void ConfigureUserService(this IdentityServerServiceFactory factory, string connString)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context>());
            
            var c = new Context("Context");
            var u = c.Users.ToList();
            var r = c.Roles.ToList();
             
            factory.UserService = new Registration<IUserService, UserService>();
            factory.Register(new Registration<UserManager>());
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<RoleStore>());
            factory.Register(new Registration<RoleManager>());
            factory.Register(new Registration<Context>(resolver => new Context(connString)));
        }
    }

    public class UserService : AspNetIdentityUserService<IdentityUser, string>
    {
        public UserService(UserManager userMgr)
            : base(userMgr)
        {
        }
    }
}