using Microsoft.Practices.Unity;

namespace GroupOneToDo.WebCommons
{
    public static class UnityContainerExtensions
    {

        public static void RegisterSingleton<TFrom, TTo>(this IUnityContainer unityContainer) where TTo : TFrom
        {
            unityContainer.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }

        public static void RegisterSingleton<TFrom, TTo>(this IUnityContainer unityContainer, params InjectionMember[] members) where TTo : TFrom
        {
            unityContainer.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager(), members);
        }


    }
}
