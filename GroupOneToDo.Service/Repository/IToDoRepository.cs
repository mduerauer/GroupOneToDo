using System;
using GroupOneToDo.Commons;
using GroupOneToDo.Model;

namespace GroupOneToDo.Service.Repository
{
    public interface IToDoRepository : IAsyncRepository<ToDo, Guid>
    {

    }
}
