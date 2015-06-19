using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace REST.Models
{
    /// <summary>
    /// Pulse entity
    /// </summary>
    public class Pulse
    {
        /// <summary>
        /// Data id
        /// </summary>
        [BsonId(IdGenerator = typeof(BsonObjectIdGenerator))]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Pulse
        /// </summary>
        public int PulseValue { get; set; }
        /// <summary>
        /// Date of analysis
        /// </summary>
        public DateTime? DateCreated { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
    }
}