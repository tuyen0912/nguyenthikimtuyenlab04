using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nguyenthikimtuyen_lab04.Startup))]
namespace nguyenthikimtuyen_lab04
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
