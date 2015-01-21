using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Linq.Expressions;
using System.Collections.Generic;
using MongoDB;
using MongoDB.Configuration;
using MongoDB.GridFS;

namespace Study.MongoDB.Core
{
    public class MongoDbRemoteRepository<T> : MongoDBaseService<T> where T : class
    {
        public override MondodbConfig GetMongoDbConfig()
        {
            return new MondodbConfig
            {
                ConnectionString = "mongodb://10.10.13.37:27200",
                TargetDb = "OpcDb",
            };
        }
    }
}
