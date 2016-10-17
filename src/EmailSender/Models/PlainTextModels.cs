namespace EmailSender.Models
{
    public class PlainTextEmail
    {
        public EmailData Data { get; set; }
    }

    public class EmailData
    {
        public string EmailAddress { get; set; }
        public EmailMessageData MessageData { get; set; }
    }

    public class EmailMessageData
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }

    public class CompetitionEmail
    {
        public CompetitionEmailData Data { get; set; }
    }

    public class CompetitionEmailData
    {
        public string EmailAddress { get; set; }
        public CompetitionEmailMessageData MessageData { get; set; }
    }

    public class CompetitionEmailMessageData
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public Competition Model { get; set; }
    }

    public class InitiativeEmail
    {
        public InitiativeEmailData Data { get; set; }
    }

    public class InitiativeEmailData
    {
        public string EmailAddress { get; set; }
        public InitiativeEmailMessageData MessageData { get; set; }
    }

    public class InitiativeEmailMessageData
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public Initiative Model { get; set; }
    }

    public class ImplementationEmail
    {
        public ImplementationEmailData Data { get; set; }
    }

    public class ImplementationEmailData
    {
        public string EmailAddress { get; set; }
        public ImplementationEmailMessageData MessageData { get; set; }
    }

    public class ImplementationEmailMessageData
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public Implementation Model { get; set; }
    }

    public class VotingEmail
    {
        public VotingEmailData Data { get; set; }
    }

    public class VotingEmailData
    {
        public string EmailAddress { get; set; }
        public VotingEmailMessageData MessageData { get; set; }
    }

    public class VotingEmailMessageData
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public Voting Model { get; set; }
    }
}
