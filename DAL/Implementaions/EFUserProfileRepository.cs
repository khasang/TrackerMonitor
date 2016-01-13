using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Implementaions
{
    class EFUserProfileRepository : IUserProfileRepository
    {
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            throw new NotImplementedException();
        }

        public UserProfile GetUserProfile(string userProfileId)
        {
            throw new NotImplementedException();
        }

        public UserProfile GetUserProfile(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void AddUserProfile(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserProfile(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserProfile(string userProfileId)
        {
            throw new NotImplementedException();
        }
    }
}
