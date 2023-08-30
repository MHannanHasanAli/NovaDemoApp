using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImperialNova.Startup))]
namespace ImperialNova
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }
    }
}
