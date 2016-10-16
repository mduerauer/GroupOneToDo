using GroupOneToDo.Model;
using System.Threading.Tasks;

namespace GroupOneToDo.Service
{
    public interface IToDoService
    {

        Task Notify(ToDo toDo, NotificationType notificationType);

        Task NotifyAll();

    }
}
