using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ColonIA.Startup))]
namespace ColonIA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
