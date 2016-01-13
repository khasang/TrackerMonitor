using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaions
{
    class EFGPSTrackerRepository : IGPSTrackerRepository
    {
        public IEnumerable<GPSTracker> GetTrackers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GPSTracker> GetTrackersByUser(Entities.ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GPSTracker> GetTrackersByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public GPSTracker GetTrackerById(string id)
        {
            throw new NotImplementedException();
        }

        public GPSTracker GetTrackerByMessage(GPSTrackerMessage message)
        {
            throw new NotImplementedException();
        }

        public void AddTracker(GPSTracker tracker)
        {
            throw new NotImplementedException();
        }

        public void DeleteTracker(GPSTracker tracker)
        {
            throw new NotImplementedException();
        }

        public void DeleteTracker(string id)
        {
            throw new NotImplementedException();
        }
    }
}
