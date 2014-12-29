#region Apache Notice
/*****************************************************************************
 * $Revision: 374175 $
 * $LastChangedDate: 2008-06-28 09:26:16 -0600 (Sat, 28 Jun 2008) $
 * $LastChangedBy: gbayon $
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
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Xml;
using MyBatis.DataMapper.Model.ResultMapping;
using MyBatis.DataMapper.Scope;
using MyBatis.DataMapper.TypeHandlers;
using MyBatis.DataMapper.DataExchange;
using MyBatis.DataMapper.Configuration.Interpreters.Config;
using MyBatis.Common.Configuration;

#endregion 

namespace MyBatis.DataMapper.Configuration.Serializers
{
	/// <summary>
	/// Summary description for ArgumentPropertyDeSerializer.
	/// </summary>
	public sealed class ArgumentPropertyDeSerializer
	{
        /// <summary>
        /// Deserializes the specified config in an ArgumentProperty object
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="resultClass">The result class.</param>
        /// <param name="constructorInfo">The constructor info.</param>
        /// <param name="dataExchangeFactory">The data exchange factory.</param>
        /// <returns></returns>
        public static ArgumentProperty Deserialize(
            IConfiguration config,
            Type resultClass,
            ConstructorInfo constructorInfo,
            DataExchangeFactory dataExchangeFactory)
        {
            //获取一个argument节点的信息
            string argumentName = ConfigurationUtils.GetMandatoryStringAttribute(config, ConfigConstants.ATTRIBUTE_ARGUMENTNAME);
            string clrType = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_TYPE);
            string callBackName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_TYPEHANDLER);
            int columnIndex = ConfigurationUtils.GetIntAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_COLUMNINDEX, ResultProperty.UNKNOWN_COLUMN_INDEX);
            string columnName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_COLUMN);
            string dbType = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_DBTYPE);
            //resultMapping属性值  此处应该是为空的
            string nestedResultMapName = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_RESULTMAPPING);
            string nullValue = config.GetAttributeValue(ConfigConstants.ATTRIBUTE_NULLVALUE);
            string select = ConfigurationUtils.GetStringAttribute(config.Attributes, ConfigConstants.ATTRIBUTE_SELECT);

            //获取当前参数的类型
            Type type = GetParameterType(constructorInfo, argumentName);

            //将参数信息存入类中 转入到内存中来
            return new ArgumentProperty(
                argumentName,
                columnName,                
                columnIndex,
                clrType,
                callBackName,
                dbType,
                nestedResultMapName,
                nullValue,
                select,
                type,
                resultClass,
                dataExchangeFactory,
                dataExchangeFactory.TypeHandlerFactory.ResolveTypeHandler(type, clrType, dbType)
                );
        }

        private static Type GetParameterType(ConstructorInfo constructorInfo, string name)
        {
            Type type = null;
            // Search argument by his name to set his type
            //获得当前构造函数的参数列表
            ParameterInfo[] parameters = constructorInfo.GetParameters();

            bool found = false;
            for (int i = 0; i < parameters.Length; i++)
            {
                found = (parameters[i].Name == name);//参数名一样
                if (found)
                {
                    type = parameters[i].ParameterType;//返回参数的类型
                    break;
                }
            }
            return type;
        }
	}
}
