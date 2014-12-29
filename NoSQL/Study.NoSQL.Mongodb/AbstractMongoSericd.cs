using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;
using MongoDB.Configuration;
using System.Linq.Expressions;

namespace Study.NoSQL.Mongodb
{
    public abstract class AbstractMongoSericd
    {
        private string connectionString = "mongodb://localhost";
        private string databaseName = "myDatabase";
        private string collectionName = "myCollection";
        private Mongo mongo;
        private MongoDatabase mongoDatabase;
        public MongoCollection<T> MongoCollection
        {
            get{ return mongoDatabase.GetCollection<T>(collectionName) as MongoCollection<T>;}
        }
        
        public AbstractMongoSericd(T type)
        {
            mongo = GetMongo(); 
            mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            //MongoCollection = mongoDatabase.GetCollection<T>(collectionName) as MongoCollection<T>;
            mongo.Connect();
        }

        private Mongo GetMongo()
        {
            var config = new MongoConfigurationBuilder();
            config.Mapping(mapping =>
            {
                mapping.DefaultProfile(profile =>
                {
                    profile.SubClassesAre(t => t.IsSubclassOf(typeof(T)));
                });
                mapping.Map<T>();
            });
            config.ConnectionString(connectionString);
            return new Mongo(config.BuildConfiguration());
        }

        public void Add(T value)
        {
            MongoCollection.Insert(value,true);
        }

        public void Delete(Expression<Func<T, bool>> func)
        {
            MongoCollection.Remove<T>(func);
        }

        public void Update<T>(T t, Expression<Func<T, bool>> func)
        {
            MongoCollection.Update(t, func, true);
        }

        public T Single(Expression<Func<T, bool>> func)
        {
           return MongoCollection.Linq().FirstOrDefault(func);
        }

        public List<T> List(int pageIndex, int pageSize, Expression<Func<T, bool>> func, out int pageCount)
        {
            pageCount = 0;
            pageCount = Convert.ToInt32(MongoCollection.Count());
            var personList = MongoCollection.Linq().Where(func).Skip(pageSize * (pageIndex - 1))
            .Take(pageSize).Select(i => i).ToList();
            mongo.Disconnect();
            return personList;
        }

        public List<T> List(Expression<Func<T, bool>> func)
        {
            var list = MongoCollection.Linq().Where(func).ToList();
            return list;
        }

        public IEnumerable<T> FindAll()
        {
            return MongoCollection.FindAll().Documents;
        }

        public void CloseConnect()
        {
            mongo.Disconnect();
        }
    }
}
