using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace REST.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the event users.
        /// </summary>
        /// <value>
        /// The event users.
        /// </value>
        [JsonIgnore]
        public virtual ICollection<EventUser> EventUsers { get; set; } 
        /// <summary>
        /// Generates the user identity asynchronous.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="authenticationType">Type of the authentication.</param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    /// <summary>
    /// DB context of application
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Sets connectionstring
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        /// <summary>
        /// Creates new object of ApplicationDbContext class
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*modelBuilder.Entity<EventUser>().HasKey(c => new { c.Id });
            modelBuilder.Entity<Event>()
                .HasMany(c => c.EventUsers)
                .WithRequired()
                .HasForeignKey(c => c.EventId);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.EventUsers)
                .WithRequired()
                .HasForeignKey(c => c.ApplicationUserId);*/
            
        }

        /// <summary>
        /// Pulses table in database
        /// </summary>
        public System.Data.Entity.DbSet<REST.Models.Pulse> Pulses { get; set; }

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public System.Data.Entity.DbSet<REST.Models.Event> Events { get; set; }
        /// <summary>
        /// Gets or sets the event users.
        /// </summary>
        /// <value>
        /// The event users.
        /// </value>
        public DbSet<EventUser> EventUsers { get; set; } 
    }
}