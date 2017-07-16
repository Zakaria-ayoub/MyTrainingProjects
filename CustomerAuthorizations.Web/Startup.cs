using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerAuthorizations.Web.Startup))]
namespace CustomerAuthorizations.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
