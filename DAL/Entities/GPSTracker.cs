using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GPSTracker
    {
        public GPSTracker()
        {
            GPSTrackerMessages = new HashSet<GPSTrackerMessage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public int OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<GPSTrackerMessage> GPSTrackerMessages { get; set; }
    }
}
