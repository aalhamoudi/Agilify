using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Agilify.Models
{
    public class PortfolioBacklogItem : BacklogItem
    {
        public enum PortfolioBacklogItemStatus
        {
            New,
            InProgress,
            Done
        }

        PortfolioBacklogItemStatus status = PortfolioBacklogItemStatus.New;

        [JsonProperty(PropertyName = "status")]

        public PortfolioBacklogItemStatus Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }
    }
}
