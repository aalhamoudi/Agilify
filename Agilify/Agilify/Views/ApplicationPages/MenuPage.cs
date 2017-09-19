using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.ViewModels;
using Xamarin.Forms;
using Label = Xamarin.Forms.Label;

namespace Agilify.Views.ApplicationPages
{
	public class MenuPage : ContentPage
	{
	    public MenuPageViewModel VM { get; set; }
	    public RootPage RootPage { get; set; }

		public MenuPage (RootPage rootPage)
		{
		    RootPage = rootPage;
            VM = new MenuPageViewModel();
		    Title = "Menu";

            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Start
            };

            var name = new Label
            {
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.StartAndExpand,
                VerticalTextAlignment = TextAlignment.Center
            };

            var email = new Label
            {
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.StartAndExpand,
                VerticalTextAlignment = TextAlignment.Center
            };

		    var header = new StackLayout
		    {
		        Orientation = StackOrientation.Horizontal,
		        Padding = new Thickness(10),
                BackgroundColor = Color.FromHex("#2196F3"),
		        Children =
		        {
		            image,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Padding = new Thickness(20),
                        Children =
                        {
                            name,
                            email
                        }
                    }
                }
		    };

            image.SetBinding(Image.SourceProperty, "Image");
            name.SetBinding(Label.TextProperty, "Name");
            email.SetBinding(Label.TextProperty, "Email");
		    header.BindingContext = VM.User;

            ListView menuList = new ListView
		    {
                HasUnevenRows = true,
                ItemsSource = VM.MenuItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var icon = new Image();
                    var title = new Label
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        VerticalTextAlignment = TextAlignment.Center
                    };

                    icon.HeightRequest = 48;

                    icon.SetBinding(Image.SourceProperty, "Icon");
                    title.SetBinding(Label.TextProperty, "Title");

                    return new ViewCell
                    {
                        Height = 60,
                        View = new StackLayout
                        {
                            Orientation   = StackOrientation.Horizontal,
                            Spacing = 10,
                            Padding = new Thickness(10),
                            Children =
                            {
                                icon,
                                title
                            }
                        }
                    };
                }),
                Header = header
            };

		    menuList.ItemSelected += OnItemSelected;

			Content = new StackLayout {
				Children =
                {
                    menuList
				}
			};
		}

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
                RootPage.Detail = new NavigationPage(((MenuPageViewModel.NavDrawerItem)e.SelectedItem).Page);

            ((ListView)sender).SelectedItem = null;
            RootPage.IsPresented = false;
        }

    }
}
