using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

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
            throw new NotImplementedException();
        }

        public IEnumerable<GPSTrackerMessage> GetMessagesOfTracker(string trackerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GPSTrackerMessage> GetAll()
        {
            throw new NotImplementedException();
        }

        public GPSTrackerMessage GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(GPSTrackerMessage item)
        {
            throw new NotImplementedException();
        }

        public void Update(GPSTrackerMessage item)
        {
            throw new NotImplementedException();
        }

        public void Delete(GPSTrackerMessage item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
