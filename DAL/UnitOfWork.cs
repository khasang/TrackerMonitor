using DAL.Implementaions;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        ApplicationDbContext dbContext;

        IApplicationUserRepository applicationUser;
        IGPSTrackerMessageRepository gpsTrackerMessage;
        IGPSTrackerRepository gpsTracker;
        IUserProfileRepository userProfile;

        public UnitOfWork(string connectionString)
        {
            dbContext = new ApplicationDbContext(connectionString);
        }

        public IApplicationUserRepository ApplicationUser
        {
            get
            {
                if (applicationUser == null)
                    applicationUser = new EFApplicationUserRepository(dbContext);
                return applicationUser;
            }
        }

        public IGPSTrackerMessageRepository GPSTrackerMessage
        {
            get
            {
                if (gpsTrackerMessage == null)
                    gpsTrackerMessage = new EFGPSTrackerMessageRepository(dbContext);
                return gpsTrackerMessage;
            }
        }

        public IGPSTrackerRepository GPSTracker
        {
            get
            {
                if (gpsTracker == null)
                    gpsTracker = new EFGPSTrackerRepository(dbContext);
                return gpsTracker;
            }
        }

        public IUserProfileRepository UserProfile
        {
            get
            {
                if (userProfile == null)
                    userProfile = new EFUserProfileRepository(dbContext);
                return userProfile;
            }
        }

        public async Task Save()
        {
            bool success = false;

            do
            {
                try
                {
                    await dbContext.SaveChangesAsync();

                    success = true;
                }
                catch (DbUpdateException ex)
                {
                    foreach (DbEntityEntry entry in ex.Entries.Where(x => x.State == EntityState.Modified))
                    {
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                    }

                    foreach (DbEntityEntry entry in ex.Entries.Where(x => x.State == EntityState.Added))
                    {
                        entry.State = EntityState.Detached;
                    }

                    foreach (DbEntityEntry entry in ex.Entries.Where(x => x.State == EntityState.Deleted))
                    {
                        entry.State = EntityState.Unchanged;
                    }
                }

            } while (!success);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
