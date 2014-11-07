using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ATOMv0.Startup))]
namespace ATOMv0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
