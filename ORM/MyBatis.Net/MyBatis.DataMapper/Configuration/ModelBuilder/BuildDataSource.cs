#region Apache Notice
/*****************************************************************************
 * $Revision: 408099 $
 * $LastChangedDate: 2009-10-10 11:53:18 -0600 (Sat, 10 Oct 2009) $
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

using MyBatis.Common.Configuration;
using MyBatis.Common.Data;

namespace MyBatis.DataMapper.Configuration
{
    public partial class DefaultModelBuilder
    {

        /// <summary>
        /// Builds the data source model.
        /// </summary>
        /// <param name="store">The store.</param>
        private void BuildDataSource(IConfigurationStore store)
        {
            if (dataSource == null)
            {
                /*
                   <database>
                                   <provider name="sqlServer2.0" />
                                   <dataSource name="iBatisNet" connectionString="data source=${datasource};database=${database};Integrated Security=SSPI;" />
                    </database>
                 */
                //取第一个配置文件对应的数据连接节点database
                IConfiguration dataBaseConfig = store.Databases[0];

                IConfiguration providerConfig = dataBaseConfig.Children.Find(DataConstants.ELEMENT_PROVIDER)[0];
                string key = providerConfig.Value ?? providerConfig.GetAttributeValue(DataConstants.ATTRIBUTE_NAME);
                //去工厂类中获得对应的DbProvider
                IDbProvider dbProvider = dbProviderFactory.GetDbProvider(key);

                //初始化DataSource对象
                dataSource = DataSourceDeSerializer.Deserialize(dbProvider, commandTimeOut, dataBaseConfig);
            }


        }
    }
}
