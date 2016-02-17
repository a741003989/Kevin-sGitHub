using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KEVIN.MVC.STORE.Startup))]
namespace KEVIN.MVC.STORE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
