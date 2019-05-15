using System;
using System.Net;
using System.Net.Mail;

namespace Cooper.Services
{
    class GmailSmtpClient : ISmtpClient
    {
        public string SmtpServerName {get;} = "smtp.gmail.com";

        private string from;
        private string password;
        
        public void SendMail(string to, string subject, string body, string token)
        {
            MailMessage msg = new MailMessage(from, to);
            msg.Subject = subject;
            msg.Body = $"{body}\nYour activate code: {token}";

            SmtpClient smtp = new SmtpClient(SmtpServerName, 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(from, password);
            smtp.Send(msg);
        }
    }
}
