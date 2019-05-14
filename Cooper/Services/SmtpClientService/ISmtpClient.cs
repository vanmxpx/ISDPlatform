using System;

namespace Cooper.Services
{
    public interface ISmtpClient
    {
        string SmtpServerName {get;}
        void SendMail(string from, string to, string password, string subject, string body, string code);
    }
}
