using GroupOneToDo.Service.Repository;
using Microsoft.Practices.Unity;

namespace GroupOneToDo.WebService
{
    public class Dependencies
    {

        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IToDoRepository, MockedToDoRepository>();
        }

    }
}