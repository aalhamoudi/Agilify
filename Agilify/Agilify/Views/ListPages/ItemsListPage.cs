using Agilify.Helpers;
using Agilify.Models;
using Agilify.ViewModels;
using Agilify.Views.CreatePages;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Agilify.Views.DetailPages;
using Agilify.Views.EditPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Syncfusion.ListView.XForms;


namespace Agilify.Views.ListPages
{
    public class ItemsListPage<T, P, D, C, E> : ItemsPage<T, P, D, C, E> where T : IItem, new() where P : IItem, new() where C : CreateItemPage<T, P>, new() where D : ItemDetailPage<T>, new() where E : EditItemPage<T>, new()
    {
        public ListView ItemsList { get; set; }

        public ItemsListPage(Func<T, bool> filter = null) : base(filter)
        {
            ItemsList = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = VM.Items,
                IsPullToRefreshEnabled = true,
            };

            ItemsList.Refreshing += (sender, args) => VM.RefreshDataCommand.Execute(new { });

            ItemsList.ItemTapped += OnItemTapped;
            ItemsList.ItemSelected += OnItemSelected;
            ItemsList.SetBinding(ListView.IsRefreshingProperty, "IsBusy", BindingMode.OneWay);

            ItemsList.BindingContext = VM;

            Content = new StackLayout
            {
                Children =
                {
                    ItemsList
                }
            };

        }

    }
}
