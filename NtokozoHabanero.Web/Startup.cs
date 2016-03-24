using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NtokozoHabanero.Web.Startup))]
namespace NtokozoHabanero.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
