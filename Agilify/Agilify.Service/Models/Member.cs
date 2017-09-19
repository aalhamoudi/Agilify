using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AgilifyService.Models
{
    public class Member : Item
    {
        public string Email { get; set; }
        public string Image { get; set; }

        [JsonIgnore]
        public virtual ICollection<Team> Teams { get; set; }

    }
}