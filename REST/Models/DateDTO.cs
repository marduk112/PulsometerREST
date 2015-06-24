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
        public bool Equals(DateDto x, DateDto y)
        {
            return x.MeasurementDate.Equals(y.MeasurementDate);
        }

        public int GetHashCode(DateDto obj)
        {
            return 1;
        }
    }
}