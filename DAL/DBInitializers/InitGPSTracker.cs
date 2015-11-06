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
            GPSTracker tracker = new GPSTracker()
            {
                Name = "NewTracker",
                Owner = user
            };

            context.GPSTrackers.Add(tracker);
            context.SaveChanges();
        }
    }
}
