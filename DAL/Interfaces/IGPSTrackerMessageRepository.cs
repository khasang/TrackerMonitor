using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGPSTrackerMessageRepository : IRepository<GPSTrackerMessage, int>
    {
        IEnumerable<GPSTrackerMessage> GetMessagesOfTracker(GPSTracker tracker);
        IEnumerable<GPSTrackerMessage> GetMessagesOfTracker(string trackerId);
    }
}
