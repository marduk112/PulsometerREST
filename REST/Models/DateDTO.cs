using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace REST.Models
{
    /// <summary>
    /// Date Data Transfer Object
    /// </summary>
    public class DateDto
    {
        /// <summary>
        /// Date of measurement
        /// </summary>
        public DateTime MeasurementDate { get; set; }
        //public int Id { get; set; }
    }

    /// <summary>
    /// Comparer for DateDTO class
    /// </summary>
    public class DateDtoComparer : IEqualityComparer<DateDto>
    {
        /// <summary>
        /// Equalses the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool Equals(DateDto x, DateDto y)
        {
            return x.MeasurementDate.Equals(y.MeasurementDate);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(DateDto obj)
        {
            return 1;
        }
    }
}