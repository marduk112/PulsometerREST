using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

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
        public ObjectId Id { get; set; }
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