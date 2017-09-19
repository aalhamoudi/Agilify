using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Agilify.Helpers;
using Xamarin.Forms;

using Agilify.Models;
using Agilify.Services;
using Agilify.Views.CreatePages;
using Agilify.Views.DetailPages;
using Agilify.Views.EditPages;
using Syncfusion.SfKanban.XForms;

namespace Agilify.Views.ListPages
{
    public class EpicsPage : BacklogPage<Epic, Project, EpicDetailPage, CreateEpicPage, EditEpicPage>
    {
        public EpicsPage(Project project) : base(e => e.ProjectId?.Equals(project.Id) ?? false)
        {
            ParentElement = project;
            Title = "Epics";

            Board.AutoGenerateColumns = false;
            Board.Columns.Add(new KanbanColumn { Title = "New", Categories = new List<object>() { "New" } });
            Board.Columns.Add(new KanbanColumn { Title = "In Progress", Categories = new List<object>() { "In Progress" } });
            Board.Columns.Add(new KanbanColumn { Title = "Done", Categories = new List<object>() { "Done" } });

            Board.ItemTapped += (sender, args) =>
            {
                var item = VM.Items.FirstOrDefault(e => Math.Abs(e.Id.GetHashCode()) == (args.Data as KanbanModel).ID);
                if (item != null)
                    Navigation.PushAsync(new FeaturesPage(item, VM));
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
