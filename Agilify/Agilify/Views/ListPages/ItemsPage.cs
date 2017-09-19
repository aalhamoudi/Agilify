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

namespace Agilify.Views.ListPages
{
    public class ItemsPage<T, P, D, C, E> : ContentPage where T : IItem, new() where P : IItem, new() where C : CreateItemPage<T, P>, new() where D : ItemDetailPage<T>, new() where E : EditItemPage<T>, new()
    {
        public ItemsPageViewModel<T> VM { get; set; }
        public P ParentElement { get; set; }
        public Command EditCommand { get; set;}
        public Command DeleteCommand { get; set; }
        public MenuItem EditMenuItem { get; set; }
        public MenuItem DeleteMenuItem { get; set; }


        public ItemsPage()
        {
            BindingContext = VM = new ItemsPageViewModel<T>();

            EditCommand = new Command(async (object item) => await OnEdit((T)item));
            DeleteCommand = new Command(async (object item) => await OnDelete((T)item));

            var add = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                Icon = "ic_action_add"
            };


            add.Clicked += OnAdd;

            ToolbarItems.Add(add);



            EditMenuItem = new MenuItem
            {
                Text = "Edit",
                Command = EditCommand,

            };

            DeleteMenuItem = new MenuItem
            {
                Text = "Delete",
                //Command = DeleteCommand,
                IsDestructive = true
            };

            

            EditMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            DeleteMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

            

        }

        public ItemsPage(P parent) : this()
        {
            ParentElement = parent;
        }
        public ItemsPage(Expression<Func<T, bool>> query) : this()
        {
            BindingContext = VM = new ItemsPageViewModel<T>(query, true);
        }

        public ItemsPage(Func<T, bool> filter) : this()
        {
            BindingContext = VM = new ItemsPageViewModel<T>(filter);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.LoadDataCommand.Execute(new { });
        }

        protected virtual void OnAdd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new C { VM = VM, ParentElement = ParentElement});
        }


        protected virtual async Task OnEdit(T item)
        {
            await Navigation.PushAsync(new E { Item = item, VM = VM });
        }

        protected virtual async Task OnDelete(T item)
        {
            var confirm = await DisplayAlert("Delete", $"Delete {item.Name}?", "Yes", "No");

            if (confirm)
                await VM.Delete(item);
        }

        public virtual void OnItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        public virtual async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            ((ListView)sender).SelectedItem = null;

            await Navigation.PushAsync(new D { Item = (T)e.SelectedItem });

        }
    }
}
