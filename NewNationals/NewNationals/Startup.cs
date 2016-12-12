using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewNationals.Startup))]
namespace NewNationals
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
