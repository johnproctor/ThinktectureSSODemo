using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySTS2.AspId
{
    //public class User : IdentityUser { }
    //public class Role : IdentityRole { }

    public class Context : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public Context(string connString)
            : base(connString)
        {
        }
    }

    public class UserStore : UserStore<IdentityUser>
    {
        public UserStore(Context ctx)
            : base(ctx)
        {
        }
    }

    public class UserManager : UserManager<IdentityUser>
    {
        public UserManager(UserStore store)
            : base(store)
        {
        }
    }

    public class RoleStore : RoleStore<IdentityRole>
    {
        public RoleStore(Context ctx)
            : base(ctx)
        {
        }
    }

    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(RoleStore store)
            : base(store)
        {
        }
    }
}