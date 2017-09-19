using Agilify.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilify.Models
{
    public class Team : Item
    {
        public ObservableRangeCollection<Member> Members { get; set; } = new ObservableRangeCollection<Member>();
        public ObservableRangeCollection<Project> Projects { get; set; } = new ObservableRangeCollection<Project>();

        public string MembersCount { get { return "Members: " + (Members?.Count ?? 0); } }
        public string ProjectsCount { get { return "Projects: " + (Projects?.Count ?? 0); } }

        public override string ToString()
        {
            return Name;
        }
    }
}
