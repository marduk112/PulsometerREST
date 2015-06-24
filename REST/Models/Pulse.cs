using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;

namespace REST.Models
{
    /// <summary>
    /// Pulse entity
    /// </summary>
    public class Pulse
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Pulse
        /// </summary>
        [Required]
        public int PulseValue { get; set; }
        /// <summary>
        /// Date of analysis
        /// </summary>
        [Required]
        public DateTime DateCreated
        {
            get
            {
                return _dateCreated.HasValue
                   ? _dateCreated.Value
                   : DateTimeOffset.UtcNow.UtcDateTime;
            }

            set { _dateCreated = value; }
        }
        /// <summary>
        /// Foreign key to AppNetUsers table
        /// </summary>
        public string ApplicationUserId { get; set; }
        /// <summary>
        /// Atribute for get data about user
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        private DateTime? _dateCreated;
    }
}