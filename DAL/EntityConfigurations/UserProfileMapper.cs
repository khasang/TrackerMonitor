using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityConfigurations
{
    public class UserProfileMapper : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileMapper()
        {
            this.ToTable("UserProfiles");

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.HasRequired<ApplicationUser>(c => c.User).WithOptional(c => c.UserProfile).WillCascadeOnDelete(false);
        }
             
    }
}
