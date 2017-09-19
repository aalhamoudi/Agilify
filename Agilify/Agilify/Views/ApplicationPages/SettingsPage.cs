using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Services;
using Xamarin.Forms;

namespace Agilify.Views.ApplicationPages
{
	public class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
		    Title = "Settings";

            var signoutButton = new Button
            {
                Text = "Sign Out"
            };

            signoutButton.Clicked += async (sender, args) =>
            {
                await App.CloudClient.LogoutAsync();
                Settings.Clear();
                App.SetMainPage();
            };

            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Children =
                {
                    signoutButton
                }
            };
        }
	}
}
