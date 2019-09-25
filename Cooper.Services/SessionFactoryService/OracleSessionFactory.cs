using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Cooper.Services.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Cooper.Services
{
    public class OracleSessionFactory : ISessionFactory, IOracleSessionFactory
    {
        private const int defaultConnectionsAmount = 10;
        private readonly IConfigProvider configProvider;
        private Queue<ISession> sessions;

        public OracleSessionFactory(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
            sessions = new Queue<ISession>();

            for (int i = 0; i < defaultConnectionsAmount; i++)
            {
                sessions.Enqueue(new OracleSession(configProvider, this));
            }
        }

        public ISession FactoryMethod()
        {
            ISession session;

            if (!sessions.TryDequeue(out session))
            {
                session = new OracleSession(configProvider, this);
            }

            return session;
        }

        public void ReturnSession(ISession session)
        {
            sessions.Enqueue(session);
        }

    }
}
