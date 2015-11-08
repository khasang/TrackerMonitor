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
        /// Широта
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Дата, время отправки
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Какому трекеру принадлежит сообщение
        /// </summary>
        public string GPSTrackerId { get; set; }
        public virtual GPSTracker GPSTracker { get; set; }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            return str.Append("GPSTrackerId: ").AppendLine(GPSTrackerId)
                        .Append("Latitude: ").AppendLine(Latitude.ToString())
                        .Append("Longitude: ").AppendLine(Longitude.ToString())
                        .Append("Date: ").AppendLine(Time.ToString())
                        .AppendLine(" ")
                        .ToString();
        }
    }
}
