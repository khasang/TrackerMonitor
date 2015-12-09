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

            context.GPSTrackers.Add(new GPSTracker()
            {
                Id = "111111",
                Name = "Tracker1",
                Owner = user.UserProfile
            });

            context.GPSTrackers.Add(new GPSTracker()
            {
                Id = "222222",
                Name = "Tracker2",
                Owner = user.UserProfile
            });

            context.GPSTrackers.Add(new GPSTracker()
            {
                Id = "11223344556677",
                Name = "Tracker3",
                Owner = user.UserProfile
            });

            context.SaveChanges();
        }
    }
}
