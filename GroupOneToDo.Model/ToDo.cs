using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GroupOneToDo.Model
{
    public class ToDo : IToDo
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [Display(Name = "Aufgabe")]
        public string Task { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Zu erledigen bis")]
        public DateTime DueDateTime { get; set; }


        [Display(Name = "Zu erledigen von")]
        public string AssignedTo { get; set; }


        [Display(Name = "Erstellt von")]
        public string CreatedBy { get; set; }


        [Display(Name = "Erstellt")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime CreatedWhen { get; set; }


        [Display(Name = "Erledigt")]
        public bool Done { get; set; }


        public ToDo() { }


        public ToDo(Guid id)
        {
            Id = id;
        }

    }
}
