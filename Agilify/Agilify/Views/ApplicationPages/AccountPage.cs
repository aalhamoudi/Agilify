using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Helpers;
using Agilify.Services;
using Xamarin.Forms;

namespace Agilify.Views.ApplicationPages
{
	public class AccountPage : ContentPage
	{
		public AccountPage ()
		{
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

			Content = new StackLayout {
				Children =
                {
					signoutButton
				}
			};
		}
	}
}
