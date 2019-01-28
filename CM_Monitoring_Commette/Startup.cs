using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CM_Monitoring_Commette.Startup))]
namespace CM_Monitoring_Commette
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
