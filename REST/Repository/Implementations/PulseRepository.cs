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

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IQueryable<PulseDTO> GetAll(string userId)
        {
            return from p in db.Pulses
                   where p.ApplicationUserId.Equals(userId)
                   select new PulseDTO
                   {
                       Id = p.Id,
                       PulseValue = p.PulseValue,
                       DateCreated = p.DateCreated,
                   };
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Pulse> GetById(int id)
        {
            return await db.Pulses.FindAsync(id);
        }

        /// <summary>
        /// Adds the specified pulse.
        /// </summary>
        /// <param name="pulse">The pulse.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task Add(Pulse pulse, string userId)
        {
            pulse.ApplicationUserId = userId;
            db.Pulses.Add(pulse);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the measurements dates.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the measurements.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public IQueryable<PulseDTO> GetMeasurements(string userId, DateTime date)
        {
            return from p in db.Pulses
                where p.ApplicationUserId.Equals(userId) && p.DateCreated.Equals(date)
                select new PulseDTO
                {
                    Id = p.Id,
                    PulseValue = p.PulseValue,
                };
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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
        /// <summary>
        /// Finalizes an instance of the <see cref="PulseRepository"/> class.
        /// </summary>
        ~PulseRepository()
       {
          Dispose(false);
       }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}