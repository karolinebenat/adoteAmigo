using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdoteUmAmigo.Startup))]
namespace AdoteUmAmigo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
