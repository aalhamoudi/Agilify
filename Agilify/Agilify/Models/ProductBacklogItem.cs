using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Agilify.Models
{
    public class ProductBacklogItem : BacklogItem
    {
        public enum ProductBacklogItemStatus
        {
            New,
            Approved,
            Committed,
            Done
        }

        ProductBacklogItemStatus status = ProductBacklogItemStatus.New;
        [JsonProperty(PropertyName = "status")]
        public ProductBacklogItemStatus Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }
    }
}
