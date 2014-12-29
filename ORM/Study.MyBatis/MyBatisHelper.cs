using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBatis.Common.Data;
using MyBatis.Common.Resources;
using MyBatis.Common.Utilities;
using MyBatis.DataMapper;
using MyBatis.DataMapper.Configuration;
using MyBatis.DataMapper.Configuration.Interpreters.Config.Xml;
using MyBatis.DataMapper.Session;
using MyBatis.Common.Logging;
using System.Reflection;

namespace Study.MyBatis
{
    public class MyBatisHelper
    {
        #region 参数
        protected  IDataMapper dataMapper = null;
        protected  ISessionFactory sessionFactory = null;
        private  ISession session;
        #endregion

        private void Init()
        {
            ConfigurationSetting configurationSetting = new ConfigurationSetting();
            string resource = "sqlMapConfig.xml";
            IConfigurationEngine engine = new DefaultConfigurationEngine(configurationSetting);
            engine.RegisterInterpreter(new XmlConfigurationInterpreter(resource));

            IMapperFactory mapperFactory = engine.BuildMapperFactory();
            sessionFactory = engine.ModelStore.SessionFactory;
            dataMapper = ((IDataMapperAccessor)mapperFactory).DataMapper;
            //dataMapper设置分叶
        }

        private  IDataMapper GetDataMapper()
        {
            if (dataMapper != null)
                return dataMapper;
            else
            {
                Init();
                if (dataMapper != null)
                    return dataMapper;
                return null;
            }
        }

        public  IDataMapper DataMapper
        {
            get { return GetDataMapper(); } 
        }

        public ISessionFactory GetSessionFactory()
        {
            if (sessionFactory != null)
            {
                return sessionFactory;
            }
            else
            {
                Init();
                if (sessionFactory != null)
                    return sessionFactory;
                return null;
            }
        }

        public void BeginTransaction()
        {
            session = GetSessionFactory().OpenSession();
            session.BeginTransaction();
        }

        public void EndTransaction()
        {
            session.Transaction.Commit();
            session.Dispose();
        }

        public void Rollback()
        {
            session.Transaction.Rollback();
            session.Dispose();
        }

        public T ExecuteWithTransaction<T>(Func<Object, Object, T> requestRunner)
        {
            T result = default(T);
            BeginTransaction();
            try
            {
                //todo
                result = requestRunner(null, null);
                EndTransaction();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
            return result;
        }

        public object Run(Object param, Object p)
        {
            return null;
        }
    }
}
