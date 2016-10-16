using GroupOneToDo.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupOneToDo.Model;
using SendGrid.Helpers.Mail;

namespace GroupOneToDo.Service
{
    public class SendGridToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;

        private readonly string _sendGridApiKey;

        private SendGridToDoService(IToDoRepository repository, string sendGridApiKey)
        {
            this._repository = repository;
            this._sendGridApiKey = sendGridApiKey;
        }

        public async Task Notify(ToDo toDo, NotificationType notificationType)
        {

            var sg = new SendGrid.SendGridAPIClient(_sendGridApiKey);

            Email from = new Email("gruppe1_fhstp@outlook.com");
            string subject = "Sending with SendGrid is Fun";
            Email to = new Email("markus.duerauer@feuerwehr.gv.at");
            Content content = new Content("text/plain", "and easy to do anywhere, even with C#");
            Mail mail = new Mail(from, subject, to, content);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());

        }

        public Task NotifyAll()
        {
            throw new NotImplementedException();
        }
    }
}
