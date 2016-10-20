//#define MOCK

using Microsoft.Practices.Unity;
using System.Configuration;
using GroupOneToDo.Service.Repository;
using GroupOneToDo.WebUI.Controllers;
using GroupOneToDo.WebCommons;

namespace GroupOneToDo.WebUI
{
    public class Dependencies
    {

        public static readonly string BaseUrl = "https://fhstp-mis16-gr1-api.azurewebsites.net/api/todo";

        public static void Register(IUnityContainer container)
        {
            // Mocked Repository
#if MOCK
            container.RegisterSingleton<IToDoRepository, MockedToDoRepository>();
#else

            // WebAPI Repository
            container.RegisterSingleton<IToDoRepository, WebApiToDoRepository>(new InjectionConstructor(BaseUrl));

            // DocumentDB Repository
            /*
            var endPointUri = GetCustomSetting("applicationSettings/AzureConnectionSettings",
                "EndPointURI");
            var primaryKey = GetCustomSetting("applicationSettings/AzureConnectionSettings",
                "PrimaryKey");

            container.RegisterSingleton<IToDoRepository, DocumentDbToDoRepository>(new InjectionConstructor(endPointUri, primaryKey));
            */

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