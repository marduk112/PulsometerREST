using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using REST.Models;
using REST.Repository.Interfaces;

namespace REST.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class EventsController : ApiController
    {
        private IEventRepository _repository;

        public EventsController(IEventRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Events
        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Event> GetEvents()
        {
            return _repository.GetEvents().AsQueryable();
        }

        // GET: api/Events/5
        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> GetEvent(int id)
        {
            var @event = await _repository.GetEvent(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        /// <summary>
        /// Joins to event.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("api/JoinToEvent"), HttpPost]
        public async Task<IHttpActionResult> JoinToEvent(int id)
        {
            Event @event;
            using (var db = new ApplicationDbContext())
            {
                @event = await db.Events.FindAsync(id);
                if (@event == null)
                    return BadRequest("Event with id " + id + " doesn't exist");
            }
            await _repository.JoinToEvent(User.Identity.GetUserId(), @event);
            return CreatedAtRoute("DefaultApi", new { id = @event.Id }, @event);
        }

        // PUT: api/Events/5
        /*[ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            db.Entry(@event).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }*/

        // POST: api/Events
        /// <summary>
        /// Posts the event.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <returns></returns>
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            @event.CreatorId = User.Identity.GetUserId();
            await _repository.AddNewEvent(@event);

            return CreatedAtRoute("DefaultApi", new { id = @event.Id }, @event);
        }

        /// <summary>
        /// Sets the event as passed.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IHttpActionResult> SetEventAsPassed(int eventId)
        {
            await _repository.SetEventAsSuccess(eventId);
            return Ok();
        }

        // DELETE: api/Events/5
        /*[ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> DeleteEvent(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Events.Remove(@event);
            await db.SaveChangesAsync();

            return Ok(@event);
        }*/
    }
}