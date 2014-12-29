#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 512878 $
 * $Date: 2008-09-21 10:29:40 -0600 (Sun, 21 Sep 2008) $
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

using MyBatis.Common.Configuration;

#endregion 

namespace MyBatis.Common.Data
{
	/// <summary>
	/// Summary description for DataSourceDeSerializer. 数据库的反序列化到DataSource类
	/// </summary>
	public sealed class DataSourceDeSerializer
	{
        /// <summary>
        /// Deserialize a DataSource object
        /// </summary>
        /// <param name="dbProvider">The db provider.</param>
        /// <param name="commandTimeOut">The command time out.</param>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        public static DataSource Deserialize(IDbProvider dbProvider, int commandTimeOut, IConfiguration config)
		{
            /*
           <database>
                           <provider name="sqlServer2.0" />
                           <dataSource name="iBatisNet" connectionString="data source=${datasource};database=${database};Integrated Security=SSPI;" />
            </database>
         */
            //获取dataSource节点对应的配置信息类
            IConfiguration dataSourceConfig = config.Children.Find(DataConstants.ELEMENT_DATASOURCE)[0];

            //获取连接字符串
            string connectionString = dataSourceConfig.Attributes[DataConstants.ATTRIBUTE_CONNECTIONSTRING];
            //获取数据库的名字
            string name = dataSourceConfig.Attributes[DataConstants.ATTRIBUTE_NAME];

            return new DataSource(name, connectionString, commandTimeOut, dbProvider);
		}

	}
}
