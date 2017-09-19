using Agilify.Helpers;
using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;


namespace Agilify.Models
{
    public class Member : Item
    {
        public string Email { get; set; }
        public string Image { get; set; }

        [JsonIgnore]
        public ObservableRangeCollection<Team> Teams { get; set; } = new ObservableRangeCollection<Team>();

        [JsonIgnore]
        public ObservableRangeCollection<Project> Projects { get; set; } = new ObservableRangeCollection<Project>();
    }

    public class UserBindingModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
