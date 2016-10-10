using System;

namespace GroupOneToDo.Model
{
    public class ToDo : IToDo
    {
        public Guid Id { get; }
        public string Task { get; set; }
        public DateTime DueDateTime { get; set; }
        public IPrincipal AssignedTo { get; set; }
        public IPrincipal CreatedBy { get; set; }
        public DateTime CreatedWhen { get; set; }

        public ToDo(Guid id)
        {
            Id = id;
        }

        public ToDo()
        {
            Id = Guid.NewGuid();
        }
    }
}
