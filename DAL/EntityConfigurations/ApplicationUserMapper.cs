using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityConfigurations
{
    public class ApplicationUserMapper : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMapper()
        {
            this.HasOptional(u => u.UserProfile)
                .WithRequired(p => p.User);
        }
    }
}
