using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReportsWebApp.Startup))]
namespace ReportsWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
