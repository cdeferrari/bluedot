using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Administracion.Startup))]
namespace Administracion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
