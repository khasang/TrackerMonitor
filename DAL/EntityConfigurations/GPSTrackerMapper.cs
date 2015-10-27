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
    public class GPSTrackerMapper : EntityTypeConfiguration<GPSTracker>
    {
        public GPSTrackerMapper()
        {
            this.ToTable("GPSTrackers");

            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(c => c.Id).IsRequired();

            this.HasRequired<ApplicationUser>(c => c.Owner)
                .WithMany(o => o.GPSTrackers)
                .HasForeignKey(c => c.OwnerId)

            this.HasMany<GPSTrackerMessage>(c => c.GPSTrackerMessages)
                .WithRequired(m => m.GPSTracker)
                .HasForeignKey(m => m.GPSTrackerId)
                .WillCascadeOnDelete(true);
        }
    }
}
