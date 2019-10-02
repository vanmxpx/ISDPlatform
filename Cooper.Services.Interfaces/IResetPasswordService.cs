namespace Cooper.Services.Interfaces
{
    public interface IResetPasswordService
    {
        string CreateToken(string email);
    }
}