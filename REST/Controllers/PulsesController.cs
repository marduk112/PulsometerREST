using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using REST.Models;
using WebGrease.Css.Extensions;

namespace REST.Controllers
{
    /// <summary>
    /// Pulses API Controller
    /// </summary>
    public class PulsesController : ApiController
    {
        private readonly MongoClient _client = new MongoClient(ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString);
        private const string DatabaseName = "appharbor_b2bp7sht";
        // GET: api/Pulses
        /// <summary>
        /// Get all pulses for user from database
        /// </summary>
        /// <returns></returns>
        public IQueryable<Pulse> GetPulses()
        {
            var server = _client.GetServer();
            var database = server.GetDatabase(DatabaseName);
            var collection = database.GetCollection<Pulse>(User.Identity.GetUserName() + "Pulse");
            return collection.AsQueryable();
            /*if (HttpContext.Current == null || HttpContext.Current.User == null ||
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
                   };*/
            return null;
        }

        // GET: api/Pulses/5
        /// <summary>
        /// Get pulse information with appropriate id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Pulse))]
        public async Task<IHttpActionResult> GetPulse(int id, [FromBody] ObjectId Id)
        {
            var pulse = await GetPulseAsync(id, Id);
            //Pulse pulse = await db.Pulses.FindAsync(id);
            //if (pulse == null)
            //{
            //    return NotFound();
            //}
            return Ok(pulse);
        }

        private Task<Pulse> GetPulseAsync(int id, ObjectId Id)
        {
            var server = _client.GetServer();
            var database = server.GetDatabase(DatabaseName);
            var collection = database.GetCollection<Pulse>(User.Identity.GetUserName() + "Pulse");
            var query = Query.EQ("Id", Id);
            var pulse = collection.FindOne(query);
            return Task.FromResult(pulse);
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
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != pulse.Id)
            //{
            //    return BadRequest();
            //}

            //pulse.ApplicationUserId = User.Identity.GetUserId();
            //db.Entry(pulse).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PulseExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

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
            var server = _client.GetServer();
            var database = server.GetDatabase(DatabaseName);
            var collection = database.GetCollection<Pulse>(User.Identity.Name + "Pulse");
            pulse.Id = new ObjectId();
            collection.Insert(pulse);
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //pulse.ApplicationUserId = User.Identity.GetUserId();
            //db.Pulses.Add(pulse);
            //await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pulse.Id}, pulse);
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
            var server = _client.GetServer();
            var database = server.GetDatabase(DatabaseName);
            //Pulse pulse = await db.Pulses.FindAsync(id);
            //if (pulse == null)
            //{
            //    return NotFound();
            //}
            
            //db.Pulses.Remove(pulse);
            //await db.SaveChangesAsync();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PulseExists(int id)
        {
            return true;
        }
    }
}