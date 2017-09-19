using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Agilify.Views.CreatePages;
using Agilify.Views.DetailPages;
using Agilify.Views.EditPages;
using Xamarin.Forms;
using Label = Xamarin.Forms.Label;

using Syncfusion.ListView.XForms;

namespace Agilify.Views.ListPages
{
	public class TeamsPage : ItemsListPage<Team, Member, TeamDetailPage, CreateTeamPage, EditTeamPage>
    {
		public TeamsPage ()
		{
            Title = "Teams";

		    ItemsList.HasUnevenRows = true;
            ItemsList.ItemTemplate = new DataTemplate(() =>
            {
                Image icon = new Image();
                Label nameLabel = new Label();
                Label projectsLabel = new Label();
                Label membersLabel = new Label();

                icon.Source = "ic_team";

                nameLabel.SetBinding(Label.TextProperty, "Name");
                nameLabel.FontSize = 18;
                nameLabel.FontAttributes = FontAttributes.Bold;

                projectsLabel.SetBinding(Label.TextProperty, "ProjectsCount");
                projectsLabel.FontSize = 14;

                membersLabel.SetBinding(Label.TextProperty, "MembersCount");
                membersLabel.FontSize = 14;

                var template = new ViewCell
                {
                    View = new StackLayout
                    {
                        Padding = new Thickness(10, 5),
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            icon,
                            new StackLayout
                            {
                                Padding = new Thickness(10, 0),
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                Children =
                                {
                                    nameLabel,
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Spacing = 80,
                                        Children =
                                        {
                                            membersLabel,
                                            projectsLabel
                                        }
                                    }
                                }
                            }
                        }
                    }
                };

                var editMenuItem = new MenuItem { Text = "Edit", Command = EditCommand };
                var deleteMenuItem = new MenuItem { Text = "Delete", Command = DeleteCommand, IsDestructive = true };

                deleteMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
                editMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));


                template.ContextActions.Add(editMenuItem);
                template.ContextActions.Add(deleteMenuItem);


                return template;
            });
        }

        public override async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            ((ListView)sender).SelectedItem = null;

            await Navigation.PushAsync(new TeamDetailPage(VM, (Team)e.SelectedItem));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
