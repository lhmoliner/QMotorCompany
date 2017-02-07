using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuadrusMotorCompany.Startup))]
namespace QuadrusMotorCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
