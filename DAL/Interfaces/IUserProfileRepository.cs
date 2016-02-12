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
        UserProfile GetByUser(ApplicationUser user);
        UserProfile GetByUserId(string userId);
    }
}
