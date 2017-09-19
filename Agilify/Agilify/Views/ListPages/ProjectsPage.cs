using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using Agilify.Helpers;
using Agilify.Models;
using Agilify.ViewModels;
using Agilify.Views.CreatePages;
using Agilify.Views.DetailPages;
using Agilify.Views.EditPages;
using Xamarin.Forms;

namespace Agilify.Views.ListPages
{
    public class ProjectsPage : ItemsListPage<Project, Team, ProjectDetailPage, CreateProjectPage, EditProjectPage>
    {
        public Team Team { get; set; }
        public ProjectsPage(Func<Project, bool> filter = null) : base(filter)
        {

            Title = "Projects";

            ItemsList.ItemTemplate = new DataTemplate(() =>
            {
                Image icon = new Image();
                Label nameLabel = new Label();

                icon.Source = "ic_project";

                nameLabel.SetBinding(Label.TextProperty, "Name");
                nameLabel.FontSize = 18;

                var template =  new ViewCell
                {
                    Height = 60,
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
                                    nameLabel
                                }
                            }
                        }
                    }
                };

                var editMenuItem = new MenuItem {Text = "Edit", Command = EditCommand};
                var deleteMenuItem = new MenuItem {Text = "Delete", Command = DeleteCommand, IsDestructive = true};

                deleteMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
                editMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));


                template.ContextActions.Add(editMenuItem);
                template.ContextActions.Add(deleteMenuItem);


                


                return template;
            });

        }

        public ProjectsPage(Team team) : this(p => p.TeamId.Equals(team.Id))
        {
            ParentElement = Team = team;
        }

    }
}
