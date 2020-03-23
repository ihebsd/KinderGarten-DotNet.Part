using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Solution.Web.Startup))]
namespace Solution.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
