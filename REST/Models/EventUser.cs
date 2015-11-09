using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace REST.Models
{
    public class EventUser
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int EventId { get; set; }
        [Required, DefaultValue(false)]
        public bool Passed { get; set; }
    }
}