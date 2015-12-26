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
                GPSTrackerMessage GpsMessage1 = new GPSTrackerMessage()
                {
                    Time = new DateTime(2010, 1, 1),
                    Latitude = 0,
                    Longitude = 0,
                    GPSTrackerId = "111111"
                };
                context.GPSTrackerMessages.Add(GpsMessage1);

            GPSTrackerMessage GpsMessage2 = new GPSTrackerMessage()
            {
                Time = new DateTime(2011, 4, 12),
                Latitude = 90,
                Longitude = 90,
                GPSTrackerId = "111111"
            };
            context.GPSTrackerMessages.Add(GpsMessage2);

            GPSTrackerMessage GpsMessage3 = new GPSTrackerMessage()
            {
                Time = new DateTime(2012, 2, 23),
                Latitude = 23,
                Longitude = 87,
                GPSTrackerId = "111111"
            };
            context.GPSTrackerMessages.Add(GpsMessage3);

            GPSTrackerMessage GpsMessage4 = new GPSTrackerMessage()
            {
                Time = new DateTime(2012, 5, 6),
                Latitude = 23,
                Longitude = 44,
                GPSTrackerId = "222222"
            };
            context.GPSTrackerMessages.Add(GpsMessage4);

            GPSTrackerMessage GpsMessage5 = new GPSTrackerMessage()
            {
                Time = new DateTime(2014, 4, 2),
                Latitude = 24,
                Longitude = 73,
                GPSTrackerId = "222222"
            };
            context.GPSTrackerMessages.Add(GpsMessage5);

            GPSTrackerMessage GpsMessage6 = new GPSTrackerMessage()
            {
                Time = new DateTime(2013, 11, 12),
                Latitude = 64,
                Longitude = 23,
                GPSTrackerId = "11223344556677"
            };
            context.GPSTrackerMessages.Add(GpsMessage6);

            context.SaveChanges();
        }
    }
}
