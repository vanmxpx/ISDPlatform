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
        private ConcurrentQueue<ManualResetEvent> eventsQueue;
        private Dictionary<ManualResetEvent, ISession> reservedSessions;

        public OracleSessionFactory(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;

            sessions = new ConcurrentQueue<ISession>();
            reservedSessions = new Dictionary<ManualResetEvent, ISession>();
            eventsQueue = new ConcurrentQueue<ManualResetEvent>();

            for (int i = 0; i < defaultConnectionsAmount; i++)
            {
                sessions.Enqueue(new OracleSession(configProvider, this));
            }
        }

        public ISession FactoryMethod()
        {

            if (!sessions.TryDequeue(out ISession session))
            {
                if (connectionsAmount < maxConnectionsAmount)
                {
                    session = new OracleSession(configProvider, this);
                    connectionsAmount++;
                }
                else
                {
                    ManualResetEvent evObj = new ManualResetEvent(false);

                    eventsQueue.Enqueue(evObj);
                    evObj.WaitOne();

                    session = reservedSessions[evObj];
                    reservedSessions.Remove(evObj);
                    evObj.Close();

                }
            }

            return session;
        }

        public void ReturnSession(ISession session)
        {
            if (eventsQueue.TryDequeue(out ManualResetEvent evObj))
            {
                reservedSessions.Add(evObj, session);

                evObj.Set();
            }
            else
            {
                sessions.Enqueue(session);
            }
        }

    }
}
