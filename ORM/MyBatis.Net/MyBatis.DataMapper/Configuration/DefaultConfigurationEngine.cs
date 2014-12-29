
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

using System;
using System.Collections.Generic;
using System.Reflection;
using MyBatis.DataMapper.Configuration.Interpreters.Config;
using MyBatis.DataMapper.Model;
using MyBatis.DataMapper.Configuration.Module;
using MyBatis.Common.Configuration;
using MyBatis.Common.Contracts;
using MyBatis.Common.Contracts.Constraints;
using MyBatis.Common.Exceptions;
using MyBatis.Common.Logging;
using MyBatis.Common.Resources;

namespace MyBatis.DataMapper.Configuration
{
    /// <summary>
    /// The default <see cref="IConfigurationEngine"/> implementation.
    /// </summary>
    /// <example>
    /// Use:
    /// 
    /// IConfigurationEngine engine = 
    ///     new DefaultConfigurationEngine( 
    ///             new XmlConfigurationInterpreter(
    ///                 new new FileResource("SqlMap.config") ) );
    /// </example>
    public class DefaultConfigurationEngine : IConfigurationEngine
    {
        private readonly IConfigurationStore configurationStore = null;
        private readonly ConfigurationSetting configurationSetting = null;
        private readonly IList<IModule> modules = null;
        private IModelStore modelStore = null;
        private IConfigurationInterpreter interpreter = null;

        /// <summary>
        /// Event launch on processing file resource
        /// </summary>
        public event EventHandler<FileResourceLoadEventArgs> FileResourceLoad = delegate { };

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigurationEngine"/> class.
        /// </summary>
        public DefaultConfigurationEngine()
        {
            configurationStore = new DefaultConfigurationStore();
            configurationSetting = new ConfigurationSetting();
            modules = new List<IModule>();

            ResourceLoaderRegistry.ResetEventHandler();
            ResourceLoaderRegistry.LoadFileResource += FileResourceEventHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigurationEngine"/> class.
        /// </summary>
        /// <param name="configurationSetting">The configuration setting.</param>
        public DefaultConfigurationEngine(ConfigurationSetting configurationSetting):this()
        {
            Contract.Require.That(configurationSetting, Is.Not.Null).When("retrieving argument ConfigurationSetting in DefaultConfigurationEngine constructor");

            this.configurationSetting = configurationSetting;

            if (configurationSetting.Properties.Count > 0)
            {
                IEnumerator<KeyValuePair<string, string>> properties = configurationSetting.Properties.GetEnumerator();
                while (properties.MoveNext())
                {
                    IConfiguration config = new MutableConfiguration(
                    ConfigConstants.ELEMENT_PROPERTY,
                    properties.Current.Key,
                    properties.Current.Value);
                    configurationStore.AddPropertyConfiguration(config);
                }
            }
        }
        #endregion

        #region IConfigurationEngine Members

        /// <summary>
        /// Gets the model store.
        /// </summary>
        /// <value>The model store.</value>
        public IModelStore ModelStore
        {
            get { return modelStore; }
        }

        /// <summary>
        /// Gets the configuration store.
        /// </summary>
        /// <value>The configuration store.</value>
        public IConfigurationStore ConfigurationStore
        {
            get { return configurationStore; }
        }

        /// <summary>
        /// Registers the <see cref="IConfigurationInterpreter"/>.
        /// </summary>
        /// <param name="interpreter">The interpreter.</param>
        public void RegisterInterpreter(IConfigurationInterpreter interpreter)
        {
            Contract.Require.That(interpreter, Is.Not.Null).When("retrieving argument interpreter in RegisterInterpreter method");

            this.interpreter = interpreter;
        }

        /// <summary>
        /// Add a module to the <see cref="IConfigurationEngine"/>.
        /// </summary>
        /// <param name="module">The module.</param>
        public void RegisterModule(IModule module)
        {
            Contract.Require.That(module, Is.Not.Null).When("retrieving argument module in RegisterModule method");
            modules.Add(module);
        }

        /// <summary>
        /// Builds the mapper factory.
        /// </summary>
        /// <returns>the mapper factory</returns>
        public IMapperFactory BuildMapperFactory()
        {
            // Registers file Xml, ... configuration element
            if (interpreter!=null)
            {
                interpreter.ProcessResource(configurationStore);

                // ensure that the default configuration settings get updated after the interpreter runs
                configurationSetting.PreserveWhitespace = TryGetSettingBoolean(ConfigConstants.ATTRIBUTE_PRESERVEWHITSPACE, configurationSetting.PreserveWhitespace);
                configurationSetting.UseReflectionOptimizer = TryGetSettingBoolean(ConfigConstants.ATTRIBUTE_USE_REFLECTION_OPTIMIZER, configurationSetting.UseReflectionOptimizer);
                configurationSetting.IsCacheModelsEnabled = TryGetSettingBoolean(ConfigConstants.ATTRIBUTE_CACHE_MODELS_ENABLED, configurationSetting.IsCacheModelsEnabled);
                configurationSetting.UseStatementNamespaces = TryGetSettingBoolean(ConfigConstants.ATTRIBUTE_USE_STATEMENT_NAMESPACES, configurationSetting.UseStatementNamespaces);
                configurationSetting.ValidateMapperConfigFile = TryGetSettingBoolean(ConfigConstants.ATTRIBUTE_VALIDATE_SQLMAP, configurationSetting.ValidateMapperConfigFile);
            }

            // Registers code configuration element
            //此处还未有案例分析？？？？
            for(int i=0;i<modules.Count;i++)
            {
                modules[i].Configure(this);
            }

            // Process Extends ResultMap
            //将父节点的子节点信息添加到含有extends节点的下面
            List<IConfiguration> resolved = new List<IConfiguration>();
            for (int i = 0; i < configurationStore.ResultMaps.Length; i++)
            {
                ResolveExtendResultMap(resolved, configurationStore.ResultMaps[i]);
            }

            // Process Extends ParameterMap
            resolved = new List<IConfiguration>();
            for (int i = 0; i < configurationStore.ParameterMaps.Length; i++)
            {
                ResolveExtendParameterMap(resolved, configurationStore.ParameterMaps[i]);
            }

            // Process Include Sql statement
            //处理statements节点下的include属性
            for (int i = 0; i < configurationStore.Statements.Length; i++)
            {
                //获取节点statement update insert delete select节点的子节点include集合
                ConfigurationCollection includes = configurationStore.Statements[i].Children.RecursiveFind(ConfigConstants.ELEMENT_INCLUDE);

                if (includes.Count > 0)
                {
                    ResolveIncludeStatement(includes);
                }
            }

            // Process Extends statement
            resolved = new List<IConfiguration>();
            for (int i = 0; i < configurationStore.Statements.Length; i++)
            {
                ResolveExtendStatement(resolved, configurationStore.Statements[i]);
            }

            modelStore = new DefaultModelStore();

            IModelBuilder builder = new DefaultModelBuilder(modelStore);
            //核心处理结果都存储在了modelStore类中了
            builder.BuildModel(configurationSetting, configurationStore);
            //将核心处理结果对象存到DataMapper类中
            IDataMapper dataMapper = new DataMapper(modelStore);

            return new DefaultMapperFactory(dataMapper);
        }

        /// <summary>
        /// Used by BuildMapperFactory to retrieve configuration values from configurationStore. 
        /// </summary>
        private bool TryGetSettingBoolean(string attributeKey, bool defaultValue)
        {
            var setting = configurationStore.Settings[attributeKey];
            if (setting != null)
            {
                return (bool)setting.GetValue(typeof(bool), defaultValue);
            }

            return defaultValue;
        }

        #endregion

        private IConfiguration ResolveExtendResultMap(IList<IConfiguration> resolvedResultMap, IConfiguration resultMap)
        {
            //检查当前节点是否包含extends属性
            if (resultMap.Attributes.ContainsKey(ConfigConstants.ATTRIBUTE_EXTENDS))
            {
                //判断当前成员变量中是否包含此resultMap节点类
                if (!resolvedResultMap.Contains(resultMap))
                {
                    //不在组织中，加入组织
                    resolvedResultMap.Add(resultMap);

                    // Find the extended resultMap
                    //获取此extends属性的值
                    string extends = resultMap.Attributes[ConfigConstants.ATTRIBUTE_EXTENDS];

                    //获取extends属性对应的resultMap节点类信息 也即是当前节点的继承的父节点配置
                    IConfiguration father = configurationStore.GetResultMapConfiguration(extends);
                    if (father == null)
                    {
                        throw new ConfigurationException("There's no extended resultMap named '" + extends + "' for the resultMap '" + resultMap.Id + "'");
                    }
                    //递归处理 看是否还有更上层的父节点
                    father = ResolveExtendResultMap(resolvedResultMap, father);
                    //父节点father移除子节点中的discriminator节点配置 
                    //当前节点resultMap添加更新后的配置包括 构造函数参数 返回值的类型等信息 因为父节点信息属于子节点信息的一部分
                    resultMap.Children.AddRange(father.Children.Remove(ConfigConstants.ELEMENT_DISCRIMINATOR));   
 
                    // Copy the groupBy attribute
                    //获取父节点的分组属性信息
                    if (father.Attributes.ContainsKey(ConfigConstants.ATTRIBUTE_GROUPBY))
                    {
                        resultMap.Attributes[ConfigConstants.ATTRIBUTE_GROUPBY] = father.Attributes[ConfigConstants.ATTRIBUTE_GROUPBY];
                    }
                }
            }
            return resultMap;
        }

        private IConfiguration ResolveExtendParameterMap(IList<IConfiguration> resolvedParameterMap, IConfiguration parameterMap)
        {
            if (parameterMap.Attributes.ContainsKey(ConfigConstants.ATTRIBUTE_EXTENDS))
            {
                if (!resolvedParameterMap.Contains(parameterMap))
                {
                    resolvedParameterMap.Add(parameterMap);

                    // Find the extended resultMap
                    string extends = parameterMap.Attributes[ConfigConstants.ATTRIBUTE_EXTENDS];

                    IConfiguration father = configurationStore.GetParameterMapConfiguration(extends);
                    if (father == null)
                    {
                        throw new ConfigurationException("There's no extended parameterMap named '" + extends + "' for the parameterMap '" + parameterMap.Id + "'");
                    }
                    father = ResolveExtendParameterMap(resolvedParameterMap, father);
                    //参数信息按先后顺序排列 父节点参数信息在子节点信息之前
                    parameterMap.Children.InsertRange(0,father.Children);
                }
            }
            return parameterMap;
        }
        /// <summary>
        /// 处理includes节点信息
        /// </summary>
        /// <param name="includes"></param>
        private void ResolveIncludeStatement(ConfigurationCollection includes)
        {
            for (int i = 0; i < includes.Count;i++ )
            {
                IConfiguration include = includes[i];
                IConfiguration toInclude =
                          configurationStore.GetStatementConfiguration(include.Id);//获得include节点属性值所对应的目标节点
                if (toInclude == null)//如果是不存在的include目标 出错
                {
                    throw new ConfigurationException("There's no include statement named '" + include.Id + "'");
                }
                //包含include节点的父节点信息 也即update insert delete statement select节点配置
                IConfiguration parent = include.Parent;
                //include在父节点children中的下标位置
                int childIndex = include.Parent.Children.IndexOf(include);
                //移除include节点
                parent.Children.RemoveAt(childIndex);
                //用include节点引用的目标节点代替原来的内容
                parent.Children.InsertRange(childIndex, toInclude.Children);
            }
        }

        private IConfiguration ResolveExtendStatement(IList<IConfiguration> resolvedStatement, IConfiguration statement)
        {
            if (statement.Attributes.ContainsKey(ConfigConstants.ATTRIBUTE_EXTENDS))
            {
                if (!resolvedStatement.Contains(statement))
                {
                    resolvedStatement.Add(statement);

                    // Find the extended resultMap
                    string extends = statement.Attributes[ConfigConstants.ATTRIBUTE_EXTENDS];
                    IConfiguration father = configurationStore.GetStatementConfiguration(extends);
                    if (father==null)
                    {
                        throw new ConfigurationException("There's no extended statement named '" + extends + "' for the statement '" + statement.Id+"'");
                    }
                    father = ResolveExtendStatement(resolvedStatement, father);
                    //sql语句按先后顺序排列 父节点的查询相关语句在子节点语句之前
                    statement.Children.InsertRange(0, father.Children);
                }
            }
            return statement;
        }

        private void FileResourceEventHandler(object sender, FileResourceLoadEventArgs evnt)
        {
            FileResourceLoad(sender, evnt);
        }
    }
}
