using Agilify.Helpers;
using Newtonsoft.Json;

namespace Agilify.Models
{
    public class Project : Item
    {
        string description = string.Empty;
        Team team;
        private string teamId = string.Empty;

        [JsonProperty(PropertyName = "description")]

        public string Description
		{
			get { return description; }
			set { SetProperty(ref description, value); }
		}

        [JsonProperty(PropertyName = "teamId")]
        public string TeamId
        {
            get { return teamId; }
            set { SetProperty(ref teamId, value); }
        }

        [JsonProperty(PropertyName = "team")]
        public Team Team
        {
            get { return team; }
            set { SetProperty(ref team, value); }
        }

        public ObservableRangeCollection<Sprint> Sprints { get; set; }
    }
}
