using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agilify.Helpers;
using Newtonsoft.Json;
using Syncfusion.SfKanban.XForms;

namespace Agilify.Models
{
    public class BacklogItem : Item
    {
        string description = string.Empty;
        string category = string.Empty;
        string color = string.Empty;
        string image = string.Empty;
        string tags = string.Empty;

        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        [JsonProperty(PropertyName = "category")]
        public string Category
        {
            get { return category; }
            set { SetProperty(ref category, value); }
        }

        [JsonProperty(PropertyName = "color")]
        public string Color
        {
            get { return color; }
            set { SetProperty(ref color, value); }
        }

        [JsonProperty(PropertyName = "image")]
        public string Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        [JsonProperty(PropertyName = "tags")]
        public string Tags
        {
            get { return tags; }
            set { SetProperty(ref tags, value); }
        }

        public static implicit operator KanbanModel (BacklogItem item)
        {
            var category = "New";
            switch (item.Category)
            {
                case "New":
                    category = "New";
                    break;
                case "In Progress":
                    category = "In Progress";
                    break;
                case "Approved":
                    category = "Approved";
                    break;
                case "Commited":
                    category = "Committed";
                    break;
                case "Done":
                    category = "Done";
                    break;
            }

            return new KanbanModel
            {
                ID = Math.Abs(item.Id.GetHashCode()),
                Title = item.Name,
                Description = item.Description,
                Category = category,
                ColorKey = item.Color,
                ImageURL = !string.IsNullOrEmpty(item.Image) ? item.Image : null,
                Tags = item.Tags?.Split(';').ToArray() ?? new string[] {}

            };
        }
    }
}
