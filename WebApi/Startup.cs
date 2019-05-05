using System.Net;
using System.Web.Http;
using System.Web.Http.Tracing;
using Owin;

namespace WebApiHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                    "DefaultApi",
                    "api/{controller}/{id}",
                    new { id = RouteParameter.Optional }
                );

            config.InitializeCustomWebHooks();
            config.InitializeCustomWebHooksApis();

            var listener = (HttpListener)appBuilder.Properties["System.Net.HttpListener"];
            listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;

            var traceWriter = config.EnableSystemDiagnosticsTracing();
            traceWriter.IsVerbose = true;
            traceWriter.MinimumLevel = TraceLevel.Error;

            appBuilder.UseWebApi(config);
        }
    }
}