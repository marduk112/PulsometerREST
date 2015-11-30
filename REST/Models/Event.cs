using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace REST.Models
{
    public class Event
    {
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string CreatorId { get; set; }
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
        [Required, Index(IsUnique = true), StringLength(450)]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the event description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        /// <summary>
        /// Gets or sets the start date time event.
        /// </summary>
        /// <value>
        /// The start date time event.
        /// </value>
        [Required]
        public DateTime StartDateTimeEvent { get; set; }
        /// <summary>
        /// Gets or sets the duration of the event.
        /// </summary>
        /// <value>
        /// The duration of the event.
        /// </value>
        [Required]
        public int EventDuration { get; set; }
        /// <summary>
        /// Gets or sets the duration (in minutes).
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [Required]
        public int Duration { get; set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        [Required]
        public Target Target { get; set; }
        /// <summary>
        /// Gets or sets the event users.
        /// </summary>
        /// <value>
        /// The event users.
        /// </value>
        [JsonIgnore]
        public virtual ICollection<EventUser> EventUsers { get; set; }
    }

    public enum Target
    {
        Min,
        Max,
        Between,
    }
}