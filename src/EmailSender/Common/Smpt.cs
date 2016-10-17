using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EmailSender.Common
{
    public class SmtpSenderSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string LocalDomain { get; set; }
    }

    public class MySmptSender
    {
        private SmtpSenderSettings _smtpSenderSettings;

        public MySmptSender(SmtpSenderSettings smtpSenderSettings)
        {
            _smtpSenderSettings = smtpSenderSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_smtpSenderSettings.DisplayName, _smtpSenderSettings.From));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            var body = new TextPart("html") { Text = message };

            //var attachmentData = new MemoryStream(Encoding.UTF8.GetBytes("MyText"));


            //var attachment = new MimePart("text", "txt")
            //{
            //    ContentObject = new ContentObject(attachmentData),
            //    FileName = "mytext.txt"
            //};

            //var multipart = new Multipart("mixed");
            //multipart.Add(body);
            //multipart.Add(attachment);

            //emailMessage.Body = multipart;
            emailMessage.Body = body;

            using (var client = new SmtpClient())
            {
                client.LocalDomain = _smtpSenderSettings.LocalDomain;

                await client.ConnectAsync(_smtpSenderSettings.Host, _smtpSenderSettings.Port, SecureSocketOptions.None).ConfigureAwait(false);
                await client.AuthenticateAsync(_smtpSenderSettings.Login, _smtpSenderSettings.Password);

                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }

        }
    }


}