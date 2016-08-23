using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QandaQuizNet.Startup))]
namespace QandaQuizNet
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
