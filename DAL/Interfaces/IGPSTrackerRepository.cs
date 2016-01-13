using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    interface IGPSTrackerRepository
    {
        IEnumerable<GPSTracker> GetTrackers();
        IEnumerable<GPSTracker> GetTrackersByUser(ApplicationUser user);
        IEnumerable<GPSTracker> GetTrackersByUserId(string userId);
        GPSTracker GetTrackerById(string id);
        GPSTracker GetTrackerByMessage(GPSTrackerMessage message);
        void AddTracker(GPSTracker tracker);
        void DeleteTracker(GPSTracker tracker);
        void DeleteTracker(string id);
    }
}
