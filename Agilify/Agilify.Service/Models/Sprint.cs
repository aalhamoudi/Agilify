using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgilifyService.Models
{
    public class Sprint : Item
    {
        [InverseProperty("Id")]
        public string ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<WorkItem> WorkItems { get; set; }
    }
}