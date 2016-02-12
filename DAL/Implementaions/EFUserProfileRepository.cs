using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity;

namespace DAL.Implementaions
{
    public class EFUserProfileRepository : IUserProfileRepository
    {
        ApplicationDbContext dbContext;

        public EFUserProfileRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserProfile GetByUser(ApplicationUser user)
        {
            return GetByUserId(user.Id);
        }

        public UserProfile GetByUserId(string userId)
        {
            return dbContext.UserProfiles.Find(userId);
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return dbContext.UserProfiles;
        }

        public UserProfile GetById(string id)
        {
            return dbContext.UserProfiles.Find(id);
        }

        public void Add(UserProfile item)
        {
            dbContext.UserProfiles.Add(item);
        }

        public void Update(UserProfile item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(UserProfile item)
        {
            Delete(item.UserId);
        }

        public void Delete(string id)
        {
            UserProfile profile = GetById(id);
            if (profile != null)
                dbContext.UserProfiles.Remove(profile);
        }
    }
}
