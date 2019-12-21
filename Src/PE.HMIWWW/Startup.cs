using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PE.HMIWWW.Startup))]
namespace PE.HMIWWW
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
      app.MapSignalR();
    }
  }
}
