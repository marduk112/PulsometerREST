using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using REST.Models;
using REST.Repository.Interfaces;

namespace REST.Controllers
{
    /// <summary>
    /// Pulses API Controller
    /// </summary>
    [Authorize]
    public class PulsesController : ApiController
    {
        private IPulseRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PulsesController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public PulsesController(IPulseRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Pulses
        /// <summary>
        /// Get all pulses for user from database
        /// </summary>
        /// <returns></returns>
        public IQueryable<PulseDTO> GetPulses()
        {
            return _repository.GetAll(User.Identity.GetUserId());
        }

        /// <summary>
        /// Gets the measurements.
        /// </summary>
        /// <returns></returns>
        [Route("api/GetMeasurementsDates"), HttpGet]
        public IQueryable<DateDto> GetMeasurementsDates()
        {
           return  _repository.GetMeasurementsDates(User.Identity.GetUserId());
        }

        /// <summary>
        /// Gets the measurements with date.
        /// </summary>
        /// <param name="measurementDate">The measurement date.</param>
        /// <returns></returns>
        [Route("api/GetMeasurementsWithDate"), HttpGet]
        [ResponseType(typeof(IQueryable<PulseDTO>))]
        public IHttpActionResult GetMeasurementsWithDate(DateTime measurementDate)
        {
            var pulses = _repository.GetMeasurements(User.Identity.GetUserId(), measurementDate);
            return Ok(pulses);
        }

        // POST: api/Pulses
        /// <summary>
        /// Add new data(pulse) to database
        /// </summary>
        /// <param name="pulse"></param>
        /// <returns></returns>
        [ResponseType(typeof(Pulse))]
        public async Task<IHttpActionResult> PostPulse(Pulse pulse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Add(pulse, User.Identity.GetUserId());

            return CreatedAtRoute("DefaultApi", new { id = pulse.Id }, pulse);
        }

        // DELETE: api/Pulses/5
        /// <summary>
        /// Delete data(pulse) from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /*[ResponseType(typeof(Pulse))]
        public async Task<IHttpActionResult> DeletePulse(int id)
        {
            Pulse pulse = await db.Pulses.FindAsync(id);
            if (pulse == null)
            {
                return NotFound();
            }

            db.Pulses.Remove(pulse);
            await db.SaveChangesAsync();

            return Ok(pulse);
        }*/
    }
}