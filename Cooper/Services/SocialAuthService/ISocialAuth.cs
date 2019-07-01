using System;

namespace Cooper.Services
{
    public interface ISocialAuth
    {
        bool getCheckAuth(string provider, string token, string id);
    }
}
