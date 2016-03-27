using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GradedUnitMaster.Startup))]
namespace GradedUnitMaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
