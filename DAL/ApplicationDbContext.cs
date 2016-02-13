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
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Здесь подключаем настройки связей сущностей между собой

            modelBuilder.Configurations.Add(new ApplicationUserMapper());
            modelBuilder.Configurations.Add(new GPSTrackerMapper());
            modelBuilder.Configurations.Add(new GPSTrackerMessageMapper());
            modelBuilder.Configurations.Add(new UserProfileMapper());

            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<ApplicationUser>().HasOptional(x => x.UserProfileId).WithRequired();

        }

    }
}
