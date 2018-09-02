using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IARTAutomationApp.Startup))]
namespace IARTAutomationApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
