using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agilify.Helpers;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Net;

using Microsoft.WindowsAzure.MobileServices;

using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using Agilify.Models;
using Agilify.Services;
using Agilify.ViewModels;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Azure.Mobile;

namespace Agilify.Droid
{
    [Activity(Label = "Agilify", Theme = "@style/Theme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate, IMessagingClient
    {
        private MobileServiceUser user;

        public ChatMessageViewModel ChatViewModel { get; set; }

        public HubConnection Connection { get; set; }

        public IHubProxy Proxy { get; set; }

        public ConnectivityManager ConectivityManager { get; set; }

        public async Task<bool> Authenticate(string provider)
        {
            var message = string.Empty;
            bool success = false;

            try
            {
                switch (provider)
                {
                    case "Facebook":
                        user = await App.CloudClient.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
                        break;
                    case "Google":
                        user = await App.CloudClient.LoginAsync(this, MobileServiceAuthenticationProvider.Google, new Dictionary<string, string>() { { "access_type", "offline" } });
                        break;
                    case "Microsoft":
                        user = await App.CloudClient.LoginAsync(this, MobileServiceAuthenticationProvider.MicrosoftAccount);
                        break;
                    case "Twitter":
                        user = await App.CloudClient.LoginAsync(this, MobileServiceAuthenticationProvider.Twitter);
                        break;
                    case "Active Directory":
                        user = await App.CloudClient.LoginAsync(this, MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
                        break;
                }

                if (user != null)
                {
                    Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                    Settings.UserId = user?.UserId ?? string.Empty;
                    success = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return success;
        }

        public async Task<bool> Refresh()
        {
            var message = string.Empty;
            bool success = false;

            try
            {
                user = await App.CloudClient.RefreshUserAsync();
                
                if (user != null)
                {
                    Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                    Settings.AuthId = user?.UserId ?? string.Empty;
                    success = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return success;
        }
       
        public void Broadcast(ChatMessage message)
        {
            Proxy.Invoke("Send", message);
        }

        public void OnMessage(ChatMessage message)
        {
            ChatViewModel.Messages.Add(message);
        }

        public void SignalR()
        {
            Connection = new HubConnection(Constants.ApplicationURL);
            Proxy = Connection.CreateHubProxy("ChatHub");
            Connection.Start();
            Proxy.On<ChatMessage>("broadcastMessage", OnMessage);
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            ConectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);

            App.Init((IAuthenticate)this, (IMessagingClient)this, ConectivityManager.ActiveNetworkInfo.IsConnected);

            MobileCenter.Configure("3dddce50-2449-4503-929f-fdc1fe6a2b79");


            LoadApplication(new App());
        }
    }
}