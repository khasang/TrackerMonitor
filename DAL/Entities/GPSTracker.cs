using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GPSTracker
    {
        /// <summary>
        /// Иденификатор устройства
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя устройства
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Владелец устройства
        /// </summary>
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        /// <summary>
        /// Коллекция сообщений, отправляемых устройством
        /// </summary>
        public virtual ICollection<GPSTrackerMessage> GPSTrackerMessages { get; set; }

        public GPSTracker()
        {
            GPSTrackerMessages = new HashSet<GPSTrackerMessage>();
        }
    }
}
