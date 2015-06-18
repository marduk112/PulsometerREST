﻿using System;
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

namespace REST.Controllers
{
    /// <summary>
    /// Pulses API Controller
    /// </summary>
    public class PulsesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Pulses
        /// <summary>
        /// Get all pulses for user from database
        /// </summary>
        /// <returns></returns>
        public IQueryable<PulseDTO> GetPulses()
        {
            if (HttpContext.Current == null || HttpContext.Current.User == null ||
                HttpContext.Current.User.Identity.Name == null) 
                return from p in db.Pulses
                       select new PulseDTO
                       {
                           Id = p.Id,
                           DateCreated = p.DateCreated,
                           PulseValue = p.PulseValue,
                           UserName = p.ApplicationUser.UserName,
                       };
            var userId = User.Identity.GetUserId();
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

        // GET: api/Pulses/5
        /// <summary>
        /// Get pulse information with appropriate id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Pulse))]
        public async Task<IHttpActionResult> GetPulse(int id)
        {
            Pulse pulse = await db.Pulses.FindAsync(id);
            if (pulse == null)
            {
                return NotFound();
            }

            return Ok(pulse);
        }

        // PUT: api/Pulses/5
        /// <summary>
        /// Modify existing data(pulse) from/to database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pulse"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPulse(int id, Pulse pulse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pulse.Id)
            {
                return BadRequest();
            }

            pulse.ApplicationUserId = User.Identity.GetUserId();
            db.Entry(pulse).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PulseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
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

            pulse.ApplicationUserId = User.Identity.GetUserId();
            db.Pulses.Add(pulse);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pulse.Id }, pulse);
        }

        // DELETE: api/Pulses/5
        /// <summary>
        /// Delete data(pulse) from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Pulse))]
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
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PulseExists(int id)
        {
            return db.Pulses.Count(e => e.Id == id) > 0;
        }
    }
}