using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Agilify.Views.ListPages;
using Xamarin.Forms;

namespace Agilify.Views.DetailPages
{
	public class ProjectDetailPage : ItemDetailPage<Project>
	{
		public ProjectDetailPage ()
		{
           


        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        if (!Children.Any())
	        {
                Children.Add(new EpicsPage(Item));
                Children.Add(new SprintsPage(Item));
            }
        }
	}
}
