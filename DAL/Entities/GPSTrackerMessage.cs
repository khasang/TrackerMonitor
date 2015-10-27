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

        /// <summary>
        /// Координаты широты
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Координаты долготы
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Время отправки сообщения
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Устройство, которое отправило сообщение
        /// </summary>
        public int GPSTrackerId { get; set; }
        public virtual GPSTracker GPSTracker { get; set; }
    }
}
