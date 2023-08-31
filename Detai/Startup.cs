using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Detai.Startup))]
namespace Detai
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
