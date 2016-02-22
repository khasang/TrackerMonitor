using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GPSTrackerMessage
    {
        /// <summary>
        /// Id сообщения
        /// </summary>
        [Display(Name = "Id сообщения")]
        public int Id { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        [Display(Name = "Широта")]
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [Display(Name = "Долгота")]
        public double Longitude { get; set; }

        /// <summary>
        /// Дата, время отправки
        /// </summary>
        [Display(Name = "Дата отправки")]
        public DateTime Time { get; set; }

        /// <summary>
        /// Какому трекеру принадлежит сообщение
        /// </summary>
        [Display(Name = "Id устройства")]
        public string GPSTrackerId { get; set; }

        public virtual GPSTracker GPSTracker { get; set; }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            return str.AppendLine("")
                        .Append("GPSTrackerId: ").AppendLine(GPSTrackerId)
                        .Append("Latitude: ").AppendLine(Latitude.ToString())
                        .Append("Longitude: ").AppendLine(Longitude.ToString())
                        .Append("Date: ").AppendLine(Time.ToString())                        
                        .ToString();
        }
    }
}
