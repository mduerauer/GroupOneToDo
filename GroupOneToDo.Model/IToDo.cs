using System;
using GroupOneToDo.Commons;

namespace GroupOneToDo.Model
{
    public interface IToDo : IAggregateRoot<Guid>
    {
        string Task { get; set; }

        DateTime DueDateTime { get; set; }

        IPrincipal AssignedTo { get; set; }

        IPrincipal CreatedBy { get; set; }

        DateTime CreatedWhen { get; set; }

    }
}
