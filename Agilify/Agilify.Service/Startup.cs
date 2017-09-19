using AgilifyService.Accounts;
using AgilifyService.Data;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AgilifyService.Startup))]

namespace AgilifyService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<AgilifyContext>(AgilifyContext.Create);
            app.CreatePerOwinContext<AgilifyUserManager>(AgilifyUserManager.Create);

            ConfigureMobileApp(app);
            app.MapSignalR();

            
        }
    }
}