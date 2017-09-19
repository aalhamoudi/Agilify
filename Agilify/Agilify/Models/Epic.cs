using Agilify.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Agilify.Models
{
    public class Epic : PortfolioBacklogItem
    {
        public string ProjectId { get; set; }
    }
}
