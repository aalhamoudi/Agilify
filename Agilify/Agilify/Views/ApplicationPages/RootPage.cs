using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Helpers;
using Agilify.Models;
using Agilify.Views.ApplicationPages;
using Agilify.Views.ListPages;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Agilify.Views
{
	public class RootPage : MasterDetailPage
	{
		public RootPage ()
		{
		    Master = new MenuPage(this);
            Detail = new NavigationPage(new TeamsPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
