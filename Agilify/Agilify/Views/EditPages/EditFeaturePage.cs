using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Agilify.ViewModels;
using Syncfusion.SfKanban.XForms;
using Xamarin.Forms;

namespace Agilify.Views.EditPages
{
	public class EditFeaturePage : EditItemPage<Feature>
	{
	    public Entry FeatureName { get; set; }
	    public Entry FeatureDescription { get; set; }
	    public Button SaveButton { get; set; }

        public ObservableCollection<KanbanModel> Cards { get; set; }

        public EditFeaturePage()
		{
            Title = "Create Feature";

            FeatureName = new Entry { Placeholder = "Name" };
            FeatureDescription = new Entry { Placeholder = "Description" };

		    SaveButton = new Button {Text = "Save"};
		    SaveButton.Clicked += async (sender, args) =>
		    {
		        await VM.Update(Item);
                Cards.Remove(Cards.First(m => m.ID == Math.Abs(Item.Id.GetHashCode())));
                Cards.Add(Item);
                await Navigation.PopAsync();
		    };


            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Children =
                {
                    FeatureName,
                    FeatureDescription,
                    SaveButton
                }
            };
        }

	    public EditFeaturePage(Feature feature, ItemsPageViewModel<Feature> vm, ObservableCollection<KanbanModel> cards) : this()
	    {
	        Item = feature;
	        VM = vm;
	        Cards = cards;
	    }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Item.Owner == null)
            Item.Owner = App.User;

            BindingContext = Item;
            FeatureName.SetBinding(Entry.TextProperty, "Name");
            FeatureDescription.SetBinding(Entry.TextProperty, "Description");
        }
    }
}
