using System.Linq;
using IdentitySTS.Config;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Thinktecture.IdentityServer.AspNetIdentity;
using Thinktecture.IdentityServer.Core.Services;

namespace IdentitySTS
{
    public class AspNetIdentityUserServiceFactory
    {
        static AspNetIdentityUserServiceFactory()
        {
//#if USE_INT_PRIMARYKEY
//            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.CreateDatabaseIfNotExists<CustomDbContext>());
//#else
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.CreateDatabaseIfNotExists<IdentityDbContext>());
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.CreateDatabaseIfNotExists<IdentitySTSContext>());
//#endif
        }

        public static IUserService Factory(string connString)
        {
//#if USE_INT_PRIMARYKEY
//            connString += "_CustomPK";

//            var db = new IdentityDbContext<CustomUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(connString);
//            var store = new UserStore<CustomUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(db);
//            var mgr = new UserManager<CustomUser, int>(store);
//            mgr.PasswordValidator = new PasswordValidator
//            {
//                RequiredLength = 3
//            };
//            var userSvc = new AspNetIdentityUserService<CustomUser, int>(mgr, db);
//            return userSvc;
//#else
            var db = new IdentitySTSContext(connString);
            var u = db.Users.ToList();
            
            var store = new UserStore<IdentityUser>(db);
            var mgr = new UserManager<IdentityUser>(store);
            mgr.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3
            };
            var userSvc = new AspNetIdentityUserService<IdentityUser, string>(mgr, db);
            return userSvc;
//#endif
        }
    }
}