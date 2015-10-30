using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json;

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
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Foreign key to AppNetUsers table
        /// </summary>
        [JsonIgnore]
        public string IdentityUserId { get; set; }
        /// <summary>
        /// Atribute for get data about user
        /// </summary>
        [JsonIgnore]
        public virtual IdentityUser IdentityUser { get; set; }
    }
}