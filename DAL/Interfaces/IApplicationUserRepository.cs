using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser, string>
    {
        ApplicationUser GetUserByProfile(UserProfile userProfile);
        ApplicationUser GetUserByTracker(GPSTracker tracker);
    }
}
