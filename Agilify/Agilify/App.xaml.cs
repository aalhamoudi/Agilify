using Agilify.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;
using Agilify.Helpers;
using Newtonsoft.Json.Linq;
using Agilify.Models;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Agilify.Services;
using Agilify.Views.ApplicationPages;

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Agilify
{
    public partial class App : Application
    {
        public static MobileServiceClient CloudClient { get; private set; }
        public static Member User { get; set; }
        public static IAuthenticate Authenticator { get; private set; }
        public static IMessagingClient MessagingClient { get; set; }
        public static JObject UserProfile { get; set; }
        public static MobileServiceSQLiteStore LocalDB { get; set; }
        public static SyncManager SyncManager { get; set; }
        public static bool IsOnline { get; set; }
        public App()
        {
            InitializeComponent();

            CloudClient = new MobileServiceClient(Constants.ApplicationURL);
            LocalDB = new MobileServiceSQLiteStore(Constants.OfflineDbPath);
            SyncManager = new SyncManager();;

            App.CloudClient.SyncContext.InitializeAsync(App.LocalDB);


            SetMainPage();

        }

        protected override void OnStart()
        {
            base.OnStart();
            MobileCenter.Start(typeof(Analytics), typeof(Crashes));

        }

        protected override void OnResume()
        {
            base.OnResume();
            AccountManager.RefreshUser();
        }

        public static void Init(IAuthenticate authenticator, IMessagingClient messagingClient, bool isOnline)
        {
            Authenticator = authenticator;
            MessagingClient = messagingClient;
            IsOnline = isOnline;
        }


        public static async void SetMainPage()
        {
            if (Settings.IsAuthenticated)
            {
                var res = await AccountManager.LoadUser();
                if (res)
                {
                    AccountManager.SyncUserData();
                    Current.MainPage = new RootPage();
                }
                else
                {
                    SetMainPage();
                }
            }
            else
                Current.MainPage = new NavigationPage(new LoginPage() { BackgroundColor = Color.FromHex("07212C") });
        }

    }
}
