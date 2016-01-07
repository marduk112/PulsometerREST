using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace REST.Models
{
    /// <summary>
    /// Pulse data transaction object
    /// </summary>
    public class PulseDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Pulse
        /// </summary>
        public int PulseValue { get; set; }
        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        [JsonIgnore]
        public DateTime DateCreated { get; set; }
    }
}