using MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.NoSQL.Mongodb
{
   public class MongoDBTest
    {
        public void TestCode()
        {
            //链接字符串
            string connectionString = "mongodb://localhost";

            //数据库名
            string databaseName = "myDatabase";

            //集合名
            string collectionName = "myCollection";
            
            //定义Mongo服务
            Mongo mongo = new Mongo(connectionString);

            //获取databaseName对应的数据库，不存在则自动创建
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;

            //获取collectionName对应的集合，不存在则自动创建
            MongoCollection<T> mongoCollection = mongoDatabase.GetCollection<T>(collectionName) as MongoCollection<T>;
            
            //链接数据库
            mongo.Connect();
            try
            {
                //定义一个文档对象，存入两个键值对
                T doc = new T();
                doc["ID"] = 1;
                doc["Msg"] = "Hello World!";

                //将这个文档对象插入集合
                mongoCollection.Insert(doc);

                //在集合中查找键值对为ID=1的文档对象
                T docFind = mongoCollection.FindOne(new T { { "ID", 1 } });
               
                //输出查找到的文档对象中键“Msg”对应的值，并输出
                Console.WriteLine(Convert.ToString(docFind["Msg"]));
            }
            finally
            {
                //关闭链接
                mongo.Disconnect();
            }
        }
    }
}
