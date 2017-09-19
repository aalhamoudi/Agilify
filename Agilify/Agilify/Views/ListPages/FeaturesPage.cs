using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Agilify.Helpers;
using Xamarin.Forms;

using Agilify.Models;
using Agilify.Services;
using Agilify.ViewModels;
using Agilify.Views.CreatePages;
using Agilify.Views.DetailPages;
using Agilify.Views.EditPages;
using Syncfusion.SfKanban.XForms;

namespace Agilify.Views.ListPages
{
    public class FeaturesPage : BacklogPage<Feature, Epic, FeatureDetailPage, CreateFeaturePage, EditFeaturePage>
    {
        public ItemsPageViewModel<Epic> ParentVM { get; set; }

        public FeaturesPage(Epic epic, ItemsPageViewModel<Epic> parentVm) : base(f => f.EpicId == epic.Id)
        {
            ParentElement = epic;
            ParentVM = parentVm;

            Board.AutoGenerateColumns = false;
            Board.Columns.Add(new KanbanColumn { Title = "New", Categories = new List<object>() { "New" } });
            Board.Columns.Add(new KanbanColumn { Title = "In Progress", Categories = new List<object>() { "In Progress" } });
            Board.Columns.Add(new KanbanColumn { Title = "Done", Categories = new List<object>() { "Done" } });

            var edit = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Icon = "ic_action_edit"
            };

            var delete = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Priority = 2,
                Icon = "ic_action_delete"
            };

            edit.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new EditEpicPage(ParentElement, ParentVM, Cards));
            };

            delete.Clicked += async (sender, args) =>
            {
                var res = await DisplayAlert("Delete", $"Do you really want to delete {ParentElement.Name}?", "Yes", "No");
                if (res)
                {
                    await ParentVM.Delete(ParentElement);
                    await Navigation.PopAsync();
                }
                
            }; 

            ToolbarItems.Add(edit);
            ToolbarItems.Add(delete);

            Board.ItemTapped += (sender, args) =>
            {
                var item = VM.Items.FirstOrDefault(e => Math.Abs(e.Id.GetHashCode()) == (args.Data as KanbanModel).ID);
                if (item != null)
                    Navigation.PushAsync(new EditFeaturePage(item, VM, Cards));
            };
            Board.DragEnd += async (sender, args) =>
            {
                var item = VM.Items.FirstOrDefault(e => Math.Abs(e.Id.GetHashCode()) == (args.Data as KanbanModel).ID);
                if (!item?.Category.Equals(args.TargetCategory.ToString()) ?? false)
                {
                    item.Category = args.TargetCategory.ToString();
                    await VM.Update(item);
                }
            };
        }
        
        protected override async void OnAppearing()
        {
            Title = ParentElement.Name;
            base.OnAppearing();
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
