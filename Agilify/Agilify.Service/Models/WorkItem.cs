using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgilifyService.Models
{
    public class WorkItem : ProductBacklogItem
    {
        public string Type { get; set; }

        [InverseProperty("Id")]
        public string SprintId { get; set; }

        [ForeignKey("SprintId")]
        public Sprint Sprint { get; set; }
    }
}