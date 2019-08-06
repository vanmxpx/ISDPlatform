using System;
using System.Collections.Generic;
using Cooper.Services.Interfaces;

namespace Cooper.ORM
{
    public class EntityORM
    {
        public Dictionary<string, object> attributeValue {get;set;}

        public EntityORM()
        {
            attributeValue = new Dictionary<string, object>();
        }

    }
}