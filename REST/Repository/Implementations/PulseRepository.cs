using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using REST.Models;
using REST.Repository.Interfaces;

namespace REST.Repository.Implementations
{
    public class PulseRepository : IPulseRepository, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private bool _disposed = false;

        public IQueryable<PulseDTO> GetAll(string userId)
        {
            return from p in db.Pulses
                   where p.IdentityUserId.Equals(userId)
                   select new PulseDTO
                   {
                       Id = p.Id,
                       PulseValue = p.PulseValue,
                       DateCreated = p.DateCreated,
                   };
        }

        public async Task<Pulse> GetById(int id)
        {
            return await db.Pulses.FindAsync(id);
        }

        public async Task Add(Pulse pulse, string userId)
        {
            pulse.IdentityUserId = userId;
            db.Pulses.Add(pulse);
            await db.SaveChangesAsync();
        }

        public IQueryable<DateDto> GetMeasurementsDates(string userId)
        {
            return (from p in db.Pulses
                where p.IdentityUserId.Equals(userId)
                select new DateDto
                {
                    MeasurementDate = p.DateCreated,
                    //Id = p.Id,
                }).Distinct();
        }

        public IQueryable<PulseDTO> GetMeasurements(string userId, DateTime date)
        {
            return from p in db.Pulses
                where p.IdentityUserId.Equals(userId) && p.DateCreated == date
                select new PulseDTO
                {
                    Id = p.Id,
                    PulseValue = p.PulseValue,
                };
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }
            db.Dispose();
            // Free any unmanaged objects here.
            //
            _disposed = true;
        }
       ~PulseRepository()
       {
          Dispose(false);
       }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}