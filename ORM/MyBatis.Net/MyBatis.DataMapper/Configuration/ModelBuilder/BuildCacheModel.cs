#region Apache Notice
/*****************************************************************************
 * $Revision: 408099 $
 * $LastChangedDate: 2008-10-16 12:14:45 -0600 (Thu, 16 Oct 2008) $
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

using System.Collections.Generic;
using MyBatis.DataMapper.Configuration.Interpreters.Config;
using MyBatis.DataMapper.Configuration.Serializers;
using MyBatis.DataMapper.Model.Cache;
using MyBatis.Common.Configuration;

namespace MyBatis.DataMapper.Configuration
{
    public partial class DefaultModelBuilder
    {

        /// <summary>
        /// Builds the cache model.
        /// </summary>
        /// <param name="store">The store.</param>
        private void BuildCacheModels(IConfigurationStore store)
        {
            /*<cacheModels>节点信息格式
               <cacheModels>
                           <cacheModel id="account-cache" type="Perpetual" flushInterval="30" >
                                  <flushOnExecute  statement="UpdateAccountViaInlineParameters"/>
                                 <flushOnExecute  statement="UpdateAccountViaParameterMap"/>
                         </cacheModel>
             <cacheModels>
                     */

            //store.CacheModels中存储的是cacheModel节点的类对象
            for (int i=0;i<store.CacheModels.Length;i++)
            {
                IConfiguration cacheModelConfig = store.CacheModels[i];
                //cacheModel节点属性保存到类对象中
                CacheModel cacheModel = CacheModelDeSerializer.Deserialize(cacheModelConfig, modelStore.DataExchangeFactory);

                string nameSpace = ConfigurationUtils.GetMandatoryStringAttribute(cacheModelConfig, ConfigConstants.ATTRIBUTE_NAMESPACE);

                // Gets all the flush on excecute statement id
                //获取当前CacheModel节点flushOnExecute子节点
                ConfigurationCollection flushConfigs = cacheModelConfig.Children.Find(ConfigConstants.ELEMENT_FLUSHONEXECUTE);
                for (int j = 0; j < flushConfigs.Count; j++)
                {
                    //获取flushOnExecute子节点的statement对应的值
                    string statementId = flushConfigs[j].Attributes[ConfigConstants.ATTRIBUTE_STATEMENT];
                    if (useStatementNamespaces)//使用命名空间
                    {
                        statementId = ApplyNamespace(nameSpace, statementId);
                    }

                    //将处理后的statementId名称加入cacheModel的列表中  即cacheModel中包含了其子statement集合名称
                    cacheModel.StatementFlushNames.Add(statementId);
                }
                //将当前cacheModel加入到modelStore中的CacheModel字典中
                modelStore.AddCacheModel(cacheModel);
            }
        }

        /// <summary>
        /// Gets the cacheModel properties.
        /// </summary>
        /// <param name="cacheModelConfiguration">The cache model configuration.</param>
        /// <returns></returns>
        private IDictionary<string, string> GetProperties(IConfiguration cacheModelConfiguration)
        {
            IDictionary<string, string> properties = new Dictionary<string, string>();

            // Get Properties 
            ConfigurationCollection propertiesConfigs = cacheModelConfiguration.Children.Find(ConfigConstants.ELEMENT_PROPERTY);

            for (int i = 0; i < propertiesConfigs.Count; i++)
            {
                IConfiguration propertie = propertiesConfigs[i];
                string name = propertie.Attributes[ConfigConstants.ATTRIBUTE_NAME];
                string value = propertie.Attributes[ConfigConstants.ATTRIBUTE_VALUE];

                properties.Add(name, value);
            }

            return properties;
        }

    }
}
