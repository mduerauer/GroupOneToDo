using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupOneToDo.Service
{
    public class SendGridToDoSettings : ISendGridToDoSettings
    {
        public string ApiKey { get; set; }

        public SendGridToDoSettings(string apiKey)
        {
            this.ApiKey = apiKey;
        }

    }
}
