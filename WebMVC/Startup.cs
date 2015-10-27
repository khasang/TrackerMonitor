using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using WebMVC.Hubs;

[assembly: OwinStartupAttribute(typeof(WebMVC.Startup))]
namespace WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
