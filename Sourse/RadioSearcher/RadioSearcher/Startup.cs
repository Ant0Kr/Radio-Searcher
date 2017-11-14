using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RadioSearcher.Startup))]
namespace RadioSearcher
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
