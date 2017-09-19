using Agilify.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilify.Models
{
    public class WorkItem : ProductBacklogItem
    {
        public string SprintId { get; set; }
        public string Type { get; set; }
    }
}
