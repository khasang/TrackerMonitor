using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBInitializers
{
    class InitUserAdmin : IInitialization
    {
        public void Initialization(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            ApplicationUser user = new ApplicationUser()
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"
            };

            IdentityRole role = new IdentityRole("Admin");

            userManager.Create(user, "password");
            roleManager.Create(role);
            userManager.AddToRole(user.Id, role.Name); ;
        }
    }
}
