using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Xamarin.Forms;

namespace Agilify.Views.CreatePages
{
	public class CreateProjectPage : CreateItemPage<Project, Team>
	{
	    public Entry ProjectName { get; set; }
	    public Entry ProjectDescription { get; set; }
	    public Picker ProjectTeam { get; set; }
		public CreateProjectPage ()
		{
		    Title = "Create Project";
            ProjectName = new Entry { Placeholder = "Project Name"};
            ProjectDescription = new Entry { Placeholder = "Project Description" };
		    ProjectTeam = new Picker();

            ProjectName.SetBinding(Entry.TextProperty, "Name");
            ProjectDescription.SetBinding(Entry.TextProperty, "Description");
            CreateButton.IsEnabled = false;


            


            Content = new StackLayout {
                Padding = new Thickness(10),
				Children =
                {
                    ProjectName,
                    ProjectDescription,
                    ProjectTeam,
                    CreateButton
				}
			};
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = Item;



            if (ParentElement != null)
            {
                Item.Team = ParentElement;
                Item.TeamId = ParentElement.Id;

                ProjectTeam.Items.Add(ParentElement.Name);
                ProjectTeam.SelectedIndex = 0;
                ProjectTeam.IsEnabled = false;

                ProjectName.TextChanged += (sender, args) => CreateButton.IsEnabled = !string.IsNullOrWhiteSpace(ProjectName.Text) ? true : false;
            }
            else
            {
                ProjectTeam.Items.Add("Select Team");
                ProjectTeam.SelectedIndex = 0;

                if (App.User?.Teams?.Any() ?? false)
                {
                    foreach (var team in App.User?.Teams)
                        ProjectTeam.Items.Add(team.ToString());
                }

                ProjectName.TextChanged += (sender, args) =>
                {
                    CreateButton.IsEnabled = !string.IsNullOrWhiteSpace(ProjectName.Text) &&
                                             ProjectTeam.SelectedIndex != 0
                        ? true
                        : false;
                };

                ProjectTeam.SelectedIndexChanged += (sender, args) =>
                {
                    if (ProjectTeam.SelectedIndex != 0)
                    {
                        Item.Team = App.User.Teams[ProjectTeam.SelectedIndex - 1];

                        Item.TeamId = App.User.Teams[ProjectTeam.SelectedIndex - 1].Id;

                    }

                    CreateButton.IsEnabled = !string.IsNullOrWhiteSpace(ProjectName.Text) && ProjectTeam.SelectedIndex != 0 ? true : false;

                };
            }

            

        }
    }
}
