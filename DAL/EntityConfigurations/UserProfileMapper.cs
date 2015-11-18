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

            this.HasKey(p => p.UserId);
        }
             
    }
}
