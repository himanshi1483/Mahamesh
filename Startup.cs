using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mahamesh.Startup))]
namespace Mahamesh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
