using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Implementaions
{
    class EFGPSTrackerMessageRepository : IGPSTrackerMessageRepository
    {
        ApplicationDbContext dbContext;

        public EFGPSTrackerMessageRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<GPSTrackerMessage> GetMessages()
        {
            throw new NotImplementedException();
        }

        public GPSTrackerMessage GetMessageById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GPSTrackerMessage> GetMessagesOfTracker(GPSTracker tracker)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GPSTrackerMessage> GetMessagesOfTracker(string trackerId)
        {
            throw new NotImplementedException();
        }

        public void AddMessage(GPSTrackerMessage message)
        {
            throw new NotImplementedException();
        }

        public void DeleteMessage(GPSTrackerMessage message)
        {
            throw new NotImplementedException();
        }

        public void DeleteMessage(int messageId)
        {
            throw new NotImplementedException();
        }
    }
}
