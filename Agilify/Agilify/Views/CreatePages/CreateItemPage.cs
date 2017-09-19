using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

using Agilify.ViewModels;
using Agilify.Helpers;
using Agilify.Models;

namespace Agilify.Views.CreatePages
{
	public class CreateItemPage<T, P> : ContentPage where T : IItem, new() where P : IItem, new()
	{
        public ItemsPageViewModel<T> VM { get; set; }
        public T Item { get; set; } = new T();
	    public P ParentElement { get; set; }
        public Button CreateButton { get; set; }

        public CreateItemPage ()
        {
            BindingContext = Item;

            Item.Owner = App.User;

            CreateButton = new Button
            {
                Text = "Create",
                IsEnabled = true
            };

            CreateButton.Clicked += OnCreate;
        }

	    public CreateItemPage(P parent) : this()
	    {
	        ParentElement = parent;

	    }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected virtual async void OnCreate(object sender, EventArgs e)
        {
            await VM.Add(Item);
            await Navigation.PopAsync();
        }
    }
}
