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
   public class Repository<T> where T : class
    {
        private string connectionString = "mongodb://localhost";
        private string databaseName = "myDatabase";
        private Mongo mongo;
        private MongoDatabase mongoDatabase;
        public IMongoCollection<T> mongoCollection;

        public Repository()
        {
            mongo = GetMongo(); 
            mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            mongoCollection= mongoDatabase.GetCollection<T>();
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
            mongoCollection.Insert(value, true);
        }

        public void Delete(Expression<Func<T, bool>> func)
        {
            mongoCollection.Remove<T>(func);
        }

        public void Update(T t, Expression<Func<T, bool>> func)
        {
            mongoCollection.Update(t, func, true);
        }

        public T Single(Expression<Func<T, bool>> func)
        {
            return mongoCollection.Linq().FirstOrDefault(func);
        }

        public List<T> List(int pageIndex, int pageSize, Expression<Func<T, bool>> func, out int pageCount)
        {
            pageCount = 0;
            pageCount = Convert.ToInt32(mongoCollection.Count());
            var personList = mongoCollection.Linq().Where(func).Skip(pageSize * (pageIndex - 1))
            .Take(pageSize).Select(i => i).ToList();
            mongo.Disconnect();
            return personList;
        }

        public List<T> List(Expression<Func<T, bool>> func)
        {
            var list = mongoCollection.Linq().Where(func).ToList();
            return list;
        }

        public IEnumerable<T> FindAll()
        {
            return mongoCollection.FindAll().Documents;
        }

        public void Connect()
        {
            try
            {
                mongo.Connect();
            }
            catch (MongoConnectionException exception)
            {
                throw exception;
            }
        }

        public void CloseConnect()
        {
            mongo.Disconnect();
        }

        public void InsertFile(string filename)
        {
            byte[] byteImg = File.ReadAllBytes(filename); 
            GridFile gridFile = new GridFile(mongoDatabase);
            using (GridFileStream gfs = gridFile.Create(filename))
            {
                gfs.Write(byteImg, 0, byteImg.Length);
            }
        }

        private byte[] GridFsRead(string filename)
        {
            GridFile gridFile = new GridFile(mongoDatabase);
            GridFileStream gridFileStream = gridFile.OpenRead(filename);
            byte[] bytes = new byte[gridFileStream.Length];
            gridFileStream.Read(bytes, 0, bytes.Length);
            return bytes;
        }  
   }
}
