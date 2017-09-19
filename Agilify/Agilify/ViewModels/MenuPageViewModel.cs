using System;
using System.Collections.Generic;
using System.Text;

using Agilify.Helpers;
using Agilify.Models;
using System.Collections;
using Agilify.Services;
using Xamarin.Forms;
using Agilify.Views;
using Agilify.Views.ApplicationPages;
using Agilify.Views.ListPages;

namespace Agilify.ViewModels
{
    public class MenuPageViewModel
    {
        public Member User { get; set; }
        public IEnumerable<NavDrawerItem> MenuItems { get; set; }

        public MenuPageViewModel()
        {
            if (App.User != null)
                User = App.User;
            else
            {
                User = new Member
                {
                    Image = "ic_account",
                    Name = "Name",
                    Email = "Email"
                };
            }
            

            MenuItems = new List<NavDrawerItem>
            {
                //new NavDrawerItem
                //{
                //    Icon = "ic_dashboard",
                //    Title = "Dashboard",
                //    Page = new DashboardPage()
                //},
                 new NavDrawerItem
                {
                    Icon = "ic_team",
                    Title = "Teams",
                    Page = new TeamsPage()
                },
                new NavDrawerItem
                {
                    Icon = "ic_project",
                    Title = "Projects",
                    Page = new ProjectsPage()
                },
                new NavDrawerItem
                {
                    Icon = "ic_calender",
                    Title = "Deadlines",
                    Page = new DeadlinesPage()
                },
                //new NavDrawerItem
                //{
                //    Icon = "ic_chat",
                //    Title = "Chat",
                //    Page = new ChatPage()
                //},
                new NavDrawerItem
                {
                    Icon = "ic_settings",
                    Title = "Settings",
                    Page = new SettingsPage()
                },
                //new NavDrawerItem
                //{
                //    Icon = "ic_account",
                //    Title = "Account",
                //    Page = new AccountPage()
                //},
                
            };

        }

        public class NavDrawerItem
        {
            public string Icon { get; set; }
            public string Title { get; set; }

            public Page Page { get; set; }
        }
    }
}
