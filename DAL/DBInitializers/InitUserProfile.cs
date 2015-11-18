using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DAL.Entities;

namespace DAL.DBInitializers
{
    public class InitUserProfile : InitializationDB
    {
        public override void Initialization(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == "admin@admin.com");
            var userProfile = new UserProfile() { User = user };
            context.UserProfiles.Add(userProfile);

            context.SaveChanges();
        }
    }
}
