using System;
using System.Collections.Generic;
using MyBatis.DataMapper;
using MyBatis.DataMapper.Configuration;
using MyBatis.DataMapper.Configuration.Interpreters.Config.Xml;
using MyBatis.DataMapper.Session;

namespace Study.MyBatis
{
    public class MyBatisSingleton
    {
        private static string databaseSelector = null;
        private static MyBatisSingleton instance = null;
        private static MyBatisSingleton Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            InitDbDictionary(databaseSelector);//访问XML文件 初始化有效的数据库
                            instance = new MyBatisSingleton();
                        }
                    }
                }
                return instance;
            }
        }
        private static IDictionary<string, IDataMapper> dbDictionary = new Dictionary<string, IDataMapper>();
        private static IDictionary<string, ISession> TranSFactory = new Dictionary<string, ISession>();
        private static readonly object syncRoot = new object();

        private MyBatisSingleton()
        {
            //使用Private使本类为单例模式              
        }

        public static MyBatisSingleton GetInstance(string databaseSelect)
        {
            databaseSelector = databaseSelect;
            return Current;
        }

        private static void InitDbDictionary(string databaseSelect)
        {
            ItemList list = XmlConvertClass.GetItemList(databaseSelect);
            foreach (Item it in list.Items)
            {
                if (it.Enable.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        ISessionFactory factory = null;
                        IDataMapper mapper = InitMybatis(it.Path, ref factory);
                        dbDictionary.Add(it.Name, mapper);
                        TranSFactory.Add(it.Name, factory.OpenSession());
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        private static IDataMapper InitMybatis(string path, ref ISessionFactory factory)
        {
            string config = databaseSelector + path;//MyBatis的Config文件
            IConfigurationEngine engine = new DefaultConfigurationEngine();
            try
            {
                engine.RegisterInterpreter(new XmlConfigurationInterpreter(config));
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            IMapperFactory mapperFactory = engine.BuildMapperFactory();
            factory = engine.ModelStore.SessionFactory;
            IDataMapper dataMapper = ((IDataMapperAccessor)mapperFactory).DataMapper;
            return dataMapper;
        }

        public static IDataMapper GetDatabaseMapper(string DbType)
        {
            if (dbDictionary.ContainsKey(DbType))
            {
                return dbDictionary[DbType];
            }
            else
            {
                return null;
            }
        }

        public static ISession GetTransaction(string projectName)
        {
            if (TranSFactory.ContainsKey(projectName))
            {
                return TranSFactory[projectName];
            }
            else
            {
                return null;
            }
        }
    }
}
