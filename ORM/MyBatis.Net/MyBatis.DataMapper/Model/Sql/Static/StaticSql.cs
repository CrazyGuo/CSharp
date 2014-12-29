
#region Apache Notice
/*****************************************************************************
 * $Revision: 476843 $
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

#region Imports

using MyBatis.DataMapper.Model.Statements;
using MyBatis.DataMapper.DataExchange;
using MyBatis.DataMapper.MappedStatements;
using MyBatis.DataMapper.Scope;
using MyBatis.DataMapper.Session;
using MyBatis.DataMapper.Data;
using MyBatis.Common.Contracts;
using MyBatis.Common.Contracts.Constraints;

#endregion

namespace MyBatis.DataMapper.Model.Sql.Static
{
	/// <summary>
	/// Represents a simple (not a procedure) mapped statement without any dynamic element.
    /// 只有update delete insert select这样的节点才会使用这个类
	/// </summary>
	public sealed class StaticSql : ISql
	{
        private readonly IStatement statement = null;
        private PreparedStatement preparedStatement = null;
        private readonly DataExchangeFactory dataExchangeFactory = null;
        private readonly DBHelperParameterCache dbHelperParameterCache = null;

		#region Constructor (s) / Destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticSql"/> class.
        /// </summary>
        /// <param name="dataExchangeFactory">The data exchange factory.</param>
        /// <param name="dbHelperParameterCache">The db helper parameter cache.</param>
        /// <param name="statement">The statement.</param>
        public StaticSql(
            DataExchangeFactory dataExchangeFactory,
            DBHelperParameterCache dbHelperParameterCache,
            IStatement statement)
		{
            Contract.Require.That(dataExchangeFactory, Is.Not.Null).When("retrieving argument dataExchangeFactory in StaticSql constructor");
            Contract.Require.That(dbHelperParameterCache, Is.Not.Null).When("retrieving argument dbHelperParameterCache in StaticSql constructor");
            Contract.Require.That(statement, Is.Not.Null).When("retrieving argument statement in StaticSql constructor");

            this.statement = statement;
            this.dataExchangeFactory = dataExchangeFactory;
            this.dbHelperParameterCache = dbHelperParameterCache;
		}
		#endregion

		#region ISql Members

        /// <summary>
        /// Builds a new <see cref="RequestScope"/> and the sql command text to execute.
        /// </summary>
        /// <param name="mappedStatement">The <see cref="IMappedStatement"/>.</param>
        /// <param name="parameterObject">The parameter object (used in DynamicSql)</param>
        /// <param name="session">The current session</param>
        /// <returns>A new <see cref="RequestScope"/>.</returns>
		public RequestScope GetRequestScope(
            IMappedStatement mappedStatement,
			object parameterObject,
            ISession session)
		{
			RequestScope request = new RequestScope(dataExchangeFactory, session, statement);

			request.PreparedStatement = preparedStatement;
			request.MappedStatement = mappedStatement;

			return request;
		}

        /// <summary>
        /// Build the PreparedStatement
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="sql">The SQL.</param>
		public void BuildPreparedStatement(ISession session, string sql)
		{
            //ISession对象是DataMapperSession类对象，其中包含了数据库的连接与事物类对象 
            //statement则包含了sql语句字符串和参数类信息
			RequestScope request = new RequestScope( dataExchangeFactory, session, statement);
            //将分散的信息集中到PreparedStatementFactory中，其中sql参数是本次的数据库语句
            PreparedStatementFactory factory = new PreparedStatementFactory(session, dbHelperParameterCache, request, statement, sql);
			//获取PreparedStatement类
            preparedStatement = factory.Prepare(false);
		}

		#endregion
	}
}
