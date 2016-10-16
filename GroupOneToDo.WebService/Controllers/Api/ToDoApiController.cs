﻿using System;
using System.Web.Http;
using GroupOneToDo.Commons;
using GroupOneToDo.Model;
using GroupOneToDo.Service.Repository;
using NLog;
using GroupOneToDo.Service;

namespace GroupOneToDo.WebService.Controllers.Api
{
    [RoutePrefix("api/todo")]
    public class ToDoApiController : RestControllerBase<ToDo, Guid>
    {

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

    }
}
