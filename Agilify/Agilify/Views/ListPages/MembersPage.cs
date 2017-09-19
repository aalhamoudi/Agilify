using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Agilify.Helpers;
using Agilify.Models;
using Agilify.ViewModels;
using Agilify.Views.CreatePages;
using Agilify.Views.DetailPages;
using Agilify.Views.EditPages;
using Xamarin.Forms;

namespace Agilify.Views.ListPages
{
    public class MembersPage : ItemsListPage<Member, Team, MemberDetailPage, AddMemberPage, EditMemberPage>
    {
        public Team Team { get; set; }
        public ItemsPageViewModel<Team> ParentVM { get; set; }
        public TeamDetailPage ParentPage { get; set; }
        public MembersPage(Func<Member, bool> filter = null) : base(filter)
        {

            Title = "Members";

            ItemsList.ItemTemplate = new DataTemplate(() =>
            {
                Image icon = new Image();
                Label nameLabel = new Label();

                icon.Source = "ic_account";

                nameLabel.SetBinding(Label.TextProperty, "Name");
                nameLabel.FontSize = 18;

                var template =  new ViewCell
                {
                    View = new StackLayout
                    {
                        Padding = new Thickness(10, 5),
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            icon,
                            new StackLayout
                            {
                                Padding = new Thickness(10, 0),
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                Children =
                                {
                                    nameLabel
                                }
                            }
                        }
                    }
                };

                var editMenuItem = new MenuItem { Text = "Edit", Command = EditCommand };
                var deleteMenuItem = new MenuItem { Text = "Delete", Command = DeleteCommand, IsDestructive = true };

                deleteMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
                editMenuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

                //template.ContextActions.Add(EditMenuItem);
                if (Team.Owner.Id.Equals(App.User.Id))
                    template.ContextActions.Add(deleteMenuItem);

                return template;
            });

        }

        public MembersPage(Team team, ItemsPageViewModel<Team> parentVm, TeamDetailPage parentPage) : this(m => team.Members.Select(tm => tm.Id).Contains(m.Id))
        {
            Team = team;
            ParentElement = team;
            ParentVM = parentVm;
            ParentPage = parentPage;
        }

        protected override void OnAdd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddMemberPage(ParentElement, ParentVM, ParentPage, VM, this));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public override void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        public override void OnItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        protected override async Task OnDelete(Member item)
        {
            var confirm = await DisplayAlert("Delete", $"Delete {item.Name}?", "Yes", "No");

            if (confirm)
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("teamId", ParentElement?.Id);
                parameters.Add("email", item.Email);
                bool res = false;
                try
                {
                    res = await App.CloudClient.InvokeApiAsync<bool>("Operations/DeleteMember", HttpMethod.Delete, parameters);

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
                if (res)
                {
                    await ParentVM.LoadItems();
                    Team = await ParentVM.GetItem(ParentElement.Id);
                    VM.UpdateFilter(m => Team.Members.Select(tm => tm.Id).Contains(m.Id));
                    await VM.LoadItems();
                }

            }
        }
    }
}
