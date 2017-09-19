using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgilifyService.Models
{
    public class Feature : PortfolioBacklogItem
    {
        [InverseProperty("Id")]
        public string EpicId { get; set; }

        [ForeignKey("EpicId")]
        public Epic Epic { get; set; }
    }
}