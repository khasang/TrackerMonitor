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
    public class ProfileMapper : EntityTypeConfiguration<UserProfile>
    {
        public ProfileMapper()
        {
            this.ToTable("UserProfiles");

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
            this.HasRequired(c => c.UserId).WithOptional();


        }
             
    }
}
