using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BackEndChoNhan.Startup))]
namespace BackEndChoNhan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
