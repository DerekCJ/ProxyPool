using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProxyPool.Startup))]
namespace ProxyPool
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
