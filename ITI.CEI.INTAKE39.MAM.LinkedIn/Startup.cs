using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITI.CEI.INTAKE39.MAM.LinkedIn.Startup))]
namespace ITI.CEI.INTAKE39.MAM.LinkedIn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
