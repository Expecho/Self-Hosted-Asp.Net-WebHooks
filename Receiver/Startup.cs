using System.Web.Http;
using System.Web.Http.Tracing;
using Owin;

namespace Receiver
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            _ = new CustomWebHookHandler();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                    "DefaultApi",
                    "api/{controller}/{id}",
                    new { id = RouteParameter.Optional }
                );

            config.InitializeReceiveCustomWebHooks();
        
            config.EnsureInitialized();

            var traceWriter = config.EnableSystemDiagnosticsTracing();
            traceWriter.IsVerbose = true;
            traceWriter.MinimumLevel = TraceLevel.Info;

            appBuilder.UseWebApi(config);
        }
    }
}