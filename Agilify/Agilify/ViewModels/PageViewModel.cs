using Agilify.Helpers;
using Agilify.Models;
using Agilify.Services;

using Xamarin.Forms;

namespace Agilify.ViewModels
{
	public class PageViewModel<T> : ObservableObject where T : IItem
	{

		bool isBusy = false;
        string title = string.Empty;

        public PageViewModel()
        {

        }

        public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}
	}
}

