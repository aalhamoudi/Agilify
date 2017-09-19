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
using System.Linq.Expressions;
using Agilify.Services;

namespace Agilify.ViewModels
{
	public class ItemsPageViewModel<T> : PageViewModel<T> where T : IItem
	{
        public static SyncStore<T> SyncStore { get; private set; } = new SyncStore<T>();

        public ObservableRangeCollection<T> Items { get; set; } = new ObservableRangeCollection<T>();

        public Command LoadDataCommand { get; set; }
        public Command RefreshDataCommand { get; set; }
	    public Expression<Func<T, bool>> QueryPredicate { get; set; }
	    public Func<T, bool> FilterPredicate { get; set; }
	    public bool Sync { get { return App.IsOnline ? true : false; } }

        public ItemsPageViewModel()
		{
			LoadDataCommand = new Command(async () => await LoadItems());
            RefreshDataCommand = new Command(async () => await LoadItems());
		}
        public ItemsPageViewModel(Expression<Func<T, bool>> query, bool isQuery) : this()
        {
            QueryPredicate = query;
        }
        public ItemsPageViewModel(Func<T, bool> filter) : this()
        {
            FilterPredicate = filter;
        }

	    public void UpdateFilter(Func<T, bool> filter)
	    {
	        FilterPredicate = filter;
	    }

        public async Task Add(T item)
        {
            await SyncStore.AddItemAsync(item);
            await LoadItems();
        }

        public async Task Update(T item)
        {
            await SyncStore.UpdateItemAsync(item);
            await LoadItems();
        }

        public async Task Delete(T item)
        {
            Items.Remove(item);
            await SyncStore.DeleteItemAsync(item);
            await LoadItems();
        }

	    public async Task<ObservableRangeCollection<T>> All()
	    {
	        return await SyncStore.GetItemsAsync(Sync);
        }


        public async Task<ObservableRangeCollection<T>> Query(Expression<Func<T, bool>>  query)
	    {
	        return await SyncStore.QueryItemsAsync(query, Sync);
	    }

        public async Task<ObservableRangeCollection<T>> Filter(Func<T, bool> filter)
        {
            return await SyncStore.FilterItemsAsync(filter, Sync);
        }


        public async Task<ObservableRangeCollection<T>> Get()
	    {
	        return FilterPredicate != null ? await Filter(FilterPredicate) : await All();
        }

	    public async Task<T> GetItem(string id)
	    {
	        return await SyncStore.GetItemAsync(id);
	    }

        public async Task LoadItems()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
			    ObservableRangeCollection<T> items = await Get();
                if (items != null)
                    Items.ReplaceRange(items);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);

                MessagingCenter.Send(new MessagingCenterAlert
				{
					Title = "Error",
					Message = "Unable to load items.",
					Cancel = "OK"
				}, "message");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}