using Agilify.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Agilify.Models
{
    public class Feature : PortfolioBacklogItem
    {
        public string EpicId { get; set; }
    }
}
