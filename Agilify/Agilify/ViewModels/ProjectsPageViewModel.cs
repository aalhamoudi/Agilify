using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

using Agilify.Helpers;
using Agilify.Models;
using Agilify.Views;


using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Agilify.ViewModels
{
	public class ProjectsPageViewModel : ItemsPageViewModel<Project>
	{
        public ProjectsPageViewModel()
		{
			Title = "Projects";

        }
	}
}