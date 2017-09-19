using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Agilify.Helpers;
using Agilify.Models;
using Agilify.ViewModels;
using Xamarin.Forms;

namespace Agilify.Views.EditPages
{
	public class EditItemPage<T> : ContentPage where T : IItem
	{
        public ItemsPageViewModel<T> VM { get; set; }
        public T Item { get; set; }
	    public Button SaveButton { get; set; }

		public EditItemPage ()
		{
            SaveButton = new Button
            {
                Text = "Save"
            };

            SaveButton.Clicked += OnSave;

        }

        protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        Title = Item.Name;
	    }

	    protected async void OnSave(object sender, EventArgs e)
	    {
	        await VM.Update(Item);
	        await Navigation.PopAsync();
	    }
    }
}
