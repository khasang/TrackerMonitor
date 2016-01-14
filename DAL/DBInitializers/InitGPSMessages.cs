using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBInitializers
{
    public class InitGPSMessages : InitializationDB
    {
        public override void Initialization(ApplicationDbContext context)
        {
            Random r = new Random();
            var gpsTracker = context.GPSTrackers.FirstOrDefault(x => x.Name == "Трекер №0");

            for (int i = 0; i < 100; i++)
            {
                GPSTrackerMessage gpsMessage = new GPSTrackerMessage()
                {
                    Time = new DateTime(2010, 1, 1),
                    Latitude = 0,
                    Longitude = 0,
                    GPSTrackerId = gpsTracker.Id,
                };

                context.GPSTrackerMessages.Add(gpsMessage);
            }
            context.SaveChanges();
        }
    }
}
