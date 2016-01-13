using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetUsers();
        ApplicationUser GetUserById(string id);
        ApplicationUser GetUserByProfile(UserProfile userProfile);
        ApplicationUser GetUserByTracker(GPSTracker tracker);
        void AddUser(ApplicationUser user);
        void DeleteUser(ApplicationUser user);
        void DeleteUser(string userId);
    }
}
