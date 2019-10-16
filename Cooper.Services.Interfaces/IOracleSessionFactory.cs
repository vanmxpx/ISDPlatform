using System;
using System.Collections.Generic;
using System.Text;

namespace Cooper.Services.Interfaces
{
    public interface IOracleSessionFactory
    {
        void ReturnSession(ISession session);
    }
}
