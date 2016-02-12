using DAL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaions
{
    public class EFApplicationUserRepository : IApplicationUserRepository
    {
        ApplicationDbContext dbContext;

        public EFApplicationUserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ApplicationUser GetUserByProfile(UserProfile userProfile)
        {
            return dbContext.Users.Find(userProfile.UserId);
        }

        public ApplicationUser GetUserByTracker(GPSTracker tracker)
        {
            return dbContext.Users.Find(tracker.OwnerId);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return dbContext.Users;
        }

        public ApplicationUser GetById(string id)
        {
            return dbContext.Users.Find(id);
        }

        public void Add(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
