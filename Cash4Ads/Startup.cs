using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cash4Ads.Startup))]
namespace Cash4Ads
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
