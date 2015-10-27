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
    public class GPSTrackerMessageMapper : EntityTypeConfiguration<GPSTrackerMessage>
    {
        public GPSTrackerMessageMapper()
        {
            this.ToTable("GPSTrackerMessages");

            this.Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Id).IsRequired();

            this.Property(m => m.Latitude).HasPrecision(17, 15);
            this.Property(m => m.Longitude).HasPrecision(18, 15);

            this.HasRequired(m => m.GPSTracker)
                .WithMany(t => t.GPSTrackerMessages)
                .HasForeignKey(m => m.GPSTrackerId);
        }
    }
}
