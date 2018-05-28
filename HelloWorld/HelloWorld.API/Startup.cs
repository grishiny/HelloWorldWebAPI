using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HelloWorld.API.Startup))]

namespace HelloWorld.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
