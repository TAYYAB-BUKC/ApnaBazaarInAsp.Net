using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApnaBazaar.Web.Startup))]
namespace ApnaBazaar.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
