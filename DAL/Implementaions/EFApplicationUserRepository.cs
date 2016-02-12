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
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserByTracker(GPSTracker tracker)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(ApplicationUser item)
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
