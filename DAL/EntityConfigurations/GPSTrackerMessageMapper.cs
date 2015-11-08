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

            this.HasRequired(m => m.GPSTracker)
                .WithMany(t => t.GPSTrackerMessages)
                .HasForeignKey(m => m.GPSTrackerId);
        }
    }
}
