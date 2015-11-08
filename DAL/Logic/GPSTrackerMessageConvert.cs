using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Logic
{
    public class GPSTrackerMessageConverter
    {
        /// <summary>
        /// Метод для парсинга сообщения от маячка в объект сообщения
        /// </summary>
        /// <param name="bytes">сообщение от маячка</param>
        /// <returns>объект сообщения</returns>
        public static GPSTrackerMessage BytesToMessage(byte[] bytes)
        {
            GPSTrackerMessage message = new GPSTrackerMessage();

            // Парсинг идентификатора маячка
            message.GPSTrackerId = Encoding.ASCII.GetString(bytes.Take(20));

            // Парсинг времени отправления сообщения
            message.Time = new DateTime(
                    Convert.ToInt32(new byte[] { bytes[10], bytes[9] }), // Парсинг года
                    Convert.ToInt32(bytes[8]), // Парсинг месяца
                    Convert.ToInt32(bytes[7]), // Парсинг числа
                    Convert.ToInt32(bytes[4]), // Парсинг часа
                    Convert.ToInt32(bytes[5]), // Парсинг минуты
                    Convert.ToInt32(bytes[6])  // Парсинг секунды
                );

            // Парсинг широты
            byte[] latitudeBytes = new byte[18];
            Array.Copy(bytes, 11, latitudeBytes, 0, 18);
            message.Latitude = Double.Parse(Encoding.UTF8.GetString(latitudeBytes));

            // Парсинг долготы
            byte[] longitudeBytes = new byte[19];
            Array.Copy(bytes, 30, longitudeBytes, 0, 19);
            message.Longitude = Double.Parse(Encoding.UTF8.GetString(longitudeBytes));

            return message;
        }

        public static byte[] MessageToBytes(GPSTrackerMessage message)
        {
            byte[] bytes = new byte[100];

            byte[] identifier = BitConverter.GetBytes(message.GPSTrackerId);
            byte[] longitude = BitConverter.GetBytes(message.Longitude);
            byte[] latitude = BitConverter.GetBytes(message.Latitude);
            byte[] year = BitConverter.GetBytes(message.Time.Year);
            byte month = Convert.ToByte(message.Time.Month);
            byte date = Convert.ToByte(message.Time.Date);
            byte hour = Convert.ToByte(message.Time.Minute);
            byte second = Convert.ToByte(message.Time.Second);

            Array.Copy(identifier, bytes, 20); // Первые 20 байт идентификатор трекера
            Array.Copy(longitude, 0, bytes, 20, 8);
            Array.Copy(latitude, 0, bytes, 27, 8);

            return bytes;
        }
    }
}