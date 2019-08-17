using Cooper.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Cooper.Services
{
    class GmailSmtpClient : ISmtpClient
    {
        public string SmtpServerName {get;} = "smtp.gmail.com";

        private readonly string from;
        private readonly string password;
        private const string url = "https://cooper.serve.games/confirm?token=";

        public GmailSmtpClient(IConfigProvider configProvider) {
            from = configProvider.GmailProvider.From;
            password = configProvider.GmailProvider.Password;
        }
        
        public void SendMail(string to, string subject, string body, string token)
        {
            MailMessage msg = new MailMessage(from, to);
            msg.Subject = subject;
            msg.Body = $"{body}\nYour activation link: {url}{token}";

            SmtpClient smtp = new SmtpClient(SmtpServerName, 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(from, password);
            smtp.Send(msg);
        }

        public void SendMail(string to, string subject, string body)
        {
            MailMessage msg = new MailMessage(from, to);
            msg.Subject = subject;
            msg.Body = $"{body}";

            SmtpClient smtp = new SmtpClient(SmtpServerName, 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(from, password);
            smtp.Send(msg);
        }
    }
}
