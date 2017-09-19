using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agilify.Helpers
{
	public interface IDataStore<T>
	{
		Task AddItemAsync(T item);
		Task UpdateItemAsync(T item);
		Task SaveItemAsync(T item);
        Task DeleteItemAsync(T item);
		Task<T> GetItemAsync(string id);
		Task<ObservableRangeCollection<T>> GetItemsAsync(bool forceRefresh = false);

		Task SyncAsync();
	}
}
