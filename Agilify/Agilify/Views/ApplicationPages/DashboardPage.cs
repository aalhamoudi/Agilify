using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Agilify.Models;
using Agilify.ViewModels;
using Xamarin.Forms;

using Syncfusion.SfKanban.XForms;
using Syncfusion.ListView.XForms;
using Syncfusion.SfPullToRefresh.XForms;
using Agilify.Services;

namespace Agilify.Views.ApplicationPages
{
	public class DashboardPage : ContentPage
	{
	    public DashboardViewModel VM { get; set; } = new DashboardViewModel();
        public DashboardPage ()
		{
		    Title = "Dashboard";
		    BindingContext = VM;

            SfPullToRefresh pull = new SfPullToRefresh {};
            ObservableCollection<Team> teams = new ObservableCollection<Team>
            {
                new Team { Name = "Team 1" },
                new Team { Name = "Team 2" }
            };
		    SfListView list = new SfListView
		    {
                ItemsSource = teams,
                ItemTemplate = new DataTemplate(() =>
                {
                    Image icon = new Image();
                    Label nameLabel = new Label();

                    icon.Source = "ic_team";

                    nameLabel.SetBinding(Label.TextProperty, "Name");
                    nameLabel.FontSize = 18;

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
                                        nameLabel
                                    }
                                }
                            }
                        }
                    };
                    return template;
                })
            };

            pull.PullingEvent += (sender, args) =>
            {
                DisplayAlert("User", Settings.UserId, "OK");

                Device.StartTimer(new TimeSpan(0, 0, 2), () =>
                {
                    return false;
                });
            };

            pull.RefreshingEvent += sender =>
		    {
                DisplayAlert("User", App.User.Id, "OK");

                Device.StartTimer(new TimeSpan(0, 0, 2), () =>
		        {
                    return false;
		        });
		    }; 
		    pull.PullableContent = new Button();

            pull.TransitionMode = TransitionType.SlideonTop;

		    Content = pull;

		}
	}
}
