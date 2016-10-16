using System;
using Newtonsoft.Json;

namespace GroupOneToDo.Model
{
    public class ToDo : IToDo
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public string Task { get; set; }

        public DateTime DueDateTime { get; set; }

        public User AssignedTo { get; set; }

        public User CreatedBy { get; set; }

        public DateTime CreatedWhen { get; set; }


        public ToDo() { }


        public ToDo(Guid id)
        {
            Id = id;
        }

    }
}
