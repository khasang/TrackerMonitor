using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    interface IUserProfileRepository
    {
        IEnumerable<UserProfile> GetUserProfiles();
        UserProfile GetUserProfile(string userProfileId);
        UserProfile GetUserProfile(ApplicationUser user);
        void AddUserProfile(UserProfile userProfile);
        void DeleteUserProfile(UserProfile userProfile);
        void DeleteUserProfile(string userProfileId);
    }
}
