using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace DAL.Logic
{
    public class GPSTrackerMessageConverter
    {
        private static readonly int longitudeLength = 20;
        private static readonly int latitudeLength = 19;
        /// <summary>
        /// Метод для парсинга сообщения от маячка в объект сообщения
        /// </summary>
        /// <param name="bytes">сообщение от маячка</param>
        /// <returns>объект сообщения</returns>
        public static GPSTrackerMessage BytesToMessage(byte[] bytes)
        {
            GPSTrackerMessage message = new GPSTrackerMessage();
            int currentIndex = 0;

            // парсинг длины идентификатора маячка
            int idLength = Convert.ToInt32(bytes[currentIndex]);
            currentIndex++;

            // Парсинг идентификатора маячка
            message.GPSTrackerId = Encoding.UTF8.GetString(bytes.Skip(currentIndex).Take(idLength).ToArray<byte>());
            currentIndex += idLength;

            // Парсинг долготы
            byte[] longitudeBytes = new byte[longitudeLength];
            Array.Copy(bytes, currentIndex, longitudeBytes, 0, longitudeLength);
            Console.WriteLine(Encoding.UTF8.GetString(longitudeBytes));
            message.Longitude = Double.Parse(Encoding.UTF8.GetString(longitudeBytes), NumberStyles.Number);
            currentIndex += longitudeLength;

            // Парсинг широты
            byte[] latitudeBytes = new byte[latitudeLength];
            Array.Copy(bytes, currentIndex, latitudeBytes, 0, latitudeLength);
            Console.WriteLine(Encoding.UTF8.GetString(latitudeBytes));
            message.Latitude = Double.Parse(Encoding.UTF8.GetString(latitudeBytes), NumberStyles.Number);
            currentIndex += latitudeLength;

            int year = (int)BitConverter.ToInt16(new byte[] { bytes[currentIndex], bytes[currentIndex + 1] }, 0);
            currentIndex += 2;

            int month = Convert.ToInt32(bytes[currentIndex]);
            currentIndex++;

            int day = Convert.ToInt32(bytes[currentIndex]);
            currentIndex++;

            int hour = Convert.ToInt32(bytes[currentIndex]);
            currentIndex++;

            int minute = Convert.ToInt32(bytes[currentIndex]);
            currentIndex++;

            int second = Convert.ToInt32(bytes[currentIndex]);
            currentIndex++;

            message.Time = new DateTime(year, month, day,hour, minute, second);

            return message;
        }

        public static byte[] MessageToBytes(GPSTrackerMessage message)
        {
            List<byte> bytes = new List<byte>();
            int idLength = message.GPSTrackerId.Length; // Длина идентификатора маячка
            bytes.Add(Convert.ToByte(idLength)); // Кодировка длины маячка

            bytes.AddRange(Encoding.UTF8.GetBytes(message.GPSTrackerId)); // Кодировка идентификатора маячка

            // Кодировка долготы маячка
            bytes.AddRange(Encoding.UTF8.GetBytes(message.Longitude.ToString("+000.000000000000000;-000.000000000000000")));

            // Кодировка широты маячка
            bytes.AddRange(Encoding.UTF8.GetBytes(message.Latitude.ToString("+00.000000000000000;-00.000000000000000")));

            bytes.AddRange(BitConverter.GetBytes((ushort)message.Time.Year)); // Год

            bytes.Add(Convert.ToByte(message.Time.Month));         // Месяц
            bytes.Add(Convert.ToByte(message.Time.Day));           // Дата
            bytes.Add(Convert.ToByte(message.Time.Hour));          // Часы
            bytes.Add(Convert.ToByte(message.Time.Minute));        // Минуты
            bytes.Add(Convert.ToByte(message.Time.Second));        // Секунды

            return bytes.ToArray();
        }
    }
}