using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AgilifyService.Models
{
    public class BacklogItem : Item
    {
        [JsonProperty(PropertyName = "description")]
        public string Descripiton { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public string Tags { get; set; }
    }
}