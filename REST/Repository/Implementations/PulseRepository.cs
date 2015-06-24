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
        private static readonly IEqualityComparer<DateDto> EqualityComparer = new DateDtoComparer();

        public IQueryable<PulseDTO> GetAll(string userId)
        {
            return from p in db.Pulses
                   where p.ApplicationUserId.Equals(userId)
                   select new PulseDTO
                   {
                       Id = p.Id,
                       PulseValue = p.PulseValue,
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

        public IQueryable<DateDto> GetMeasurementsDates(string userId)
        {
            return (from p in db.Pulses
                where p.ApplicationUserId.Equals(userId)
                select new DateDto
                {
                    MeasurementDate = p.DateCreated,
                    //Id = p.Id,
                }).Distinct();
        }

        public async Task<IQueryable<PulseDTO>> GetMeasurements(string userId, DateDto date)
        {
            var userName = db.Users.Find(userId).UserName;
            return from p in db.Pulses
                where p.ApplicationUserId.Equals(userId) && p.DateCreated.Equals(date.MeasurementDate)
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