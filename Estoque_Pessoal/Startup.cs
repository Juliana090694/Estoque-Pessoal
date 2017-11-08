using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Estoque_Pessoal.Startup))]
namespace Estoque_Pessoal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
