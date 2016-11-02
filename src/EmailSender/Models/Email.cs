using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSender.Models
{
    public class Email
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public bool IsHtml { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }
    }

    public class Attachment
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string Base64Value { get; set; }
    }
}
