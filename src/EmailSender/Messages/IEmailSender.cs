using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailSender.Models;

namespace EmailSender.Messages
{
    public interface IEmailSender
    {
        void Send(Email email);
    }
}
