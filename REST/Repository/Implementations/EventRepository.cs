using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using REST.Models;
using REST.Repository.Interfaces;

namespace REST.Repository.Implementations
{
    public class EventRepository : IEventRepository, IDisposable
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private bool _disposed = false;

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Event> GetEvents()
        {
            return _db.Events;
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        public async Task<Event> GetEvent(int eventId)
        {
            var @event = await _db.Events.FindAsync(eventId);
            return @event;
        }

        /// <summary>
        /// Joins to event.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="event">The event.</param>
        /// <returns></returns>
        public async Task JoinToEvent(string userId, Event @event)
        {
            if (!_db.EventUsers.Any(x => x.ApplicationUserId.Equals(userId) && x.EventId == @event.Id))
            {
                var eventUser = new EventUser
                {
                    ApplicationUserId = userId,
                    EventId = @event.Id,
                    Passed = false,
                };
                _db.EventUsers.Add(eventUser);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Adds the new event.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <returns></returns>
        public async Task AddNewEvent(Event @event)
        {
            _db.Events.Add(@event);
            await _db.SaveChangesAsync();
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
            _db.Dispose();
            // Free any unmanaged objects here.
            //
            _disposed = true;
        }
        /// <summary>
        /// Finalizes an instance of the <see cref="EventRepository"/> class.
        /// </summary>
        ~EventRepository()
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