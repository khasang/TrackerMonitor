using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaions
{
    class EFGPSTrackerRepository : IGPSTrackerRepository
    {
        ApplicationDbContext dbContext;

        public EFGPSTrackerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<GPSTracker> GetTrackers()
        {
            return dbContext.GPSTrackers.ToList();
        }

        public IEnumerable<GPSTracker> GetTrackersByUser(ApplicationUser user)
        {
            return dbContext.GPSTrackers.Where(x => x.Owner.UserId  == user.Id);
        }

        public IEnumerable<GPSTracker> GetTrackersByUserId(string userId)
        {
            return dbContext.GPSTrackers.Where(x => x.Owner.UserId == userId);
        }

        public GPSTracker GetTrackerById(string id)
        {
            return dbContext.GPSTrackers.FirstOrDefault(x => x.Id == id);
        }

        public GPSTracker GetTrackerByMessage(GPSTrackerMessage message)
        {
            return dbContext.GPSTrackers.FirstOrDefault(x => x.Id == message.GPSTrackerId);
        }

        public void AddTracker(GPSTracker tracker)
        {
            dbContext.GPSTrackers.Add(tracker);
        }

        public void DeleteTracker(GPSTracker tracker)
        {
            dbContext.GPSTrackers.Remove(tracker);
        }

        public void DeleteTracker(string id)
        {
            dbContext.GPSTrackers.Remove(GetTrackerById(id));
        }
    }
}
