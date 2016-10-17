using EmailSender.AzureStorage.Tables;
using EmailSender.Common.Log;
using EmailSender.Settings;
using Microsoft.Practices.Unity;

namespace EmailSender.Config
{
    public class UnityConfig
    {
        public static IUnityContainer InitContainer(IBaseSettings settings)
        {
            var container = new UnityContainer();

            container.RegisterInstance(settings);

            RegisterLogs(container, settings);

            RegisterJobs(container);

            return container;
        }

        private static void RegisterJobs(IUnityContainer container)
        {
            container.RegisterType<EmailsQueueReader>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterLogs(IUnityContainer container, IBaseSettings settings)
        {
            var logToTable = new LogToTable(new AzureTableStorage<LogEntity>(settings.Azure.LogsConnString, "EmailsQueueReader", null));

            container.RegisterInstance(logToTable);
            container.RegisterType<LogToConsole>();

            container.RegisterType<ILog, LogToTableAndConsole>();

            return container;
        }
    }
}
