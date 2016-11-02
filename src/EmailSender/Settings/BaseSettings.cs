namespace EmailSender.Settings
{
    public interface IBaseSettings
    {
        AzureSettings Azure { get; set; }
        string TemplatesLink { get; set; }
        ServiceBusSettings ServiceBus { get; set; }
    }

    public class BaseSettings : IBaseSettings
    {
        public AzureSettings Azure { get; set; }
        public string TemplatesLink { get; set; }
        public ServiceBusSettings ServiceBus { get; set; }
    }

    public class AzureSettings
    {
        public string EmailsQueueConnString { get; set; }
    }

    public class ServiceBusSettings
    {
        public string NamespaceUrl { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        public string Entity { get; set; }
        public int Port { get; set; }
    }
}
