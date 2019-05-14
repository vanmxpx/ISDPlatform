using System;
using System.Net;
using System.Net.Mail;

namespace Cooper.Services
{
    class GmailSmtpClient : ISmtpClient
    {
        public string SmtpServerName {get;} = "smtp.gmail.com";
        
        public void SendMail(string from, string to, string password, string subject, string body, string code)
        {
            MailMessage msg = new MailMessage(from, to);
            msg.Subject = subject;
            msg.Body = $"{body}\nYour activate code: {code}";

            SmtpClient smtp = new SmtpClient(SmtpServerName, 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(from, password);
            smtp.Send(msg);
        }
    }
}
