using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiCore
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            ConfigureWebApiCors(config);
            ConfigureWebApiRoutes(config);
            ConfigureWebApiFormatters(config);
            ConfigureCustom(config);
            config.SuppressDefaultHostAuthentication();

         
        }

        private static void ConfigureWebApiCors(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
        }

        private static void ConfigureWebApiRoutes(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void ConfigureWebApiFormatters(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }


        private static void ConfigureCustom(HttpConfiguration config)
        {
        }

    }
}
