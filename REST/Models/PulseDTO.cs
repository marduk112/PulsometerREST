using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class PulseDTO
    {
        public int Id { get; set; }
        public int PulseValue { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
    }
}