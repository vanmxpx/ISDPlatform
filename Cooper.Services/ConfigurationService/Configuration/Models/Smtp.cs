using Cooper.Services.Interfaces;

namespace Cooper.Services
{
    public class Smtp : ISmtp
    {
        public string From { get; set; }
        public string Password { get; set; }
    }
}
