using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailSender.Models;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Net;
using EmailSender.Messages;
using EmailSender.Settings;

namespace EmailSender
{
    public class MessageHandler
    {
        private readonly IBaseSettings _settings;
        private readonly IEmailSender _emailSender;

        public MessageHandler(IBaseSettings settings, IEmailSender emailSender)
        {
            _settings = settings;
            _emailSender = emailSender;
        }

        public async Task<bool> HandleMessage(CloudQueueMessage message)
        {
            var messageString = message.AsString;

            var words = messageString.Split(new[] { ':' }, 2);
            var messageType = words[0];
            var messageObjectString = words[1];

            if (messageType == "CompetitionPlatformMail")
            {
                var jObject = JsonConvert.DeserializeObject<JObject>(messageObjectString);

                var emailType = jObject["Data"]["MessageData"]["Subject"].ToString();

                switch (emailType)
                {
                    case "Initiative":
                        var initiativeMessage = JsonConvert.DeserializeObject<InitiativeEmail>(messageObjectString);
                        await SendInitiative(initiativeMessage);
                        return true;
                    case "Competition":
                        var competitionMessage = JsonConvert.DeserializeObject<CompetitionEmail>(messageObjectString);
                        await SendCompetition(competitionMessage);
                        return true;
                    case "ImplementationFollower":
                        var implementationFollowerMessage =
                            JsonConvert.DeserializeObject<ImplementationEmail>(messageObjectString);
                        await SendImplementation(implementationFollowerMessage);
                        return true;
                    case "ImplementationParticipant":
                        var implementationParticipantMessage =
                            JsonConvert.DeserializeObject<ImplementationEmail>(messageObjectString);
                        await SendImplementation(implementationParticipantMessage);
                        return true;
                    case "Voting":
                        var votingMessage = JsonConvert.DeserializeObject<VotingEmail>(messageObjectString);
                        await SendVoting(votingMessage);
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }

        private async Task SendInitiative(InitiativeEmail plainTextEmail)
        {
            var html = await GetHtmlTemplate(plainTextEmail.Data.MessageData.Subject);
            var initiativeModel = plainTextEmail.Data.MessageData.Model;

            var emailBody = InsertData(html, initiativeModel);

            var email = new Email
            {
                To = new List<string> { plainTextEmail.Data.EmailAddress },
                Subject = plainTextEmail.Data.MessageData.Subject,
                IsHtml = true,
                Body = emailBody

            };

            _emailSender.Send(email);
        }

        private async Task SendCompetition(CompetitionEmail plainTextEmail)
        {
            var html = await GetHtmlTemplate(plainTextEmail.Data.MessageData.Subject);
            var competitionModel = plainTextEmail.Data.MessageData.Model;

            var emailBody = InsertData(html, competitionModel);

            var email = new Email
            {
                To = new List<string> { plainTextEmail.Data.EmailAddress },
                Subject = plainTextEmail.Data.MessageData.Subject,
                IsHtml = true,
                Body = emailBody

            };

            _emailSender.Send(email);
        }

        private async Task SendImplementation(ImplementationEmail plainTextEmail)
        {
            var html = await GetHtmlTemplate(plainTextEmail.Data.MessageData.Subject);
            var implementationModel = plainTextEmail.Data.MessageData.Model;

            var emailBody = InsertData(html, implementationModel);

            var email = new Email
            {
                To = new List<string> { plainTextEmail.Data.EmailAddress },
                Subject = plainTextEmail.Data.MessageData.Subject,
                IsHtml = true,
                Body = emailBody

            };

            _emailSender.Send(email);
        }

        private async Task SendVoting(VotingEmail plainTextEmail)
        {
            var html = await GetHtmlTemplate(plainTextEmail.Data.MessageData.Subject);
            var votingModel = plainTextEmail.Data.MessageData.Model;

            var emailBody = InsertData(html, votingModel);

            var email = new Email
            {
                To = new List<string> { plainTextEmail.Data.EmailAddress },
                Subject = plainTextEmail.Data.MessageData.Subject,
                IsHtml = true,
                Body = emailBody

            };

            _emailSender.Send(email);
        }

        private async Task<string> GetHtmlTemplate(string templateName)
        {
            var req = WebRequest.Create(_settings.TemplatesLink + templateName + ".html");
            req.Method = "GET";

            string source;

            var response = await req.GetResponseAsync();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                source = reader.ReadToEnd();
            }

            return source;
        }

        private string InsertData<T>(string emailTemplate, T templateVm)
        {
            var sb = new StringBuilder(emailTemplate);

            foreach (var prop in templateVm.GetType().GetProperties())
            {
                // in the email template, placeholders look like this: @[propertyName]
                if (prop.GetValue(templateVm, null) != null)
                    sb.Replace("@[" + prop.Name + "]", prop.GetValue(templateVm, null).ToString());
            }

            return sb.ToString();
        }
    }
}
