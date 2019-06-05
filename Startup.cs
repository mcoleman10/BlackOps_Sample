using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlattSampleApp.Startup))]
namespace PlattSampleApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
