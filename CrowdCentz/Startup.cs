using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrowdCentz.Startup))]
namespace CrowdCentz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
