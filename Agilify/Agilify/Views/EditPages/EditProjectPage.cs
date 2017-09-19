using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Xamarin.Forms;

namespace Agilify.Views.EditPages
{
	public class EditProjectPage : EditItemPage<Project>
	{
        public Entry ProjectName { get; set; }
        public Entry ProjectDescription { get; set; }
        public Picker ProjectTeam { get; set; }
        public Button SaveButton { get; set; }
        public EditProjectPage ()
		{
            Title = "Create Project";
            ProjectName = new Entry { Placeholder = "Project Name" };
            ProjectDescription = new Entry { Placeholder = "Project Description" };
            ProjectTeam = new Picker();
            SaveButton = new Button { Text = "Save" };

            ProjectName.TextChanged += (sender, args) => SaveButton.IsEnabled = !string.IsNullOrWhiteSpace(ProjectName.Text) ? true : false;


            ProjectTeam.IsEnabled = false;

		    SaveButton.Clicked += async (sender, args) =>
		    {
		        await VM.Update(Item);
		        await Navigation.PopAsync();
		    }; 

            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Children =
                {
                    ProjectName,
                    ProjectDescription,
                    ProjectTeam,
                    SaveButton
                }
            };
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
            BindingContext = Item;
            ProjectName.SetBinding(Entry.TextProperty, "Name");
            ProjectDescription.SetBinding(Entry.TextProperty, "Description");

            ProjectTeam.Items.Add(App.User.Teams.First(t => t.Id == Item?.TeamId)?.Name);
	        ProjectTeam.SelectedIndex = 0;

	    }
    }
}
