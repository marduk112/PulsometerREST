using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;

namespace REST.Models
{
    public class Pulse
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public int PulseValue { get; set; }
        [Required]
        public DateTime DateCreated
        {
            get
            {
                return _dateCreated.HasValue
                   ? _dateCreated.Value
                   : DateTimeOffset.UtcNow.UtcDateTime;
            }

            set { _dateCreated = value; }
        }
        [Required, ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        private DateTime? _dateCreated;
    }
}