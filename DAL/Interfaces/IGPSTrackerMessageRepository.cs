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
        IEnumerable<GPSTrackerMessage> GetMessagesByTracker(GPSTracker tracker);
        IEnumerable<GPSTrackerMessage> GetMessagesByTrackerId(string trackerId);
    }
}
