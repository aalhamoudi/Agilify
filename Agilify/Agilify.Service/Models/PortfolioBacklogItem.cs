using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AgilifyService.Models
{
    public class PortfolioBacklogItem : BacklogItem
    {
        public enum PortfolioBacklogItemStatus
        {
            New,
            InProgress,
            Don
        }

        [JsonProperty(PropertyName = "status")]

        public PortfolioBacklogItemStatus Status { get; set; }
    }
}