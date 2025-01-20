using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Requirement.Startup))]
namespace Requirement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
