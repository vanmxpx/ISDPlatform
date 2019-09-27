using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Data;
using Cooper.Services.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System.Threading;

namespace Cooper.Services
{
    public class OracleSessionFactory : ISessionFactory, IOracleSessionFactory
    {
        private const int defaultConnectionsAmount = 10;
        private const int maxConnectionsAmount = 100;
        private readonly IConfigProvider configProvider;
        private int connectionsAmount = defaultConnectionsAmount;
        private ConcurrentQueue<ISession> sessions;

        public OracleSessionFactory(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
            sessions = new ConcurrentQueue<ISession>();

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
                if (connectionsAmount < maxConnectionsAmount)
                {
                    session = new OracleSession(configProvider, this);
                    connectionsAmount++;
                }
                else
                {
                    while (sessions.Count == 0)
                    {
                        Thread.Sleep(100);
                    }
                    return FactoryMethod();
                }
            }

            return session;
        }

        public void ReturnSession(ISession session)
        {
            sessions.Enqueue(session);
        }

    }
}
