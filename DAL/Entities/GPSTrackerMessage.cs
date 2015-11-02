using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GPSTrackerMessage
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Time { get; set; }

        public int GPSTrackerId { get; set; }
        public virtual GPSTracker GPSTracker { get; set; }
    }
}
