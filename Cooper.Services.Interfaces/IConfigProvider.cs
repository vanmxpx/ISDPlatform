namespace Cooper.Services.Interfaces
{
    public interface IConfigProvider
    {
        IConnectionStrings ConnectionStrings { get; set; }
        IProvider FacebookProvider { get; set; }
        ISmtp GmailProvider { get; set; }
        IMediaserverConf MediaserverConf { get; set; }
        IJwtToken JwtToken { get; set; }

    }
}
