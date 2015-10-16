using DAL.Entities;
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
        // public DbSet<Entity> Entities { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Здесь подключаем настройки связей сущностей между собой

            modelBuilder.Configurations.Add(new ClassMapper());
         

            base.OnModelCreating(modelBuilder);
        }
         */
    }
}
