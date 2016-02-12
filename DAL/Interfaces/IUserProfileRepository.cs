using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserProfileRepository : IRepository<UserProfile, string>
    {
        UserProfile GetUserProfile(string userProfileId);
        UserProfile GetUserProfile(ApplicationUser user);
        void AddUserProfile(UserProfile userProfile);
    }
}
