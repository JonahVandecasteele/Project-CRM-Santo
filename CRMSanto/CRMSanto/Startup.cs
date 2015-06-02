using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRMSanto.Startup))]
namespace CRMSanto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
