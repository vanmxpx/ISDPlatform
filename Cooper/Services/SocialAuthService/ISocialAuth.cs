using System;

namespace Cooper.Services
{
    public interface ISocialAuth
    {
        bool IsFacebookAuth(string token, string user_id);
        bool IsGoogleAuth(string idToken, string email);
    }
}