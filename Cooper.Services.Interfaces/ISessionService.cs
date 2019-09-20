using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Cooper.Services.Interfaces
{
    public interface ISessionService
    {
        void StartSession();
        void EndSession();
        void Commit();
        void Rollback();
        IDbTransaction GetTransaction();
        IDbConnection GetConnection();
    }
}
