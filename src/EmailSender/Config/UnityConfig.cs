using EmailSender.AzureStorage.Tables;
using EmailSender.Common.Log;
using EmailSender.Messages;
using EmailSender.Settings;
using Microsoft.Practices.Unity;

namespace EmailSender.Config
{
    public class UnityConfig
    {
        public static IUnityContainer InitContainer(IBaseSettings settings, string logsConnString)
        {
            var container = new UnityContainer();

            container.RegisterInstance(settings);

            RegisterLogs(container, settings, logsConnString);

            RegisterServices(container, settings);

            RegisterJobs(container);

            return container;
        }

        private static void RegisterJobs(IUnityContainer container)
        {
            container.RegisterType<EmailsQueueReader>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterLogs(IUnityContainer container, IBaseSettings settings, string logsConnString)
        {
            var logToTable = new LogToTable(new AzureTableStorage<LogEntity>(logsConnString, "EmailsQueueReader", null));

            container.RegisterInstance(logToTable);
            container.RegisterType<LogToConsole>();

            container.RegisterType<ILog, LogToTableAndConsole>();

            return container;
        }

        private static void RegisterServices(IUnityContainer container, IBaseSettings settings)
        {
            container.RegisterType<IEmailSender, ServiceBusEmailSender>();
            container.RegisterType<MessageHandler>(new ContainerControlledLifetimeManager());
        }
    }
}
