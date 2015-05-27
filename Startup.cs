using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EAIFMVC.Startup))]
namespace EAIFMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
