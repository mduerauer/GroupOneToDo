using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using NLog;

namespace GroupOneToDo.WebCommons
{
    public class UnityResolver : IDependencyResolver
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IUnityContainer _container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            _container = container;
        }
        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException e)
            {
                Logger.Warn(e, "Can't resolve dependency due to exception: " + e.Message);
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException e)
            {
                Logger.Warn(e, "Can't resolve dependency due to exception: " + e.Message);
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}