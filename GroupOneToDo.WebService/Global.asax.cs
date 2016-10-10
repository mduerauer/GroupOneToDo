using System.Web.Http;
using Microsoft.Practices.Unity;

namespace GroupOneToDo.WebService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IUnityContainer _container;

        public IUnityContainer Container => _container;

        protected void Application_Start()
        {
            CreateContainer();

            GlobalConfiguration.Configure(x => WebApiConfig.Register(x, _container));
        }

        protected virtual void CreateContainer()
        {
            _container = new UnityContainer();
            Dependencies.Register(_container);
        }
    }
}
