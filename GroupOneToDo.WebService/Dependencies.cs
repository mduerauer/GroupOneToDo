//#define MOCK

using GroupOneToDo.Service.Repository;
using Microsoft.Practices.Unity;
using System.Configuration;
using GroupOneToDo.WebCommons;
using GroupOneToDo.Service;

namespace GroupOneToDo.WebService
{
    public class Dependencies
    {

        public static void Register(IUnityContainer container)
        {
            // Mocked Repository
            #if MOCK
            container.RegisterSingleton<IToDoRepository, MockedToDoRepository>();
            container.RegisterType<IToDoService, MockedToDoService>();
            #else

            // DocumentDB Repository
            var endPointUri = GetCustomSetting("applicationSettings/AzureConnectionSettings",
                "EndPointURI");
            var primaryKey = GetCustomSetting("applicationSettings/AzureConnectionSettings",
                "PrimaryKey");
            var sendGridApiKey = GetCustomSetting("applicationSettings/AzureConnectionSettings",
                "SendGridApiKey");

            container.RegisterSingleton<IToDoRepository, DocumentDbToDoRepository>(new InjectionConstructor(endPointUri, primaryKey));

            var settings = new SendGridToDoSettings(sendGridApiKey);
            container.RegisterInstance<ISendGridToDoSettings>(settings);
            container.RegisterType<IToDoService, SendGridToDoService>();
            
            #endif
        }

        protected static string GetCustomSetting(string section, string setting)
        {
            var config = ConfigurationManager.GetSection(section);
            return ((ClientSettingsSection) config)?.Settings.Get(setting).Value.ValueXml.InnerText;
        }

    }
}