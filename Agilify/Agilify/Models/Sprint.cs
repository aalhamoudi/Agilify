using Agilify.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilify.Models
{
    public class Sprint : Item
    {
        public string ProjectId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today;

        public ObservableRangeCollection<WorkItem> WorkItems { get; set; }
    }
}
