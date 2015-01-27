using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thinktecture.IdentityManager;
using Thinktecture.IdentityServer.AspNetIdentity;

namespace IdentitySTS
{
    public class AspNetIdentityIdentityManagerFactory
    {
        static AspNetIdentityIdentityManagerFactory()
        {
#if USE_INT_PRIMARYKEY
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.CreateDatabaseIfNotExists<CustomDbContext>());
#else
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.CreateDatabaseIfNotExists<IdentityDbContext>());
#endif

        }

        string connString;
        public AspNetIdentityIdentityManagerFactory(string connString)
        {
            this.connString = connString;
#if USE_INT_PRIMARYKEY
            this.connString += "_CustomPK";
#endif
        }

        public IIdentityManagerService Create()
        {
#if USE_INT_PRIMARYKEY
            var db = new IdentityDbContext<CustomUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(connString);
            var store = new UserStore<CustomUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(db);
            var usermgr = new UserManager<CustomUser, int>(store);
            usermgr.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3
            };

            var rolestore = new RoleStore<CustomRole, int, CustomUserRole>(db);
            var rolemgr = new RoleManager<CustomRole, int>(rolestore);

            var svc = new Thinktecture.IdentityManager.AspNetIdentity.AspNetIdentityManagerService<CustomUser, int, CustomRole, int>(usermgr, rolemgr);
            var dispose = new DisposableIdentityManagerService(svc, db);
            return dispose;
#else
            var db = new IdentityDbContext<IdentityUser>(this.connString);
            var userstore = new UserStore<IdentityUser>(db);
            var usermgr = new Microsoft.AspNet.Identity.UserManager<IdentityUser>(userstore);
            usermgr.PasswordValidator = new Microsoft.AspNet.Identity.PasswordValidator
            {
                RequiredLength = 3
            };
            var rolestore = new RoleStore<IdentityRole>(db);
            var rolemgr = new Microsoft.AspNet.Identity.RoleManager<IdentityRole>(rolestore);

            var svc = new Thinktecture.IdentityManager.AspNetIdentity.AspNetIdentityManagerService<IdentityUser, string, IdentityRole, string>(usermgr, rolemgr);
            var dispose = new DisposableIdentityManagerService(svc, db);
            return dispose;
#endif
        }
    }
}