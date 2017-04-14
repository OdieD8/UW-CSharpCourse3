using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FullMVCSite.Startup))]
namespace FullMVCSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
