using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Agilify.Models;
using Agilify.ViewModels;
using Xamarin.Forms;

namespace Agilify.Views.ApplicationPages
{
    class TestPage : ContentPage
    {
        public ListView ItemsList { get; set; }
        public Command DeleteCommand { get; set; }
        public MenuItem DeleteMenuItem { get; set; }

        public TestPage()
        {
            Title = "Test";

            var items = new ObservableCollection<string> {"One", "Two"};
            

            DeleteCommand = new Command(async (object item) => await OnDelete((string)item));

            DeleteMenuItem = new MenuItem
            {
                Text = "Delete",
                Command = DeleteCommand,
                IsDestructive = true
            };



            DeleteMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

            ItemsList = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = items,
                IsPullToRefreshEnabled = true,
            };





            Content = new StackLayout
            {
                Children =
                {
                    ItemsList
                }
            };

            ItemsList.ItemTemplate = new DataTemplate(() =>
            {
                Image icon = new Image();
                Label nameLabel = new Label();

                icon.Source = "ic_project";

                nameLabel.SetBinding(Label.TextProperty, ".");
                nameLabel.FontSize = 18;

                var template = new ViewCell
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

                var menuItem = new MenuItem
                {
                    Text = "Click"
                };

                menuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

                menuItem.Clicked += (sender, args) => OnDelete(((MenuItem) sender).CommandParameter as string);
                
                
                template.ContextActions.Add(menuItem);



                return template;
            });
        }

        protected virtual async Task OnDelete(string item)
        {
            var confirm = await DisplayAlert("Delete", $"Delete {item}?", "Yes", "No");

           
        }
    }
}
