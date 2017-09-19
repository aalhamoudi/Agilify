using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Agilify.ViewModels;
using Agilify.Views.ListPages;
using Xamarin.Forms;

namespace Agilify.Views.DetailPages
{
	public class TeamDetailPage : ItemDetailPage<Team>
	{
	    public ItemsPageViewModel<Team> VM { get; set; }
        public TeamDetailPage()
        {
        }
        public TeamDetailPage (ItemsPageViewModel<Team> vm, Team team) : this()
		{
		    VM = vm;
		    Item = team;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!Children.Any())
            {
                Children.Add(new ProjectsPage(Item));
                Children.Add(new MembersPage(Item, VM, this));
            }
        }
    }
}
