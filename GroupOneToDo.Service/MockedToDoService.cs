using System.Threading.Tasks;
using GroupOneToDo.Model;
using NLog;

namespace GroupOneToDo.Service
{
    public class MockedToDoService : IToDoService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task Notify(ToDo toDo, NotificationType notificationType)
        {
            Logger.Info("Would send notification of type {0} for ToDo with id {1}", notificationType, toDo.Id.ToString());

            await Task.CompletedTask;
        }

        public async Task NotifyAll()
        {
            Logger.Info("Would send notifications for all ToDos");

            await Task.CompletedTask;
        }
    }
}
