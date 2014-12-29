#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 591621 $
 * $Date: 2008-06-28 09:26:16 -0600 (Sat, 28 Jun 2008) $
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

using MyBatis.Common.Data;

namespace MyBatis.DataMapper.Configuration.Interpreters.Config.Xml.Processor
{
    /// <summary>
    /// Analyse the iBATIS XML configuration and import their configurations in the <see cref="IConfigurationStore"/>
    /// </summary>
    public partial class XmlConfigProcessor : BaseXmlProcessor
    {
        /// <summary>
        /// Registers the element handlers.
        /// 注册不同类型节点对应的处理函数
        /// </summary>
        protected override void RegisterElementHandlers()
		{
            RegisterElementHandler(ConfigConstants.ELEMENT_PROPERTIES, new XmlTagHandler(ProcessPropertiesElement));//ok
            RegisterElementHandler(ConfigConstants.ELEMENT_PROPERTY, new XmlTagHandler(ProcessPropertyElement));//ok
            RegisterElementHandler(ConfigConstants.ELEMENT_SETTING, new XmlTagHandler(ProcessSettingElement));//ok
            RegisterElementHandler(ConfigConstants.ELEMENT_ADD, new XmlTagHandler(ProcessAddElement));//ok
            RegisterElementHandler(DataConstants.ELEMENT_PROVIDERS, new XmlTagHandler(ProcessProvidersElement));//ok
            RegisterElementHandler(DataConstants.ELEMENT_PROVIDER, new XmlTagHandler(ProcessProviderElement));//ok
            RegisterElementHandler(ConfigConstants.ELEMENT_DATABASE, new XmlTagHandler(ProcessDatabaseElement));//ok
            RegisterElementHandler(DataConstants.ELEMENT_DATASOURCE, new XmlTagHandler(ProcessDataSourceElement));//ok
            RegisterElementHandler(ConfigConstants.ELEMENT_TYPEALIAS, new XmlTagHandler(ProcessTypeAliasElement));//ok
            RegisterElementHandler(ConfigConstants.ELEMENT_TYPEHANDLER, new XmlTagHandler(ProcessTypeHandlerElement));//ok
            RegisterElementHandler(ConfigConstants.ELEMENT_SQLMAP, new XmlTagHandler(ProcessSqlMapElement));//ok
		}

    }
}
