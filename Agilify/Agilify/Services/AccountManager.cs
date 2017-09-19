using System;
using System.Net.Http;
using System.Threading.Tasks;
using Agilify.Helpers;
using Agilify.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace Agilify.Services
{
    public class AccountManager
    {
        public static async Task<bool> Login(string provider)
        {
            var res = await App.Authenticator.Authenticate(provider);
            Settings.Provider = provider;

            if (res)
                return await AccountManager.RetrieveUserInfo();
            else
                return false;
        }

        public static async Task<bool> RetrieveUserInfo()
        {
            if (Settings.IsAuthenticated)
            {
                try
                {
                    App.User = await App.CloudClient.InvokeApiAsync<Member>("Account/User", HttpMethod.Get, null);
                    if (App.User != null)
                    {
                        Settings.UserEmail = App.User.Email;
                        Settings.UserName = App.User.Name;
                        Settings.UserImage = App.User.Image;
                        Settings.UserId = App.User.Id;
                        Settings.AuthId = App.CloudClient.CurrentUser.UserId;
                        Settings.AuthToken = App.CloudClient.CurrentUser.MobileServiceAuthenticationToken;

                        return true;
                    }
                    else
                        return false;

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;

                }

            }
            return false;

        }

        public static async Task<bool> LoadUser()
        {
            if (string.IsNullOrEmpty(Settings.UserEmail))
            {
                Settings.Provider = string.Empty;
                return false;
            }
            RefreshUser();
            App.User = new Member
            {
                Id = Settings.UserId,
                Email = Settings.UserEmail,
                Name = Settings.UserName,
                Image = Settings.UserImage
            };

            App.CloudClient.CurrentUser = new MobileServiceUser(Settings.AuthId)
            {
                MobileServiceAuthenticationToken = Settings.AuthToken
            };
            return true;
        }

        public static async void RefreshUser()
        {
            if (Settings.Provider == "Microsoft" || Settings.Provider == "Google")
            {
                bool res = await App.Authenticator.Refresh();

                if (res)
                {
                    Settings.AuthId = App.CloudClient.CurrentUser.UserId;
                    Settings.AuthToken = App.CloudClient.CurrentUser.MobileServiceAuthenticationToken;
                }
            }
        }

        public static async void SyncUserData()
        {
            var teamsStore = new SyncStore<Team>();
            var projectsStore = new SyncStore<Project>();

            var teams = await teamsStore.GetItemsAsync(true);
            var projects = await projectsStore.GetItemsAsync(true);

            if (teams != null)
                App.User.Teams.AddRange(teams);
            if (projects != null)
                App.User.Projects.AddRange(projects);
        }


    }
}