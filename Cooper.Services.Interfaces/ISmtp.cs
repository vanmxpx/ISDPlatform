namespace Cooper.Services.Interfaces
{
    public interface ISmtp
    {
        string From { get; set; }
        string Password { get; set; }
    }
}