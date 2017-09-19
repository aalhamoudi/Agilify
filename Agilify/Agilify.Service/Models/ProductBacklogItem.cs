using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AgilifyService.Models
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


        [JsonProperty(PropertyName = "status")]
        public ProductBacklogItemStatus Status { get; set; }
    }
}