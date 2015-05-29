using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EIDTest.Startup))]
namespace EIDTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
