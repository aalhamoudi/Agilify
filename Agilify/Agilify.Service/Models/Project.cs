using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;

namespace AgilifyService.Models
{
    public class Project : Item
    {
        public string Description { get; set; }

        public string TeamId { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }

        public virtual ICollection<Epic> Epics { get; set; }
        public virtual ICollection<Sprint> Sprints { get; set; }
    }
}