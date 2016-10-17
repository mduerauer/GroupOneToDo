using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GroupOneToDo.Model
{
    public class ToDo : IToDo
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public string Task { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDateTime { get; set; }
        
        public string AssignedTo { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedWhen { get; set; }


        public ToDo() { }


        public ToDo(Guid id)
        {
            Id = id;
        }

    }
}
