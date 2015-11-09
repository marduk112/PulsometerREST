using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using REST.Models;

namespace REST.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<REST.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(REST.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                const string email = "godfryd2@gmail.com";
                var existingUser = um.FindByEmail(email);
                if (existingUser == null)
                {
                    um.Create(new ApplicationUser
                    {
                        Email = email,
                        EmailConfirmed = true,
                        UserName = email,
                        LockoutEnabled = false,
                    },
                    "Test1#");
                }
            }
        }
    }
}
