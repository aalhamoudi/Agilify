using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agilify.Models;
using Agilify.Views.CreatePages;
using Agilify.Views.DetailPages;
using Agilify.Views.EditPages;
using Syncfusion.SfKanban.XForms;
using Xamarin.Forms;

namespace Agilify.Views.ListPages
{
    public class WorkItemsPage : BacklogPage<WorkItem, Sprint, WorkItemDetailPage, CreateWorkItemPage, EditWorkItemPage>
    {
        public WorkItemsPage(Sprint sprint) : base(w => w.SprintId == sprint.Id)
        {
            ParentElement = sprint;
            Title = sprint.Name;

            Board.AutoGenerateColumns = false;
            Board.Columns.Add(new KanbanColumn { Title = "New", Categories = new List<object>() { "New" } });
            Board.Columns.Add(new KanbanColumn { Title = "Approved", Categories = new List<object>() { "Approved" } });
            Board.Columns.Add(new KanbanColumn { Title = "Commited", Categories = new List<object>() { "Commited" } });
            Board.Columns.Add(new KanbanColumn { Title = "Done", Categories = new List<object>() { "Done" } });

            Board.ItemTapped += (sender, args) =>
            {
                var item = VM.Items.FirstOrDefault(e => Math.Abs(e.Id.GetHashCode()) == (args.Data as KanbanModel).ID);
                if (item != null)
                {
                    Navigation.PushAsync(new EditWorkItemPage(item, ParentElement, VM, Cards));
                    
                }
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
    }
}
