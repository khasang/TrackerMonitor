using DAL.Entities;
using DAL.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //      Здесь подключаем сущности EF
        public DbSet<GPSTracker> GPSTrackers { get; set; }
        public DbSet<GPSTrackerMessage> GPSTrackerMessages { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public ApplicationDbContext()
            : base("PrimaryConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Здесь подключаем настройки связей сущностей между собой

            modelBuilder.Configurations.Add(new UserProfileMapper());
            modelBuilder.Configurations.Add(new GPSTrackerMessageMapper());

            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<ApplicationUser>().HasOptional(x => x.UserProfileId).WithRequired();

        }

    }
}
