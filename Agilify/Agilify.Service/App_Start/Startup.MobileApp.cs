using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Http;
using AgilifyService.Accounts;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using AgilifyService.Data;
using AgilifyService.Models;
using Owin;

namespace AgilifyService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            app.CreatePerOwinContext<AgilifyContext>(AgilifyContext.Create);
            app.CreatePerOwinContext<AgilifyUserManager>(AgilifyUserManager.Create);

            config.MapHttpAttributeRoutes();

            config.EnableSystemDiagnosticsTracing();

            config.EnableCors();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            Database.SetInitializer(new AgilifyInitializer());

            //var migrator = new DbMigrator(new AgilifyService.Migrations.Configuration());
            //migrator.Update();

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            config.Routes.MapHttpRoute("API", "api/{controller}/{action}");

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;

            app.UseWebApi(config);

            
        }
    }

    public class AgilifyInitializer : DropCreateDatabaseIfModelChanges<AgilifyContext>
    {
        protected override void Seed(AgilifyContext context)
        {
            base.Seed(context);
        }
    }
}

