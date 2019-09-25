using System;
using System.Collections.Generic;
using System.Text;

namespace Cooper.Services.Interfaces
{
    public interface ISessionFactory
    {
        ISession FactoryMethod();
    }
}
