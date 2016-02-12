using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public IEnumerable<GPSTracker> GetTrackersByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public GPSTracker GetTrackerByMessage(GPSTrackerMessage message)
        {
            throw new NotImplementedException();
        }

        public GPSTracker GetTrackerByMessageId(int messageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GPSTracker> GetAll()
        {
            throw new NotImplementedException();
        }

        public GPSTracker GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(GPSTracker item)
        {
            throw new NotImplementedException();
        }

        public void Update(GPSTracker item)
        {
            throw new NotImplementedException();
        }

        public void Delete(GPSTracker item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
