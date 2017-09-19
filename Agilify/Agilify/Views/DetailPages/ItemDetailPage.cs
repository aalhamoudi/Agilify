using Xamarin.Forms;

using Agilify.Helpers;
using Agilify.Models;
using Agilify.ViewModels;
using Agilify.Views.EditPages;

namespace Agilify.Views.DetailPages
{
	public class ItemDetailPage<T> : TabbedPage where T : IItem
	{
        public T Item { get; set; }

        public ItemDetailPage ()
        {
            BarBackgroundColor = Color.FromHex("#2196F3");

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = Item.Name;
        }
    }
}
