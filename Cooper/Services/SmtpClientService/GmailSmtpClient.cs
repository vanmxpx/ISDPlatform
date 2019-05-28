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
        private string url = "https://localhost:5001/confirm?token="; //TODO: get automatically server url
        
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
    }
}
