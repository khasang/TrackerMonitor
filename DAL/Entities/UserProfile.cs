using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<GPSTracker> GPSTrackers { get; set; }
        
        public UserProfile()
        {
            GPSTrackers = new HashSet<GPSTracker>();
        }
    }

    
}
