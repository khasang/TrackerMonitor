using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Implementaions
{
    public class EFUserProfileRepository : IUserProfileRepository
    {
        ApplicationDbContext dbContext;

        public EFUserProfileRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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

        public IEnumerable<UserProfile> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserProfile GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(UserProfile item)
        {
            throw new NotImplementedException();
        }

        public void Update(UserProfile item)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserProfile item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
