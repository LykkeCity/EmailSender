using System.Threading.Tasks;
using EmailSender.Common;
using EmailSender.Common.Log;
using EmailSender.Settings;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace EmailSender
{
    public class EmailsQueueReader : TimerPeriod
    {
        private const int TimerPeriodSeconds = 3;

        private readonly IBaseSettings _settings;
        private readonly CloudQueue _queue;
        private readonly MessageHandler _messageHandler;

        public EmailsQueueReader(IBaseSettings settings, ILog logger, MessageHandler messageHandler) : this("EmailsQueueReader", TimerPeriodSeconds * 1000, logger)
        {
            _settings = settings;

            var storageAccount = CloudStorageAccount.Parse(settings.Azure.EmailsQueueConnString);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("emailsqueue");
            _queue = queue;
            _messageHandler = messageHandler;
        }

        private EmailsQueueReader(string componentName, int periodMs, ILog log)
                : base(componentName, periodMs, log)
        {
        }

        protected override async Task Execute()
        {
            var queueData = await _queue.GetMessageAsync();

            while (queueData != null)
            {
                var result = await _messageHandler.HandleMessage(queueData);
                if (result)
                    await _queue.DeleteMessageAsync(queueData);

                queueData = await _queue.GetMessageAsync();
            }
        }
    }
}
