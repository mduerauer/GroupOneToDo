using System;
using System.Collections.Generic;
using GroupOneToDo.Commons;
using GroupOneToDo.Model;

namespace GroupOneToDo.Service.Repository
{
    public class MockedToDoRepository : IToDoRepository
    {

        private readonly IDictionary<Guid, ToDo> _data;

        public MockedToDoRepository()
        {
            _data = new Dictionary<Guid, ToDo>();

            for (var i = 0; i < 10; i++)
            {
                Save(MakeTodo(i));
            }
        }

        private ToDo MakeTodo(int c)
        {
            var user = new User("user " + c%2);

            var todo = new ToDo(Guid.NewGuid())
            {
                CreatedWhen = new DateTime(),
                Task = "A simple task " + c,
                DueDateTime = DateTime.Now.AddDays(c%3),
                AssignedTo = user,
                CreatedBy = user
            };

            return todo;
        }

        public ToDo GetById(Guid id)
        {
            return _data.ContainsKey(id) ? _data[id] : null;
        }

        public ICollection<ToDo> FindAll()
        {
            return _data.Values;
        }

        public ToDo DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(ToDo entity)
        {
            _data.Add(entity.Id, entity);
        }
    }
}
