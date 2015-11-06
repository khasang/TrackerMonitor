using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBInitializers
{
    public class InitGPSTracker : InitializationDB
    {
        public override void Initialization(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == "admin@admin.com");

            GPSTracker tracker1 = new GPSTracker()
            {
                Name = "Tracker1",
                Owner = user
            };

            GPSTracker tracker2 = new GPSTracker()
            {
                Name = "Tracker2",
                Owner = user
            };

            context.GPSTrackers.Add(tracker1);
            context.GPSTrackers.Add(tracker2);

            context.SaveChanges();
        }
    }
}
