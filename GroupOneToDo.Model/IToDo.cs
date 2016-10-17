using System;
using GroupOneToDo.Commons;

namespace GroupOneToDo.Model
{
    public interface IToDo : IAggregateRoot<Guid>
    {
        string Task { get; set; }

        DateTime DueDateTime { get; set; }

        string AssignedTo { get; set; }

        string CreatedBy { get; set; }

        DateTime CreatedWhen { get; set; }

    }
}
