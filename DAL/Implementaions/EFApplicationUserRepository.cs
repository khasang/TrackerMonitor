using DAL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaions
{
    class EFApplicationUserRepository : IApplicationUserRepository
    {
        public IEnumerable<ApplicationUser> GetUsers()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserByProfile(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserByTracker(GPSTracker tracker)
        {
            throw new NotImplementedException();
        }

        public void AddUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
