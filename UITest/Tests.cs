using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            if (app == null)
                app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("Login Screen");
        }

        [Test]
        public void MicrosoftLogin()
        {
            app.Tap(b => b.Button("Microsoft"));
            app.EnterText(x => x.Class("WebView").Css("INPUT#i0116"), "agilify@outlook.com");
            app.Tap(x => x.Class("WebView").Css("INPUT#idSIButton9"));
            app.Tap(x => x.Class("WebView").Css("INPUT#i0118"));
            app.EnterText(x => x.Class("WebView").Css("INPUT#i0118"), "@gilify0");
            app.ScrollDown();
            app.Tap(x => x.Class("WebView").Css("INPUT#idSIButton9"));
            //app.Tap(x => x.Class("WebView").Css("INPUT#idBtn_Accept"));
            app.Screenshot("Microsoft Login");
        }

        [Test]
        public void CreateTeam()
        {
            // Arrange
            MicrosoftLogin();

            // Act
            app.Tap(x => x.Class("ActionMenuItemView"));
            app.Tap(x => x.Class("EntryEditText"));
            app.EnterText(x => x.Class("EntryEditText"), "Team");
            app.Tap(x => x.Text("Create"));

            // Assert
            app.WaitForElement(x => x.Text("Team"));
        }

        [Test]
        public void DeleteTeam()
        {
            CreateTeam();

            app.TouchAndHold(x => x.Text("Team"));
            app.Tap(x => x.Marked("Delete"));
            app.Tap(x => x.Id("button1"));
        }

        [Test]
        public void CreateProject()
        {
            CreateTeam();

            app.Tap(x => x.Text("Team"));
            app.Tap(x => x.Class("ActionMenuItemView"));
            app.Tap(x => x.Class("EntryEditText"));
            app.EnterText(x => x.Class("EntryEditText"), "Project");
            app.Tap(x => x.Class("EntryEditText").Index(1));
            app.EnterText(x => x.Class("EntryEditText").Index(1), "Project Description");
            app.Tap(x => x.Text("Create"));


        }

    }
}

