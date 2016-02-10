using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FormBuilderApp.Startup))]
namespace FormBuilderApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
