using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(UnisantaChat.Startup))]

namespace UnisantaChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.MapSignalR();
        }
    }
}