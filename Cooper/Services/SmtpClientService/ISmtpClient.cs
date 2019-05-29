using System;

namespace Cooper.Services
{
    public interface ISmtpClient
    {
        string SmtpServerName {get;}
        void SendMail(string to, string subject, string body, string token);
    }
}
