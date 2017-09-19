using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Agilify.Models
{
    public class Deadline : Item
    {
        private DateTime from = DateTime.Now;
        private DateTime to = DateTime.Now;
        private Color color = Xamarin.Forms.Color.Black;
        private Project project = new Project();
        private Sprint sprint = new Sprint();

        [JsonProperty(PropertyName = "from")]
        public DateTime From
        {
            get { return from; }
            set { SetProperty(ref from, value); }
        }

        [JsonProperty(PropertyName = "to")]
        public DateTime To
        {
            get { return to; }
            set { SetProperty(ref to, value); }
        }

        [JsonProperty(PropertyName = "color")]
        public Color Color
        {
            get { return color; }
            set { SetProperty(ref color, value); }
        }

        [JsonProperty(PropertyName = "project")]
        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }

        [JsonProperty(PropertyName = "sprint")]
        public Sprint Sprint
        {
            get { return sprint; }
            set { SetProperty(ref sprint, value); }
        }

        public string Title
        {
            get { return (Project?.Name ?? "Project") + ": " + (Sprint?.Name ?? "Sprint");}
        }
    }
}
