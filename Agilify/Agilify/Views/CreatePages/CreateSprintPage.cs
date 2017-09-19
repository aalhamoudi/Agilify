using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agilify.Models;
using Xamarin.Forms;

namespace Agilify.Views.CreatePages
{
	public class CreateSprintPage : CreateItemPage<Sprint, Project>
	{
		public CreateSprintPage()
		{
		    Title = "Create Sprint";
		    BindingContext = Item;

		    var sprintName = new Entry
		    {
		        Placeholder = "Sprint Name"
		    };

		    var startDate = new DatePicker {HorizontalOptions = LayoutOptions.FillAndExpand, MinimumDate = DateTime.Today};
		    var endDate = new DatePicker {HorizontalOptions = LayoutOptions.FillAndExpand, MinimumDate = DateTime.Today };


            sprintName.SetBinding(Entry.TextProperty, "Name");
            startDate.SetBinding(DatePicker.DateProperty, "StartDate");
            endDate.SetBinding(DatePicker.DateProperty, "EndDate");

            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Spacing = 10,
                Children =
                {
                    sprintName,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Children =
                                {
                                    new Label { Text = "Start Date", HorizontalOptions = LayoutOptions.CenterAndExpand},
                                    startDate
                                }
                            },
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Children =
                                {
                                    new Label { Text = "End Date", HorizontalOptions = LayoutOptions.CenterAndExpand },
                                    endDate
                                }
                            }
                            
                        
                        }
                    },
                    CreateButton
                }
            };
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        Item.ProjectId = ParentElement.Id;
	    }
	}
}
