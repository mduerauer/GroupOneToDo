using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GroupOneToDo.WebUI.Startup))]
namespace GroupOneToDo.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
