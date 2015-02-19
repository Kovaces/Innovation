using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Innovation.Web.Startup))]
namespace Innovation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
