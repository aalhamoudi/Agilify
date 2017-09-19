using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Xamarin.Forms;
using Label = Xamarin.Forms.Label;

namespace Agilify.Views.EditPages
{
	public class EditSprintPage : EditItemPage<Sprint>
	{
	    public Entry SprintName { get; set; }
	    public DatePicker StartDate { get; set; }
	    public DatePicker EndDate { get; set; }
		public EditSprintPage()
		{
            Title = "Edit Sprint";

            SprintName = new Entry
            {
                Placeholder = "Sprint Name"
            };

            StartDate = new DatePicker { HorizontalOptions = LayoutOptions.FillAndExpand, Date = DateTime.Today };
            EndDate = new DatePicker { HorizontalOptions = LayoutOptions.FillAndExpand, Date = DateTime.Today };
		    var saveButton = new Button {Text = "save"};


            

		    saveButton.Clicked += async (sender, args) =>
		    {
		        await VM.Update(Item);
		        await Navigation.PopAsync();
		    };

            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Spacing = 10,
                Children =
                {
                    SprintName,
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
                                    StartDate
                                }
                            },
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Children =
                                {
                                    new Label { Text = "End Date", HorizontalOptions = LayoutOptions.CenterAndExpand },
                                    EndDate
                                }
                            }


                        }
                    },
                    saveButton
                }
            };
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
            BindingContext = Item;
            SprintName.SetBinding(Entry.TextProperty, "Name");
            StartDate.SetBinding(DatePicker.DateProperty, "StartDate");
            EndDate.SetBinding(DatePicker.DateProperty, "EndDate");

	        if (Item.Owner == null)
	            Item.Owner = App.User;
	    }
	}
}
