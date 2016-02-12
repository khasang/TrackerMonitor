using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGPSTrackerMessageRepository
    {
        IEnumerable<GPSTrackerMessage> GetMessages();
        GPSTrackerMessage GetMessageById(int id);
        IEnumerable<GPSTrackerMessage> GetMessagesOfTracker(GPSTracker tracker);
        IEnumerable<GPSTrackerMessage> GetMessagesOfTracker(string trackerId);
        void AddMessage(GPSTrackerMessage message);
        void DeleteMessage(GPSTrackerMessage message);
        void DeleteMessage(int messageId);
    }
}
