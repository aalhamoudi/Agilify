using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Xamarin.Forms;

namespace Agilify.Views.CreatePages
{
	public class CreateWorkItemPage : CreateItemPage<WorkItem, Sprint>
	{
	    public Entry WorkItemName { get; set; }
	    public Entry WorkItemDescription { get; set; }
	    public Picker WorkItemType { get; set; }

        public CreateWorkItemPage()
        {
            Title = "Create Work Item";
            BindingContext = Item;
            Item.Owner = App.User;
            Item.Category = "New";

            WorkItemName = new Entry {Placeholder = "Name"};
            WorkItemDescription = new Entry {Placeholder = "Description"};
            WorkItemType = new Picker();

            WorkItemName.SetBinding(Entry.TextProperty, "Name");
            WorkItemDescription.SetBinding(Entry.TextProperty, "Description");

            WorkItemType.Items.Add("Select Type");
            WorkItemType.Items.Add("Task");
            WorkItemType.Items.Add("Bug");

            WorkItemType.SelectedIndex = 0;

            WorkItemType.SelectedIndexChanged += (sender, args) =>
            {
                if (WorkItemType.SelectedIndex == 0)
                {
                    CreateButton.IsEnabled = false;
                    return;
                }
                Item.Type = WorkItemType.SelectedIndex == 1 ? "Task" : "Bug";
                Item.Tags = Item.Type;
                CreateButton.IsEnabled = !string.IsNullOrWhiteSpace(WorkItemName.Text) ? true : false;
            };

            CreateButton.IsEnabled = false;

			Content = new StackLayout {
                Padding = new Thickness(10),
				Children =
                {
                    WorkItemName,
                    WorkItemDescription,
                    WorkItemType,
                    CreateButton
				}
			};
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        Item.SprintId = ParentElement.Id;

	        if (Item.Owner == null)
	            Item.Owner = App.User;
	    }
	}
}
