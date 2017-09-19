using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AgilifyService.Models
{
    public class Team : Item
    {
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}