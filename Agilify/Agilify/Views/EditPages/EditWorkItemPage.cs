using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Agilify.Services;
using Agilify.ViewModels;
using Syncfusion.SfKanban.XForms;
using Xamarin.Forms;

namespace Agilify.Views.EditPages
{
	public class EditWorkItemPage : EditItemPage<WorkItem>
	{
        public Entry WorkItemName { get; set; }
        public Entry WorkItemDescription { get; set; }
        public Picker WorkItemType { get; set; }
        public Picker WorkItemSprint { get; set; }
        public Button SaveButton { get; set; }

	    public Sprint Sprint { get; set; }
	    public int SprintIndex { get; set; }

	    public ObservableCollection<KanbanModel> Cards { get; set; }

        public EditWorkItemPage()
		{
            Title = "Create Work Item";

            WorkItemName = new Entry { Placeholder = "Name" };
            WorkItemDescription = new Entry { Placeholder = "Description" };
            WorkItemType = new Picker();
            WorkItemSprint = new Picker();


            WorkItemType.Items.Add("Type");
            WorkItemType.Items.Add("Task");
            WorkItemType.Items.Add("Bug");


            WorkItemType.SelectedIndexChanged += (sender, args) =>
            {
                if (WorkItemType.SelectedIndex == 0)
                {
                    SaveButton.IsEnabled = false;
                    return;
                }
                Item.Type = WorkItemType.SelectedIndex == 1 ? "Task" : "Bug";
                Item.Tags = Item.Type;
                SaveButton.IsEnabled = !string.IsNullOrWhiteSpace(WorkItemName.Text) ? true : false;
            };
            SaveButton = new Button { Text = "Save" };
            SaveButton.Clicked += async (sender, args) =>
            {
                if (WorkItemSprint.SelectedIndex != SprintIndex)
                {
                    var project = await App.SyncManager.ProjectsStore.GetItemAsync(Sprint.ProjectId);
                    var newSprint = project.Sprints.ElementAt(WorkItemSprint.SelectedIndex);

                    if (Sprint.WorkItems != null)
                    {
                        Sprint.WorkItems?.Remove(Item);
                        await App.SyncManager.SprintsStore.UpdateItemAsync(Sprint);
                    }
                   

                    Sprint = newSprint;
                    if (Sprint.WorkItems != null)
                    {
                        Sprint.WorkItems?.Remove(Item);
                        await App.SyncManager.SprintsStore.UpdateItemAsync(Sprint);
                    }
                    Item.SprintId = newSprint.Id;

                    await App.SyncManager.SprintsStore.UpdateItemAsync(Sprint);

                }

                await VM.Update(Item);
                Cards.Remove(Cards.First(m => m.ID == Math.Abs(Item.Id.GetHashCode())));
                if (WorkItemSprint.SelectedIndex == SprintIndex)
                    Cards.Add(Item);
                await Navigation.PopAsync();
            };


            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Children =
                {
                    WorkItemName,
                    WorkItemDescription,
                    WorkItemType,
                    WorkItemSprint,
                    SaveButton
                }
            };
        }

	    public EditWorkItemPage(WorkItem workItem, Sprint sprint, ItemsPageViewModel<WorkItem> vm, ObservableCollection<KanbanModel> cards) : this()
	    {
	        Item = workItem;
	        Sprint = sprint;
	        VM = vm;
	        Cards = cards;
	    }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Item.Owner == null)
                Item.Owner = App.User;

            BindingContext = Item;
            WorkItemName.SetBinding(Entry.TextProperty, "Name");
            WorkItemDescription.SetBinding(Entry.TextProperty, "Description");

            WorkItemType.SelectedIndex = Item.Type == "Task" ? 1 : 2;

            var project = await App.SyncManager.ProjectsStore.GetItemAsync(Sprint.ProjectId);
            foreach (var sprint in project.Sprints)
            {
                WorkItemSprint.Items.Add(sprint.Name);
            }
            var projectSprint = project.Sprints.FirstOrDefault(s => s.Id == Sprint.Id);
            SprintIndex = project.Sprints.IndexOf(projectSprint);
            WorkItemSprint.SelectedIndex = SprintIndex;


        }
    }
}
