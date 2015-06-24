using System;
using System.Collections.Generic;
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
                   where p.ApplicationUserId.Equals(userId)
                   select new PulseDTO
                   {
                       Id = p.Id,
                       DateCreated = p.DateCreated,
                       PulseValue = p.PulseValue,
                       UserName = p.ApplicationUser.UserName,
                   };
        }

        public async Task<Pulse> GetById(int id)
        {
            return await db.Pulses.FindAsync(id);
        }

        public async Task Add(Pulse pulse, string userId)
        {
            pulse.ApplicationUserId = userId;
            db.Pulses.Add(pulse);
            await db.SaveChangesAsync();
        }

        public IQueryable<DateDTO> GetMeasurementsDates(string userId)
        {
            var result = from p in db.Pulses
                where p.ApplicationUserId.Equals(userId)
                select new DateDTO
                {
                    MeasurementDate = p.DateCreated,
                    Id = p.Id,
                };
            var distinctResult = result.Distinct(new DateDTOComparer());
            return distinctResult;
        }

        public async Task<IQueryable<PulseDTO>> GetMeasurements(string userId, int id)
        {
            var date = (await db.Pulses.FindAsync(id)).DateCreated;
            var userName = db.Users.Find(userId).UserName;
            return from p in db.Pulses
                where p.ApplicationUserId.Equals(userId) && p.DateCreated.Equals(date)
                select new PulseDTO
                {
                    Id = p.Id,
                    DateCreated = p.DateCreated,
                    PulseValue = p.PulseValue,
                    UserName = userName,
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