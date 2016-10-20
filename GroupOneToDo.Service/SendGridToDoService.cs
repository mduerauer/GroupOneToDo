using GroupOneToDo.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupOneToDo.Model;
using SendGrid.Helpers.Mail;
using System.Configuration;

namespace GroupOneToDo.Service
{
    public class SendGridToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;

        private readonly string _apiKey;

        public SendGridToDoService(IToDoRepository repository, ISendGridToDoSettings settings)
        {
            _repository = repository;
            _apiKey = settings.ApiKey;
        }

        public async Task Notify(ToDo toDo, NotificationType notificationType)
        {

            var sg = new SendGrid.SendGridAPIClient(_apiKey);

            Email from = new Email("gruppe1_fhstp@outlook.com");
            string subject = string.Format("Todo NotificationType {1}", toDo.Id.ToString(), notificationType.ToString());
            Email to = new Email(toDo.AssignedTo);
            Content content = new Content("text/plain", string.Format("Task: {0}, zu erledigen bis: {1}", toDo.Task, toDo.DueDateTime.ToString()));
            Mail mail = new Mail(from, subject, to, content);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());

        }

        public async Task NotifyAll()
        {
            var todos = await _repository.FindAll();
            foreach(var todo in todos)
            {
                await Notify(todo, NotificationType.ToDoReminder);
            }

        }

        public static string GetCustomSetting(string section, string setting)
        {
            var config = ConfigurationManager.GetSection(section);
            return ((ClientSettingsSection)config)?.Settings.Get(setting).Value.ValueXml.InnerText;
        }
    }

    
}
