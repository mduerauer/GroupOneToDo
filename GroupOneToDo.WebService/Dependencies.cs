#define MOCK

using GroupOneToDo.Service.Repository;
using Microsoft.Practices.Unity;
using System.Configuration;
using GroupOneToDo.WebCommons;

namespace GroupOneToDo.WebService
{
    public class Dependencies
    {

        public static void Register(IUnityContainer container)
        {
            // Mocked Repository
            #if MOCK
            container.RegisterSingleton<IToDoRepository, MockedToDoRepository>();
            #else

            // DocumentDB Repository
            var endPointUri = GetCustomSetting("applicationSettings/AzureConnectionSettings",
                "EndPointURI");
            var primaryKey = GetCustomSetting("applicationSettings/AzureConnectionSettings",
                "PrimaryKey");

            container.RegisterSingleton<IToDoRepository, DocumentDbToDoRepository>(new InjectionConstructor(endPointUri, primaryKey));
            #endif
        }

        protected static string GetCustomSetting(string section, string setting)
        {
            var config = ConfigurationManager.GetSection(section);
            return ((ClientSettingsSection) config)?.Settings.Get(setting).Value.ValueXml.InnerText;
        }

    }
}