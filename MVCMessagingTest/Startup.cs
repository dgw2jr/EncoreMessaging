using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCMessagingTest.Startup))]
namespace MVCMessagingTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
