﻿using System;
using System.Collections.Generic;
using GroupOneToDo.Model;
using System.Threading.Tasks;

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
                Create(MakeTodo(i));
            }
        }

        private static ToDo MakeTodo(int c)
        {

            var todo = new ToDo()
            {
                Id = Guid.NewGuid(),
                CreatedWhen = DateTime.Now.AddDays(-1),
                Task = "A simple task " + c,
                DueDateTime = DateTime.Now.AddDays(c%3),
                AssignedTo = "user " + c % 2,
                CreatedBy = "user " + c % 2
            };

            return todo;
        }

        public async Task<ToDo> GetById(Guid id)
        {
            return await Task.FromResult<ToDo>(_data.ContainsKey(id) ? _data[id] : null);
        }

        public async Task<ICollection<ToDo>> FindAll()
        {
            return await Task.FromResult<ICollection<ToDo>>(_data.Values);
        }

        public async Task<ToDo> DeleteById(Guid id)
        {
            var result = await GetById(id);
    
            if(result != null) { 
                _data.Remove(id);
                return await Task.FromResult<ToDo>(result);
            }

            return await Task.FromResult<ToDo>(null);
        }

        public async Task<ToDo> Create(ToDo entity)
        {
            _data.Add(entity.Id, entity);
            return await Task.FromResult<ToDo>(entity);
        }

        public async Task<ToDo> Update(ToDo entity)
        {
            _data[entity.Id] = entity;
            return await Task.FromResult<ToDo>(entity);
        }
    }
}
