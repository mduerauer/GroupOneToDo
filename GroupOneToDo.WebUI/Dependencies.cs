#define MOCK

using Microsoft.Practices.Unity;
using System.Configuration;
using GroupOneToDo.WebCommons;
using GroupOneToDo.Service.Repository;
using GroupOneToDo.WebUI.Controllers;

namespace GroupOneToDo.WebUI
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

            container.RegisterType<AccountController>(new InjectionConstructor());
        }

        protected static string GetCustomSetting(string section, string setting)
        {
            var config = ConfigurationManager.GetSection(section);
            return ((ClientSettingsSection) config)?.Settings.Get(setting).Value.ValueXml.InnerText;
        }

    }
}