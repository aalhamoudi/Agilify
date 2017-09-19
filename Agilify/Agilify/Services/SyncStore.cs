using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.WindowsAzure.MobileServices;

using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

using Agilify.Helpers;
using Agilify.Models;
using Syncfusion.DataSource.Extensions;

namespace Agilify.Services
{
    public class SyncStore<T> : IDataStore<T> where T : IItem
    {
        MobileServiceSQLiteStore store;
        IMobileServiceSyncTable<T> table;


        public SyncStore()
        {
            this.store = App.LocalDB;
            this.table = App.CloudClient.GetSyncTable<T>();
        }


        public async Task AddItemAsync(T item)
        {
            try
            {
                await table.InsertAsync(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task UpdateItemAsync(T item)
        {
            await table.UpdateAsync(item);
        }

        public async Task SaveItemAsync(T item)
        {
            if (item.Id == null)
            {
                await AddItemAsync(item);
            }
            else
            {
                await UpdateItemAsync(item);
            }
        }

        public async Task DeleteItemAsync(T item)
        {
            await table.DeleteAsync(item);
        }

        public async Task<T> GetItemAsync(string id)
        {
            return await table.LookupAsync(id);       
        }

        public async Task<ObservableRangeCollection<T>> QueryItemsAsync(Expression<Func<T, bool>> query, bool sync)
        {
            if (sync)
            {
                await this.SyncAsync();
            }

            IEnumerable<T> items = await table.Where(query).ToEnumerableAsync();
            return new ObservableRangeCollection<T>(items);
        }

        public async Task<ObservableRangeCollection<T>> FilterItemsAsync(Func<T, bool> filter, bool sync)
        {
            if (sync)
            {
                await this.SyncAsync();
            }
            
            var items = await table.ToEnumerableAsync();
            var filtered = items.Where(filter);
            return new ObservableRangeCollection<T>(filtered);
        }

        public async Task<ObservableRangeCollection<T>> GetItemsAsync(bool sync = false)
        {
            try
            {
                if (sync)
                {
                    await this.SyncAsync();
                }

                IEnumerable<T> items = await table.ToEnumerableAsync();

                if (items != null)
                    return new ObservableRangeCollection<T>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{typeof(T).Name} Table Sync error: {e.Message}");
            }
            return null;
        }


        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await App.CloudClient.SyncContext.PushAsync();

                var parameters = new Dictionary<string, string>();
                var userId = App.User.Id;
                parameters.Add("memberId", App.User.Id);
                await this.table.PullAsync(nameof(T), this.table.CreateQuery().WithParameters(parameters));
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        // Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }

    }
}
