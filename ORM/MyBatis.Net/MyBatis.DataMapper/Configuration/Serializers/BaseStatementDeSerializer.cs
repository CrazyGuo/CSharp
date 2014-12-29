#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 469233 $
 * $Date: 2009-06-28 10:11:37 -0600 (Sun, 28 Jun 2009) $
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

#region Using

using System;
using System.Data;
using MyBatis.DataMapper.Configuration.Interpreters.Config;
using MyBatis.DataMapper.DataExchange;
using MyBatis.DataMapper.Model;
using MyBatis.DataMapper.Model.Cache;
using MyBatis.DataMapper.Model.ParameterMapping;
using MyBatis.DataMapper.Model.ResultMapping;
using MyBatis.DataMapper.Model.Sql.External;
using MyBatis.DataMapper.Model.Statements;
using MyBatis.Common.Configuration;
using MyBatis.Common.Utilities.Objects;

#endregion 

namespace MyBatis.DataMapper.Configuration.Serializers
{
    /// <summary>
    /// Base class for StatementDeserializer
    /// </summary>
    public abstract class BaseStatementDeSerializer
    {
        protected string id = string.Empty;
        protected string cacheModelName = string.Empty;
        protected string extendsName = string.Empty;
        protected string listClassName = string.Empty;
        protected string parameterClassName = string.Empty;
        protected string parameterMapName = string.Empty;
        protected string resultClassName = string.Empty;
        protected string resultMapName = string.Empty;
        protected bool remapResults = false;
        protected string nameSpace = string.Empty;
        protected string sqlSourceClassName = string.Empty;
        protected bool preserveWhitespace;

        protected ResultMapCollection resultsMap = new ResultMapCollection();
        protected Type resultClass = null;
        protected ParameterMap parameterMap = null;
        protected Type parameterClass = null;
        protected Type listClass = null;
        protected IFactory listClassFactory = null;
        protected CacheModel cacheModel = null;
        protected ISqlSource sqlSource = null;

        /// <summary>
        /// Deserializes the specified configuration in a Statement object.
        /// </summary>
        /// <param name="modelStore"></param>
        /// <param name="config">The config.</param>
        /// <param name="configurationSetting">Default settings.</param>
        /// <returns></returns>
        protected void BaseDeserialize(IModelStore modelStore, IConfiguration config, ConfigurationSetting configurationSetting)
        {
            nameSpace = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_NAMESPACE);
            id = configurationSetting.UseStatementNamespaces ? ApplyNamespace(nameSpace, config.Id) : config.Id;
            cacheModelName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_CACHEMODEL);
            extendsName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_EXTENDS);
            listClassName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_LISTCLASS);
            parameterClassName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_PARAMETERCLASS);
            parameterMapName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_PARAMETERMAP);
            resultClassName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_RESULTCLASS);
            resultMapName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_RESULTMAP);
            remapResults = ConfigurationUtils.GetBooleanAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_REMAPRESULTS, false);
            sqlSourceClassName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_SQLSOURCE);
            preserveWhitespace = ConfigurationUtils.GetBooleanAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_PRESERVEWHITSPACE, configurationSetting.PreserveWhitespace);

            // Gets the results Map
            //resultMaps的属性值会有多个返回值的情况 并且以","分割
            if (resultMapName.Length > 0)
            {
                string[] ids = resultMapName.Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    string name = ApplyNamespace(nameSpace, ids[i].Trim());
                    resultsMap.Add(modelStore.GetResultMap(name));
                }
            }
            // Gets the results class
            //resultClass的属性值也会有多个返回值的情况 并且以","分割
            if (resultClassName.Length > 0)
            {
                string[] classNames = resultClassName.Split(',');
                for (int i = 0; i < classNames.Length; i++)
                {
                    resultClass = modelStore.DataExchangeFactory.TypeHandlerFactory.GetType(classNames[i].Trim());
                    IFactory resultClassFactory = null;
                    if (Type.GetTypeCode(resultClass) == TypeCode.Object &&
                        (resultClass.IsValueType == false) && resultClass != typeof(DataRow))
                    {
                        //用于创建类对象的函数
                        resultClassFactory = modelStore.DataExchangeFactory.ObjectFactory.CreateFactory(resultClass, Type.EmptyTypes);
                    }
                    IDataExchange dataExchange = modelStore.DataExchangeFactory.GetDataExchangeForClass(resultClass);
                    bool isSimpleType = modelStore.DataExchangeFactory.TypeHandlerFactory.IsSimpleType(resultClass);
                    IResultMap autoMap = new AutoResultMap(resultClass, resultClassFactory, dataExchange, isSimpleType);
                    resultsMap.Add(autoMap);
                }
            }

            // Gets the ParameterMap
            //去DefaultModelBuilder中的parameterMaps字典中获取对应的参数类
            if (parameterMapName.Length > 0)
            {
                parameterMap = modelStore.GetParameterMap(parameterMapName);
            }
            // Gets the ParameterClass
            //去TypeHandlerFactory中的typeAlias字典中获取其对应的类别名类
            if (parameterClassName.Length > 0)
            {
                parameterClass = modelStore.DataExchangeFactory.TypeHandlerFactory.GetType(parameterClassName);
            }

            // Gets the listClass
            if (listClassName.Length > 0)
            {
                //去TypeHandlerFactory中的typeAlias字典中获取其对应的类别名类
                listClass = modelStore.DataExchangeFactory.TypeHandlerFactory.GetType(listClassName);
                //创建对应的对象创建工厂类
                listClassFactory = modelStore.DataExchangeFactory.ObjectFactory.CreateFactory(listClass, Type.EmptyTypes);
            }

            // Gets the CacheModel
            if (cacheModelName.Length > 0)
            {
                //DefaultModelBuilder中的cacheModels字典中获取对应的缓存类
                cacheModel = modelStore.GetCacheModel(cacheModelName);
            }
            // Gets the SqlSource
            if (sqlSourceClassName.Length > 0)
            {
                //获取SqlSource属性值对应的类
                Type sqlSourceType = modelStore.DataExchangeFactory.TypeHandlerFactory.GetType(sqlSourceClassName);
                //获取创建得到类的工厂
                IFactory factory = modelStore.DataExchangeFactory.ObjectFactory.CreateFactory(sqlSourceType, Type.EmptyTypes);
                //调用构造函数 初始化该类
                sqlSource = (ISqlSource)factory.CreateInstance(null);
            }
        }

        /// <summary>
        /// Deserializes the specified configuration in a Statement object.
        /// </summary>
        /// <param name="modelStore">The model store.</param>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        public abstract IStatement Deserialize(IModelStore modelStore, IConfiguration config, ConfigurationSetting configurationSetting);

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
    }
}
