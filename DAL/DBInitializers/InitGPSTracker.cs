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
            Random r = new Random();
            var user = context.Users.FirstOrDefault(x => x.Email == "admin@admin.com");

            for (int i = 0; i < 100; i++)
            {
                GPSTracker track = new GPSTracker()
                {
                    Id = Guid.NewGuid().ToString().Substring(0, 10),
                    Name = $"Трекер №{i}",
                    Owner = user.UserProfile,
                    IsActive = r.Next(5) > 2 ? true : false //Рандомно задаем активность трекера.
                };

                context.GPSTrackers.Add(track);
            }
            context.SaveChanges();
        }
    }
}
