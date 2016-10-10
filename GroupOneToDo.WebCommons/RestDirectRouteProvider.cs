using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace GroupOneToDo.WebCommons
{
    public class RestDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>
            (inherit: true);
        }

    }
}