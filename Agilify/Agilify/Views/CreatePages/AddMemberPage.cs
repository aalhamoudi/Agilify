using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Agilify.Models;
using Agilify.ViewModels;
using Agilify.Views.DetailPages;
using Agilify.Views.ListPages;
using Xamarin.Forms;

namespace Agilify.Views.CreatePages
{
    public class AddMemberPage : CreateItemPage<Member, Team>
    {
        public Entry MembersEmail { get; set; }
        public Button AddButton { get; set; }
        public ItemsPageViewModel<Team> ParentVM { get; set; }
        public TeamDetailPage ParentPage { get; set; }
        public MembersPage MembersPage { get; set; }

        public AddMemberPage()
        {
            Title = "Add Member";

            MembersEmail = new Entry
            {
                Placeholder = "Email"
            };

            AddButton = new Button
            {
                Text = "Add Member",
                IsEnabled = false
            };

            MembersEmail.TextChanged += (sender, args) =>
            {
                
                if (Regex.IsMatch(MembersEmail.Text,
                    @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                    RegexOptions.IgnoreCase))
                    AddButton.IsEnabled = true;
                else
                {
                    AddButton.IsEnabled = false;
                }
            };

            AddButton.Clicked += OnAddMember;

            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Children =
                {
                    MembersEmail,
                    AddButton
                }
            };
        }

        public AddMemberPage(Team parentElement, ItemsPageViewModel<Team> parentVM, TeamDetailPage parentPage, ItemsPageViewModel<Member> vm, MembersPage page) : this()
        {
            VM = vm;
            ParentElement = parentElement;
            ParentVM = parentVM;
            ParentPage = parentPage;
            MembersPage = page;
        }

        private async void OnAddMember(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("teamId", ParentElement?.Id);
            parameters.Add("email", MembersEmail.Text);
            bool res = false;
            try
            {
                res = await App.CloudClient.InvokeApiAsync<bool>("Operations/AddMember", HttpMethod.Get, parameters);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            if (!res)
                await DisplayAlert("Not Found", "No user found with provided email address", "OK");
            else
            {
                await ParentPage.VM.LoadItems();
                var item = await ParentPage.VM.GetItem(ParentPage.Item.Id);
                ParentPage.Item = item;
                MembersPage.Team = item;

                VM.UpdateFilter(m => item.Members.Select(tm => tm.Id).Contains(m.Id));
                await VM.LoadItems();
                
                await Navigation.PopAsync();
            }
        }
    }
}
