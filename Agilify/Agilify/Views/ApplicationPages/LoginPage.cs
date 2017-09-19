using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Agilify.Services;
using Xamarin.Forms;

namespace Agilify.Views.ApplicationPages
{
	public class LoginPage : ContentPage
	{
		public LoginPage ()
		{
		    Title = "Login";

		    var register = new Button
		    {
                Text = "Register"
		    };

		    var facebook = new Button
		    {
                Image = "facebook",
                Text = "Facebook",
                TextColor = Color.Black,
                BackgroundColor = Color.White
            };

            var twitter = new Button
            {
                Image = "twitter",
                Text = "Twitter"
            };

            var google = new Button
            {
                Image = "google",
                Text = "Google"
            };

            var microsoft = new Button
            {
                Image = "microsoft",
                Text = "Microsoft",
                TextColor = Color.Black,
                BackgroundColor = Color.White
            };

		    var agilify = new Image {Source = "agilify", HeightRequest = 350};
            

		    register.Clicked += (sender, args) => Navigation.PushAsync(new RegistrationPage());
		    facebook.Clicked += (sender, args) => OnLogin("Facebook");
		    twitter.Clicked += (sender, args) => OnLogin("Twitter");
            google.Clicked += (sender, args) => OnLogin("Google");
            microsoft.Clicked += (sender, args) => OnLogin("Microsoft");

            
            Content = new StackLayout {
                BackgroundColor = Color.FromHex("07212C"),
                Padding = new Thickness(15),
                VerticalOptions = LayoutOptions.FillAndExpand,
				Children =
                {
                    agilify,
                    facebook,
                    //twitter,
                    //google,
                    microsoft
				}
			};
		}

        private async void OnLogin(string provider)
        {
            await AccountManager.Login(provider);

            await Navigation.PopAsync();
            await Task.Delay(300); // Fix issue due to fast transitions before page reinflation is complete

            App.SetMainPage();

        }
    }
}
