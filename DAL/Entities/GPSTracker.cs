using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GPSTracker
    {
        /// <summary>
        /// Идентификатор устройства
        /// </summary>
        [Display(Name = "Id устройства")]
        public string Id { get; set; }

        /// <summary>
        /// Телефонный номер трекера
        /// </summary>
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Имя устройства
        /// </summary>
        [Display(Name = "Имя устройства")]
        public string Name { get; set; }

        /// <summary>
        /// Владелец устройства
        /// </summary>
        [Display(Name = "Id владельца")]
        public string OwnerId { get; set; }

        public virtual UserProfile Owner { get; set; }

        /// <summary>
        /// Активно или неактивно устройство
        /// </summary>
        [Display(Name = "Активность")]
        public bool IsActive { get; set; }

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
