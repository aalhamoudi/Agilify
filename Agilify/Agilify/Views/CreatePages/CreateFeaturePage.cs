using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Xamarin.Forms;

namespace Agilify.Views.CreatePages
{
	public class CreateFeaturePage : CreateItemPage<Feature, Epic>
	{
		public CreateFeaturePage()
		{
            Title = "Create Feature";
            BindingContext = Item;

            var featureName = new Entry { Placeholder = "Name" };
            var featureDescription = new Entry { Placeholder = "Description" };

            featureName.SetBinding(Entry.TextProperty, "Name");
            featureDescription.SetBinding(Entry.TextProperty, "Description");

            Item.Category = "New";

            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Children =
                {
                    featureName,
                    featureDescription,
                    CreateButton
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Item.EpicId = ParentElement?.Id;
            Item.Owner = App.User;
        }
    }
}
