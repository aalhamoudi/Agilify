using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgilifyService.Models
{
    public class Epic : PortfolioBacklogItem
    {
        [InverseProperty("Id")]
        public string ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public virtual ICollection<Feature> Features { get; set; }
    }
}