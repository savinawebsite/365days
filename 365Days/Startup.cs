using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_365Days.Startup))]
namespace _365Days
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
