using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Xamarin.Forms;

namespace Agilify.Views.CreatePages
{
	public class CreateEpicPage : CreateItemPage<Epic, Project>
	{
		public CreateEpicPage()
		{
		    Title = "Create Epic";
		    BindingContext = Item;

		    var epicName = new Entry {Placeholder = "Name"};
		    var epicDescription = new Entry {Placeholder = "Description"};

            epicName.SetBinding(Entry.TextProperty, "Name");
            epicDescription.SetBinding(Entry.TextProperty, "Description");

		    Item.Category = "New";

			Content = new StackLayout {
                Padding = new Thickness(10),
				Children =
                {
                    epicName,
                    epicDescription,
                    CreateButton
                }
			};
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
            Item.ProjectId = ParentElement?.Id;

        }
    }
}
