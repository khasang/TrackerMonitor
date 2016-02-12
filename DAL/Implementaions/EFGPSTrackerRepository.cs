using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaions
{
    public class EFGPSTrackerRepository : IGPSTrackerRepository
    {
        ApplicationDbContext dbContext;

        public EFGPSTrackerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<GPSTracker> GetTrackersByUser(ApplicationUser user)
        {
            return dbContext.GPSTrackers.Where(t => t.OwnerId == user.Id);
        }

        public IEnumerable<GPSTracker> GetTrackersByUserId(string userId)
        {
            return dbContext.GPSTrackers.Where(t => t.OwnerId == userId);
        }

        public GPSTracker GetTrackerByMessage(GPSTrackerMessage message)
        {
            return dbContext.GPSTrackers.FirstOrDefault(t => t.Id == message.GPSTrackerId);
        }

        public GPSTracker GetTrackerByMessageId(int messageId)
        {
            return dbContext.GPSTrackers.FirstOrDefault(t => t.Id == dbContext.GPSTrackerMessages.Find(messageId).GPSTrackerId);
        }

        public IEnumerable<GPSTracker> GetAll()
        {
            return dbContext.GPSTrackers;
        }

        public GPSTracker GetById(string id)
        {
            return dbContext.GPSTrackers.Find(id);
        }

        public void Add(GPSTracker item)
        {
            dbContext.GPSTrackers.Add(item);
        }

        public void Update(GPSTracker item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(GPSTracker item)
        {
            Delete(item.Id);
        }

        public void Delete(string id)
        {
            GPSTracker tracker = GetById(id);
            if (tracker != null)
                dbContext.GPSTrackers.Remove(tracker);
        }
    }
}
