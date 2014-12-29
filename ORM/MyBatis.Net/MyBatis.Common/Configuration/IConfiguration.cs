
#region Apache Notice
/*****************************************************************************
 * $Revision: 408099 $
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

using System;
using System.Collections.Generic;

namespace MyBatis.Common.Configuration
{
    /// <summary>
    /// <see cref="IConfiguration"/>  is a interface encapsulating a configuration node  处理配置文件节点的接口
    ///	used to retrieve configuration values.
    /// </summary>
    public interface IConfiguration
    {

        /// <summary>
        /// Gets the parent. 获取当前节点的父节点
        /// </summary>
        /// <value>The parent.</value>
        IConfiguration Parent { get; set; } 

        /// <summary>
        /// Gets the type of the node.
        /// </summary>
        /// <value>
        /// The type of the node. 当前节点类型
        /// </value> 
        string Type { get; }

        /// <summary>
        /// Gets the id of the node. 节点ID 唯一属性
        /// </summary>
        /// <value>
        /// The id of the node.
        /// </value> 
        string Id { get; }

        /// <summary>
        /// Gets the value of the node. 节点的值
        /// </summary>
        /// <value>
        /// The Value of the node.
        /// </value> 
        string Value { get; }

        /// <summary>
        /// List of <see cref="IConfiguration"/> 节点的字节点信息
        /// elements for children node .
        /// </summary>
        /// <value>The Collection of child nodes.</value>
        ConfigurationCollection Children { get; }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>If key exists, the attribute value else null</returns>
        string GetAttributeValue(string key);

        /// <summary>
        /// Gets the attributes. 节点信息列表
        /// </summary>
        /// <value>The attributes.</value>
        IDictionary<string, string> Attributes { get; }

        /// <summary>
        /// Gets the value of the node and converts it 
        /// into specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/></param>
        /// <param name="defaultValue">
        /// The Default value returned if the convertion fails.
        /// </param>
        /// <returns>The Value converted into the specified type.</returns>
        object GetValue(Type type, object defaultValue);
    }
}
