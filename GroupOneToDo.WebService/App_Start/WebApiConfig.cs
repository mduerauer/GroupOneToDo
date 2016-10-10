using System.Net.Http.Headers;
using System.Web.Http;
using GroupOneToDo.WebCommons;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;

namespace GroupOneToDo.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IUnityContainer container)
        {
            config.MapHttpAttributeRoutes(new RestDirectRouteProvider());

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
