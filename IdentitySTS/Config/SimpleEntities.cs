using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentitySTS.Config
{
    public class User : IdentityUser { }
    public class Role : IdentityRole { }

    public class IdentitySTSContext : IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public IdentitySTSContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class UserStore : UserStore<User>
    {
        public UserStore(IdentitySTSContext ctx)
            : base(ctx)
        {
        }
    }

    public class UserManager : UserManager<User>
    {
        public UserManager(UserStore store)
            : base(store)
        {
        }
    }

    public class RoleStore : RoleStore<Role>
    {
        public RoleStore(IdentitySTSContext ctx)
            : base(ctx)
        {
        }
    }

    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(RoleStore store)
            : base(store)
        {
        }
    }
}