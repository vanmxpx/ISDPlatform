using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Cooper.Services.Interfaces
{
    public interface ISession
    {
        void StartSession();
        void Commit(bool endSession);
        void Rollback(bool endSession);
        IDbTransaction GetTransaction();
        IDbConnection GetConnection();
    }
}
