namespace Cooper.Services.Interfaces
{
    public interface IJwtHandlerService
    {
        string GetPayloadAttributeValue(string attribute, string token);
    }
}
