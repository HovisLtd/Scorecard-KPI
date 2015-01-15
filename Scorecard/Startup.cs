using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Scorecard.Startup))]
namespace Scorecard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
