namespace Cooper.Services.Interfaces
{
    public interface IJwtToken
    {
         string Issuer { get; set; }
         string Audience { get; set; }
         string Key { get; set; }
         int Lifetime { get; set; }
    }
}
