#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 470514 $
 * $Date: 2008-10-16 12:14:45 -0600 (Thu, 16 Oct 2008) $
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
using System.Collections.Generic;
using System.Reflection;
using MyBatis.DataMapper.Configuration.Interpreters.Config;
using MyBatis.DataMapper.DataExchange;
using MyBatis.DataMapper.Exceptions;
using MyBatis.DataMapper.Model.ResultMapping;
using System.Data;
using MyBatis.Common.Configuration;
using MyBatis.Common.Exceptions;
using MyBatis.Common.Utilities.Objects;

#endregion 

namespace MyBatis.DataMapper.Configuration.Serializers
{
	/// <summary>
	/// Summary description for ResultMapDeSerializer.
	/// </summary>
	public sealed class ResultMapDeSerializer
	{
        public static BindingFlags ANY_VISIBILITY_INSTANCE = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// Deserializes the specified config.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="dataExchangeFactory">The data exchange factory.</param>
        /// <param name="waitResultPropertyResolution">The wait result property resolution delegate.</param>
        /// <param name="waitDiscriminatorResolution">The wait discriminator resolution.</param>
        /// <returns></returns>
        public static ResultMap Deserialize(
            IConfiguration config,
            DataExchangeFactory dataExchangeFactory,
            WaitResultPropertyResolution waitResultPropertyResolution,
            WaitDiscriminatorResolution waitDiscriminatorResolution
            )
        {
            /*resultMaps子节点信息格式
                 <resultMap id="account-result-constructor"  class="Account" >
                    <constructor>
                        <argument argumentName="identifiant"	column="Account_ID"/>
                        <argument argumentName="firstName"    column="Account_FirstName"/>
                        <argument argumentName="lastName"     column="Account_LastName"/>
                 </constructor>
                 <result property="EmailAddress" column="Account_Email" nullValue="no_email@provided.com"/>
                 <result property="BannerOption" column="Account_Banner_Option" dbType="Varchar" type="bool"/>
                 <result property="CartOption"	  column="Account_Cart_Option" typeHandler="HundredsBool"/>
              </resultMap>
                     */
            //从config中对应的resultMap节点获取其属性
            string id = config.Id;
            string className = ConfigurationUtils.GetMandatoryStringAttribute(config, ConfigConstants.ATTRIBUTE_CLASS);
            string extends = config.GetAttributeValue(ConfigConstants.ATTRIBUTE_EXTENDS);
            string groupBy = config.GetAttributeValue(ConfigConstants.ATTRIBUTE_GROUPBY);
            string keyColumns = config.GetAttributeValue(ConfigConstants.ATTRIBUTE_KEYS_PROPERTIES);
            string suffix = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_SUFFIX, string.Empty);
            string prefix = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_PREFIX, string.Empty);

            //从工厂类的别名字典中获取
            Type type = dataExchangeFactory.TypeHandlerFactory.GetType(className);
            //根据type类型获取IDataExchange类对象
            IDataExchange dataExchange = dataExchangeFactory.GetDataExchangeForClass(type);
            IFactory factory = null;
            //准备存储构造函数参数argument节点信息
            ArgumentPropertyCollection arguments = new ArgumentPropertyCollection();

                     #region Get the constructor & associated parameters 

            //获取config下节点构造函数constructor的集合
            ConfigurationCollection constructors = config.Children.Find(ConfigConstants.ELEMENT_CONSTRUCTOR);

            if (constructors.Count > 0)
            {
                //默认获取第一个构造函数constructor节点  因为是初始化一个类  一个构造函数就足够了
                IConfiguration constructor = constructors[0];

                Type[] argumentsType = new Type[constructor.Children.Count];
                string[] argumentsName = new string[constructor.Children.Count];

                // Builds param name list
                //argument节点的个数
                for (int i = 0; i < constructor.Children.Count; i++)
                {
                    //argumentName属性的取值
                    argumentsName[i] = ConfigurationUtils.GetStringAttribute(constructor.Children[i].Attributes, ConfigConstants.ATTRIBUTE_ARGUMENTNAME);
                }

                // Find the constructor  匹配构造函数
                ConstructorInfo constructorInfo = GetConstructor(id, type, argumentsName);

                // Build ArgumentProperty and parameter type list
                //处理构造函数的参数 每一个参数添加到arguments参数列表中
                for (int i = 0; i < constructor.Children.Count; i++)
                {
                    ArgumentProperty argumentMapping = ArgumentPropertyDeSerializer.Deserialize(
                        constructor.Children[i],//第i个Argument节点配置类
                        type,//当前构造函数的类
                        constructorInfo,//当前构造函数的信息
                        dataExchangeFactory);

                    arguments.Add(argumentMapping);

                    //此处NestedResultMapName字符串应该为空
                    if (argumentMapping.NestedResultMapName.Length > 0)
                    {
                        waitResultPropertyResolution(argumentMapping);
                    }
                    //保存当前参数的类型
                    argumentsType[i] = argumentMapping.MemberType;
                }
                // Init the object factory
                //构造函数参数信息分析完成   动态初始化这个构造函数
                factory = dataExchangeFactory.ObjectFactory.CreateFactory(type, argumentsType);
            }
            else
            {
                if (!dataExchangeFactory.TypeHandlerFactory.IsSimpleType(type) && type!=typeof(DataRow))
                {
                    factory = dataExchangeFactory.ObjectFactory.CreateFactory(type, Type.EmptyTypes);
                }
            }

            #endregion

            //处理result所有节点
            ResultPropertyCollection properties = BuildResultProperties(
                id, 
                config, 
                type, 
                prefix,
                suffix,
                dataExchangeFactory, 
                waitResultPropertyResolution);
            //对discriminator鉴别器的分析
            Discriminator discriminator = BuildDiscriminator(config, type, dataExchangeFactory, waitDiscriminatorResolution);

            //子节点分析完毕 将这些信息加入到resultMap父节点中
            ResultMap resultMap = new ResultMap(
                    id,
                    className,
                    extends,
                    groupBy,
                    keyColumns,
                    type,
                    dataExchange,
                    factory,
                    dataExchangeFactory.TypeHandlerFactory,
                    properties,
                    arguments,
                    discriminator
                    );                

            return resultMap;
        }

        /// <summary>
        /// Finds the constructor that takes the parameters.
        /// </summary>
        /// <param name="resultMapId">The result map id.</param>
        /// <param name="type">The <see cref="System.Type"/> to find the constructor in.</param>
        /// <param name="parametersName">The parameters name to use to find the appropriate constructor.</param>
        /// <returns>
        /// An <see cref="ConstructorInfo"/> that can be used to create the type with
        /// the specified parameters.
        /// </returns>
        /// <exception cref="DataMapperException">
        /// Thrown when no constructor with the correct signature can be found.
        /// </exception>
        private static ConstructorInfo GetConstructor(string resultMapId, Type type, string[] parametersName)
        {
            //获取type类所有的构造函数集合
            ConstructorInfo[] candidates = type.GetConstructors(ANY_VISIBILITY_INSTANCE);
            for (int i = 0; i < candidates.Length; i++)
            {
                //获取当前构造函数的参数集合
                ParameterInfo[] parameters = candidates[i].GetParameters();

                //判断是否参数个数相等
                if (parameters.Length == parametersName.Length)
                {
                    bool found = true;

                    //确保每一个参数名称一样 才能确定是同一个构造函数
                    for (int j = 0; j < parameters.Length; j++)
                    {
                        bool ok = (parameters[j].Name == parametersName[j]);
                        if (!ok)
                        {
                            found = false;
                            break;
                        }
                    }

                    if (found)
                    {
                        return candidates[i];
                    }
                }
            }
            throw new DataMapperException("In ResultMap (" + resultMapId + ") can't find an appropriate constructor which map parameters in class: " + type.Name);
        }

        /// <summary>
        /// Builds the result properties.
        /// </summary>
        /// <param name="resultMapId">The result map id.</param>
        /// <param name="resultMapConfig">The result map config.</param>
        /// <param name="resultClass">The result class.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="suffix">The suffix.</param>
        /// <param name="dataExchangeFactory">The data exchange factory.</param>
        /// <param name="waitResultPropertyResolution">The wait result property resolution.</param>
        /// <returns></returns>
        private static ResultPropertyCollection BuildResultProperties(
            string resultMapId,
            IConfiguration resultMapConfig, 
            Type resultClass,
            string prefix,
            string suffix,
            DataExchangeFactory dataExchangeFactory,
            WaitResultPropertyResolution waitResultPropertyResolution)
        {
            ResultPropertyCollection properties = new ResultPropertyCollection();
            //获取result节点的集合配置信息
            ConfigurationCollection resultsConfig = resultMapConfig.Children.Find(ConfigConstants.ELEMENT_RESULT);
            for (int i = 0; i < resultsConfig.Count; i++)
            {
                ResultProperty mapping = null;
                try
                {
                    mapping = ResultPropertyDeSerializer.Deserialize(resultsConfig[i], resultClass, prefix, suffix, dataExchangeFactory);
                }
                catch(Exception e)
                {
                    throw new ConfigurationException("In ResultMap (" + resultMapId + ") can't build the result property: " + ConfigurationUtils.GetStringAttribute(resultsConfig[i].Attributes, ConfigConstants.ATTRIBUTE_PROPERTY) + ". Cause " + e.Message, e);
                }
                if (mapping.NestedResultMapName.Length > 0)//resultMapping属性如果有值 此处一般会有
                {
                    //添加到DefaultModelBuilder中的ResultPropertyCollection nestedProperties集合中
                    waitResultPropertyResolution(mapping);
                }
                properties.Add(mapping);
            }

            return properties;
        }

        /// <summary>
        /// Builds the discriminator and his subMaps
        /// </summary>
        /// <param name="resultMapConfig">The result map config.</param>
        /// <param name="resultClass">The result class.</param>
        /// <param name="dataExchangeFactory">The data exchange factory.</param>
        /// <param name="waitDiscriminatorResolution">The wait discriminator resolution.</param>
        /// <returns></returns>
        private static Discriminator BuildDiscriminator(
            IConfiguration resultMapConfig,
            Type resultClass,
            DataExchangeFactory dataExchangeFactory,
            WaitDiscriminatorResolution waitDiscriminatorResolution)
        {
            Discriminator discriminator = null;
            // Build the Discriminator/Case Property
            //获取resultMap子节点discriminator的集合信息
            ConfigurationCollection discriminatorsConfig = resultMapConfig.Children.Find(ConfigConstants.ELEMENT_DISCRIMINATOR);
            if (discriminatorsConfig.Count > 0)
            {
                //configScope.ErrorContext.MoreInfo = "initialize discriminator";

                // Find the cases
                IList<Case> cases = new List<Case>();
                //默认获取第一个鉴别器下的子节点case信息集合
                ConfigurationCollection caseConfigs = discriminatorsConfig[0].Children.Find(ConfigConstants.ELEMENT_CASE);
                for (int i = 0; i < caseConfigs.Count; i++)
                {
                    Case caseElement = CaseDeSerializer.Deserialize(caseConfigs[i]);
                    cases.Add(caseElement);
                }

                discriminator = DiscriminatorDeSerializer.Deserialize(
                    discriminatorsConfig[0], 
                    resultClass, 
                    dataExchangeFactory,
                    cases
                    );
                //将discriminator加入到DefaultModelBuilder中的IList<Discriminator> discriminators集合中
                waitDiscriminatorResolution(discriminator);
            }
            return discriminator;
        }
	}
}
