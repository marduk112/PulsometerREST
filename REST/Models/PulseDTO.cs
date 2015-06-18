using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

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
        //public ObjectId Id { get; set; }
        //public int Serial { get; set; }
        /// <summary>
        /// Pulse
        /// </summary>
        public int PulseValue { get; set; }
        /// <summary>
        /// Date of analysis
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
    }
}