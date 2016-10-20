using System;
using System.Web.Http;
using GroupOneToDo.Commons;
using GroupOneToDo.Model;
using GroupOneToDo.Service.Repository;
using NLog;
using GroupOneToDo.Service;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace GroupOneToDo.WebService.Controllers.Api
{
    [RoutePrefix("api/todo")]
    public class ToDoApiController : RestControllerBase<ToDo, Guid>
    {

        public static readonly string NotificationToken = "test123";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // Das Repository wird per Unity Dependency Injection eingefügt
        private readonly IToDoRepository _repository;

        private readonly IToDoService _service;

        public override IAsyncRepository<ToDo, Guid> Repository => _repository;

        public ToDoApiController(IToDoRepository repository, IToDoService service)
        {
            _repository = repository;
            _service = service;
        }


        public override void AfterInsert(ToDo entity)
        {
            Logger.Debug("AfterInsert called().");

            // Nach dem Erstellen eines ToDos sofort eMail-Notification verschicken.
            _service.Notify(entity, NotificationType.ToDoCreated);
        }

        public override void AfterUpdate(ToDo entity)
        {
            Logger.Debug("AfterUpdate called().");

            // Nach dem Erstellen eines ToDos sofort eMail-Notification verschicken.
            _service.Notify(entity, NotificationType.ToDoChanged);
        }

        public override void AfterDelete(ToDo entity)
        {
            Logger.Debug("AfterDelete called().");

            // Nach dem Löschen eines ToDos sofort eMail-Notification verschicken.
            _service.Notify(entity, NotificationType.ToDoRemoved);
        }


        [HttpGet]
        [Route("_notifyAll")]
        public async Task<HttpResponseMessage> NotifyAll(string token)
        {
            HttpResponseMessage response;

            // TODO: Check if apiKey is valid
            if (!token.Equals(NotificationToken))
            {
                response = Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Token not valid.");
                return response;
            }

            try
            {
                await _service.NotifyAll();

                response = Request.CreateResponse(HttpStatusCode.OK, "ok");
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }

            return response;
        }

    }
}
