using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Agilify.Services;
using Xamarin.Forms;

namespace Agilify.Views.CreatePages
{
	public class CreateTeamPage : CreateItemPage<Team, Member>
	{
		public CreateTeamPage ()
		{
            Title = "Create Team";

            var teamName = new Entry { Placeholder = "Team Name" };

            Item.Owner = App.User;
            Item.Members.Add(App.User);

            teamName.SetBinding(Entry.TextProperty, "Name");
		    teamName.TextChanged += (sender, args) => CreateButton.IsEnabled = string.IsNullOrWhiteSpace(teamName.Text) ? false : true;

            Content = new StackLayout {
                Padding = new Thickness(10),
				Children =
                {
                    teamName,
                    CreateButton
				}
			};
		}

	    protected override void OnCreate(object sender, EventArgs e)
	    {
	        base.OnCreate(sender, e);
            AccountManager.SyncUserData();

        }
    }
}
