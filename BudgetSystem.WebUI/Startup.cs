using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetSystem.WebUI.Startup))]
namespace BudgetSystem.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
