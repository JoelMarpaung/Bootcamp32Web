using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bootcamp32Web.Startup))]
namespace Bootcamp32Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
