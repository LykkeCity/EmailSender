using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amqp;
using EmailSender.Models;
using EmailSender.Settings;
using Newtonsoft.Json;

namespace EmailSender.Messages
{
    public class ServiceBusEmailSender : IEmailSender
    {
        private readonly IBaseSettings _settings;

        public ServiceBusEmailSender(IBaseSettings settings)
        {
            _settings = settings;
        }

        public void Send(Email email)
        {
            var nameSpace = _settings.ServiceBus.NamespaceUrl;
            var keyName = _settings.ServiceBus.KeyName;
            var keyValue = _settings.ServiceBus.KeyValue;
            var entity = _settings.ServiceBus.Entity;
            var port = _settings.ServiceBus.Port;

            var address = new Address(nameSpace, port, keyName, keyValue);
            var connection = new Connection(address);

            var amqpSession = new Session(connection);

            var sender = new SenderLink(amqpSession, "lykkestreams-emailsender", entity);

            var message = new Message(JsonConvert.SerializeObject(email));

            sender.Send(message);

            Console.WriteLine("Message sent to Service Bus");

            sender.Close();
            amqpSession.Close();
            connection.Close();
        }
    }
}
