namespace EmailSender.Settings
{
    public interface IBaseSettings
    {
        AzureSettings Azure { get; set; }
        string TemplatesLink { get; set; }
    }

    public class BaseSettings : IBaseSettings
    {
        public AzureSettings Azure { get; set; }
        public string TemplatesLink { get; set; }
    }

    public class AzureSettings
    {
        public string LogsConnString { get; set; }
        public string EmailsQueueConnString { get; set; }
    }
}
