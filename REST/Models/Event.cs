using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace REST.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Creator { get; set; }
        [Required]
        public string Name { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        [Required]
        public Target Target { get; set; }
        public virtual ICollection<IdentityUser> Members { get; set; }
    }

    public enum Target
    {
        Min,
        Max,
        Between,
    }
}