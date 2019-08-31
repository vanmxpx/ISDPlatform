namespace Cooper.Services.Interfaces
{
    public interface IConfigProvider
    {
        IConnectionStrings ConnectionStrings { get; set; }
        IJwtToken JwtToken { get; set; }
        IProvider FacebookProvider { get; set; }
        ISmtp GmailProvider { get; set; }
    }
}
