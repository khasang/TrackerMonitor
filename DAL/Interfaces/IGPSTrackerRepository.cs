using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGPSTrackerRepository : IRepository<GPSTracker, string>
    {
        IEnumerable<GPSTracker> GetTrackersByUser(ApplicationUser user);
        IEnumerable<GPSTracker> GetTrackersByUserId(string userId);
        GPSTracker GetTrackerByMessage(GPSTrackerMessage message);
        GPSTracker GetTrackerByMessageId(int messageId);
    }
}
