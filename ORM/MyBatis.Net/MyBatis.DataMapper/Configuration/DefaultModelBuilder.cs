
#region Apache Notice
/*****************************************************************************
 * $Revision: 408099 $
 * $LastChangedDate: 2009-06-28 10:11:37 -0600 (Sun, 28 Jun 2009) $
 * $LastChangedBy: rgrabowski $
 * 
 * iBATIS.NET Data Mapper
 * Copyright (C) 2008/2005 - The Apache Software Foundation
 *  
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 ********************************************************************************/
#endregion

using System.Reflection;
using System.Collections.Generic;
using MyBatis.DataMapper.Model.Alias;
using MyBatis.DataMapper.DataExchange;
using MyBatis.DataMapper.Model;
using MyBatis.DataMapper.Model.Cache.Implementation;
using MyBatis.DataMapper.Session.Transaction;
using MyBatis.DataMapper.TypeHandlers;
using MyBatis.DataMapper.Configuration.Interpreters.Config;
using MyBatis.DataMapper.Model.ParameterMapping;
using MyBatis.DataMapper.Configuration.Serializers;
using MyBatis.DataMapper.Session;
using MyBatis.DataMapper.Model.ResultMapping;
using MyBatis.DataMapper.Model.Cache;
using MyBatis.DataMapper.MappedStatements;
using MyBatis.DataMapper.Session.Stores;
using MyBatis.DataMapper.Session.Transaction.Ado;
using MyBatis.Common.Contracts;
using MyBatis.Common.Contracts.Constraints;
using MyBatis.Common.Data;
using MyBatis.Common.Logging;
using MyBatis.Common.Utilities.Objects;
using MyBatis.Common.Utilities.Objects.Members;

namespace MyBatis.DataMapper.Configuration
{
    public delegate void WaitResultPropertyResolution(ResultProperty property);
    public delegate void WaitDiscriminatorResolution(Discriminator discriminator);
  
    /// <summary>
    /// Default implementation of the <see cref="IModelBuilder"/> contract. 
    /// </summary>
    public partial class DefaultModelBuilder : IModelBuilder
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ResultPropertyCollection nestedProperties = new ResultPropertyCollection();
        private readonly IList<Discriminator> discriminators = new List<Discriminator>();

        private readonly IModelStore modelStore = null;
        private DbProviderFactory dbProviderFactory = null;
        private IDataSource dataSource = null;
        private DeSerializerFactory deSerializerFactory = null;
        private readonly InlineParemeterMapBuilder inlineParemeterMapBuilder = null;

        private bool useStatementNamespaces = false;
        private bool isCacheModelsEnabled = false;
        private bool useReflectionOptimizer = true;
        private bool preserveWhitespace;
        private int commandTimeOut = -1;

        private readonly WaitResultPropertyResolution waitResultPropertyResolution = null;
        private readonly WaitDiscriminatorResolution waitDiscriminatorResolution = null;
        private ISqlSource dynamicSqlEngine = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultModelBuilder"/> class.
        /// </summary>
        /// <param name="modelStore">The model store.</param>
        public DefaultModelBuilder(IModelStore modelStore)
        {
            Contract.Require.That(modelStore, Is.Not.Null).When("retrieving argument modelStore in DefaultModelBuilder constructor");

            this.modelStore = modelStore;
            inlineParemeterMapBuilder = new InlineParemeterMapBuilder(modelStore);

            //result节点的resultMapping属性存在 就会调用此委托
            waitResultPropertyResolution = WaitResultPropertyResolution;
            waitDiscriminatorResolution = WaitDiscriminatorResolution;
        }

        #region IModelBuilder Members

        /// <summary>
        /// Builds the the iBATIS core model (statement, alias, resultMap, parameterMap, dataSource)
        /// from an <see cref="IConfigurationStore"/> and store all the refrences in an <see cref="IModelStore"/> .
        /// </summary>
        /// <param name="configurationSetting">The configuration setting.</param>
        /// <param name="store">The configuration store.</param>
        /// <returns>The model store</returns>
        public virtual void BuildModel(ConfigurationSetting configurationSetting, IConfigurationStore store)
        {
            IObjectFactory objectFactory = null;
            IGetAccessorFactory getAccessorFactory = null;
            ISetAccessorFactory setAccessorFactory = null;
            ISessionFactory sessionFactory = null;
            ISessionStore sessionStore = null;
            
            if (configurationSetting != null)
            {
                objectFactory = configurationSetting.ObjectFactory;
                setAccessorFactory = configurationSetting.SetAccessorFactory;
                getAccessorFactory = configurationSetting.GetAccessorFactory;
                dataSource = configurationSetting.DataSource;
                sessionFactory = configurationSetting.SessionFactory;
                sessionStore = configurationSetting.SessionStore;
                dynamicSqlEngine = configurationSetting.DynamicSqlEngine;
                isCacheModelsEnabled = configurationSetting.IsCacheModelsEnabled;
                useStatementNamespaces = configurationSetting.UseStatementNamespaces;
                useReflectionOptimizer = configurationSetting.UseReflectionOptimizer;
                preserveWhitespace = configurationSetting.PreserveWhitespace;
            }
            
            // Xml setting override code setting 
            //取store中的数据初始化类成员 为下面做准备
            LoadSetting(store);

            if (objectFactory == null)
            {
                objectFactory = new ObjectFactory(useReflectionOptimizer);
            }
            if (setAccessorFactory == null)
            {
                setAccessorFactory = new SetAccessorFactory(useReflectionOptimizer);
            }
            if (getAccessorFactory == null)
            {
                getAccessorFactory = new GetAccessorFactory(useReflectionOptimizer);
            }
            AccessorFactory accessorFactory = new AccessorFactory(setAccessorFactory, getAccessorFactory);

            TypeHandlerFactory typeHandlerFactory = new TypeHandlerFactory();
            TypeAlias alias = new TypeAlias("MEMORY", typeof(PerpetualCache));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);
            alias = new TypeAlias("Perpetual", typeof(PerpetualCache));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);

            alias = new TypeAlias("LRU", typeof(LruCache));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);
            alias = new TypeAlias("Lru", typeof(LruCache));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);

            alias = new TypeAlias("FIFO", typeof(FifoCache));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);
            alias = new TypeAlias("Fifo", typeof(FifoCache));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);

            alias = new TypeAlias("Weak", typeof(WeakCache));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);
            alias = new TypeAlias("WEAK", typeof(WeakCache));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);

            alias = new TypeAlias("MemCached", typeof(MemCached));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);
            alias = new TypeAlias("MEMCACHED", typeof(MemCached));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);

            alias = new TypeAlias("AnsiStringTypeHandler", typeof(AnsiStringTypeHandler));
            typeHandlerFactory.AddTypeAlias(alias.Id, alias);

            //将以上类信息存入到modelStore中 实质性工作
            modelStore.DataExchangeFactory = new DataExchangeFactory(typeHandlerFactory, objectFactory, accessorFactory);

            if (sessionStore == null)
            {
                sessionStore = SessionStoreFactory.GetSessionStore(modelStore.Id);
            }
            //将ISessionStore类对象存入modelStore中 准备数据库的连接 与 事物操作功能  实质性工作
            modelStore.SessionStore = sessionStore;

            //初始化DefaultModelBuilder的成员变量 
            deSerializerFactory = new DeSerializerFactory(modelStore); 

            //设置一个空类型的参数映射类
            ParameterMap emptyParameterMap = new ParameterMap(
                ConfigConstants.EMPTY_PARAMETER_MAP,
                string.Empty,
                string.Empty,
                typeof(string),
                modelStore.DataExchangeFactory.GetDataExchangeForClass(null), //获得ComplexDataExchange对象
                false);

            //向参数字典中添加一个空参数类
            modelStore.AddParameterMap(emptyParameterMap);

            //完成了对DefaultModelBuilder成员变量dbProviderFactory的初始化，其中完成了对provider使用中节点的类初始化
            BuildProviders(store);

            //完成了对DefaultModelBuilder成员变量IDataSource的初始化,包括数据库的名字 连接字符串 以及DbPrivder类
            BuildDataSource(store);

            if (sessionFactory == null)
            {
                sessionFactory = new DefaultSessionFactory(
                    dataSource, 
                    modelStore.SessionStore,
                    new DefaultTransactionManager(new AdoTransactionFactory()));
            }
            //初始化modelStore中的ISessionFactory变量
            modelStore.SessionFactory = sessionFactory;

            //将具体SQLXML配置文件中的Alias节点对应的别名信息写入到modelStore中的TypeHandlerFactory的字典中
            BuildTypeAlias(store);

            //将TypeHanders下的子节点信息放入 modelStore.DataExchangeFactory.TypeHandlerFactory中的字典当中
            BuildTypeHandlers(store);

            //将cacheModels下子节点cacheModel加入到modelStore的CacheModel字典中
            BuildCacheModels(store);
            //将resultMap节点信息添加到modelStore的resultMaps中
            BuildResultMaps(store);

            //为resultMapping属性所在的节点信息类设置对应的ResultProperty
            for (int i = 0; i < nestedProperties.Count; i++)
            {
                ResultProperty property = nestedProperties[i];
                property.NestedResultMap = modelStore.GetResultMap(property.NestedResultMapName);
            }

            for (int i = 0; i < discriminators.Count; i++)
            {
                //完成对discriminator类中对字典的初始化
                discriminators[i].Initialize(modelStore);
            }
            //将parameter节点信息添加到modelStore类中的parameterMaps字典中
            BuildParameterMaps(store);

            //将statements下的节点添加到modelStore的statements的字典中
            BuildMappedStatements(store, configurationSetting);

            for (int i = 0; i < store.CacheModels.Length; i++)
            {
                CacheModel cacheModel = modelStore.GetCacheModel(store.CacheModels[i].Id);

                for (int j = 0; j < cacheModel.StatementFlushNames.Count; j++)
                {
                    string statement = cacheModel.StatementFlushNames[j];
                    IMappedStatement mappedStatement = modelStore.GetMappedStatement(statement);
                    if (mappedStatement != null)
                    {
                        //为IMappedStatement类的Executed制定委托事件  目的是清空缓存
                        cacheModel.RegisterTriggerStatement(mappedStatement);
                        if (logger.IsDebugEnabled)
                        {
                            logger.Debug("Registering trigger statement [" + statement + "] to cache model [" + cacheModel.Id + "]");
                        }
                    }
                    else
                    {
                        if (logger.IsWarnEnabled)
                        {
                            logger.Warn("Unable to register trigger statement [" + statement + "] to cache model [" + cacheModel.Id + "]. Statement does not exist.");
                        }
                    }

                }
            }

            if (logger.IsInfoEnabled)
            {
                logger.Info("Model Store");
                logger.Info(modelStore.ToString());
            }
        }

        /// <summary>
        /// Stores ResultProperty from which the NestedResultMap property must be resolved
        /// Delay resolution until all the ResultMap are processed.
        /// </summary>
        /// <param name="property">The property.</param>
        private void WaitResultPropertyResolution(ResultProperty property)
        {
            nestedProperties.Add(property);
        }

        /// <summary>
        /// Stores Discriminator from which the subMaps property must be resolved
        /// Delay resolution until all the ResultMap are processed.
        /// ///
        /// </summary>
        /// <param name="discriminator">The discriminator.</param>
        private void WaitDiscriminatorResolution(Discriminator discriminator)
        {
            discriminators.Add(discriminator);
        }

        /// <summary>
        /// Register under Statement Name or Fully Qualified Statement Name
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        /// <param name="id">An Identity</param>
        /// <returns>The new Identity</returns>
        public string ApplyNamespace(string nameSpace, string id)
        {
            string newId = id;

            if (nameSpace != null && nameSpace.Length > 0
                && id != null && id.Length > 0 && id.IndexOf(".") < 0)
            {
                newId = nameSpace + ConfigConstants.DOT + id;
            }
            return newId;
        }

        #endregion

    }
}
