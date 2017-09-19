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
	public class EditEpicPage : EditItemPage<Epic>
	{
	    public Entry EpicName { get; set; }
	    public Entry EpicDescription { get; set; }
        public ObservableCollection<KanbanModel> Cards { get; set; }

        public EditEpicPage()
		{
            Title = "Edit Epic";


            EpicName = new Entry { Placeholder = "Name", Text = Item?.Name};
            EpicDescription = new Entry { Placeholder = "Description" };
		    var saveButton = new Button {Text = "Save"};

            

		    saveButton.Clicked += async (sender, args) =>
		    {
		        try
		        {
                    await VM.Update(Item);
                    Cards.Remove(Cards.First(m => m.ID == Math.Abs(Item.Id.GetHashCode())));
                    Cards.Add(Item);
                    await Navigation.PopAsync();
                }
		        catch (Exception e)
		        {
		            Console.WriteLine(e);
		        }
		    };

            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Children =
                {
                    EpicName,
                    EpicDescription,
                    saveButton
                }
            };
        }

	    public EditEpicPage(Epic epic, ItemsPageViewModel<Epic> ParentVM, ObservableCollection<KanbanModel> cards) : this()
	    {
            if (epic != null)
                Item = epic;
	        if (ParentVM != null)
	            VM = ParentVM;
	        if (cards != null)
	            Cards = cards;
	    }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
            BindingContext = Item;
	        if (Item.Owner == null)
	            Item.Owner = App.User;
            EpicName.SetBinding(Entry.TextProperty, "Name");
            EpicDescription.SetBinding(Entry.TextProperty, "Description");
        }
	}
}
