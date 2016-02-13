using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDataManager : IDisposable
    {
        IApplicationUserRepository Users { get; }
        IGPSTrackerRepository GPSTrackers { get; }
        IGPSTrackerMessageRepository GPSTrackerMessages { get; }
        Task Save();
        IUserProfileRepository UserProfiles { get; }
    }
}
