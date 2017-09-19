using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agilify.Models;
using Agilify.Views.CreatePages;
using Agilify.Views.DetailPages;
using Agilify.Views.EditPages;
using Xamarin.Forms;

namespace Agilify.Views.ListPages
{
    public class SprintsPage : ItemsListPage<Sprint, Project, SprintDetailPage, CreateSprintPage, EditSprintPage>
    {
        public SprintsPage(Project project) : base(s => s.ProjectId == project.Id)
        {
            ParentElement = project;
            Title = "Sprints";

            ItemsList.ItemTemplate = new DataTemplate(() =>
            {
                Image icon = new Image();
                Label nameLabel = new Label();

                icon.Source = "ic_sprint";

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
            await Navigation.PushAsync(new WorkItemsPage((Sprint)e.SelectedItem));
        }
    }
}
