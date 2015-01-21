using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.MongoDB.Core
{
    public class MongoDbLocalRepository<T> : MongoDBaseService<T> where T : class
    {
        public override MondodbConfig GetMongoDbConfig()
        {
            return new MondodbConfig
            {
                ConnectionString = "mongodb://10.10.11.13:27101",
                TargetDb = "OpcDb",
            };
        }
    }
}
