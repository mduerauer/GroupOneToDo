using System;
using System.Web.Http;
using GroupOneToDo.Commons;
using GroupOneToDo.Model;
using GroupOneToDo.Service.Repository;

namespace GroupOneToDo.WebService.Controllers.Api
{
    [RoutePrefix("api/todo")]
    public class ToDoApiController : RestControllerBase<ToDo, Guid>
    {
        // Das Repository wird per Unity Dependency Injection eingefügt
        private readonly IToDoRepository _repository;
        public override IAsyncRepository<ToDo, Guid> Repository => _repository;

        public ToDoApiController(IToDoRepository repository)
        {
            _repository = repository;
        }

    }
}
