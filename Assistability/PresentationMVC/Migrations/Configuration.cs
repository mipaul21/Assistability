/// <summary>
/// Jory A. Wernette
/// Created: 2021/04/07
/// 
/// Created this file from the NuGet console
/// </summary>

namespace PresentationMVC.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PresentationMVC.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PresentationMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        /// <remarks>
        /// Jory A. Wernette
        /// Updated: 2021/04/07
        /// Updated Seed method to contain an admin
        /// </remarks>
        protected override void Seed(PresentationMVC.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            const string admin = "first@administrator.com";
            const string defaultPassword = "newuser";
            const string client = "client@company.com";

            // before creating our first user, let's create our roles
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Admin" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Caregiver" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Client" });

            context.SaveChanges(); // save our newly added roles

            // now to add the admin

            if (!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    FirstName = "admin",
                    LastName = "administrator",
                    UserName = admin,
                    Email = admin
                };



                IdentityResult result = userManager.Create(user, defaultPassword);
                context.SaveChanges();

                if (result.Succeeded)
                {
                    // we created the admin, now give the admin the Admin role
                    userManager.AddToRole(user.Id, "Admin");
                    context.SaveChanges();
                }
            }


            // now to add the client

            if (!context.Users.Any(u => u.UserName == client))
            {
                var user = new ApplicationUser()
                {
                    FirstName = "client",
                    LastName = "clientUser",
                    UserName = client,
                    Email = client
                };



                IdentityResult result = userManager.Create(user, defaultPassword);
                context.SaveChanges();

                if (result.Succeeded)
                {
                    // we created the admin, now give the client the Client role
                    userManager.AddToRole(user.Id, "Client");
                    context.SaveChanges();
                }
            }
        }
    }
}
