using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CapV4.Startup))]
namespace CapV4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
