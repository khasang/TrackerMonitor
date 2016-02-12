using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity;

namespace DAL.Implementaions
{
    public class EFGPSTrackerMessageRepository : IGPSTrackerMessageRepository
    {
        ApplicationDbContext dbContext;

        public EFGPSTrackerMessageRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<GPSTrackerMessage> GetMessagesOfTracker(GPSTracker tracker)
        {
            return GetMessagesOfTracker(tracker.Id);
        }

        public IEnumerable<GPSTrackerMessage> GetMessagesOfTracker(string trackerId)
        {
            return dbContext.GPSTrackerMessages.Where(m => m.GPSTrackerId == trackerId);
        }

        public IEnumerable<GPSTrackerMessage> GetAll()
        {
            return dbContext.GPSTrackerMessages;
        }

        public GPSTrackerMessage GetById(int id)
        {
            return dbContext.GPSTrackerMessages.Find(id);
        }

        public void Add(GPSTrackerMessage item)
        {
            dbContext.GPSTrackerMessages.Add(item);
        }

        public void Update(GPSTrackerMessage item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(GPSTrackerMessage item)
        {
            Delete(item.Id);
        }

        public void Delete(int id)
        {
            GPSTrackerMessage message = GetById(id);
            if (message != null)
                dbContext.GPSTrackerMessages.Remove(message);
        }
    }
}
