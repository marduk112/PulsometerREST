using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace REST.Models
{
    public class DateDTO
    {
        /// <summary>
        /// Date of measurement
        /// </summary>
        public DateTime MeasurementDate { get; set; }
        //public int Id { get; set; }
    }

    public class DateDTOComparer : IEqualityComparer<DateDTO>
    {
        public bool Equals(DateDTO x, DateDTO y)
        {
            return x.MeasurementDate.Equals(y.MeasurementDate);
        }

        public int GetHashCode(DateDTO obj)
        {
            return obj.MeasurementDate.GetHashCode();
        }
    }
}