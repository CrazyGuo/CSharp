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
using System.Text;
using MyBatis.DataMapper.Configuration.Interpreters.Config;
using MyBatis.DataMapper.Configuration.Serializers;
using MyBatis.DataMapper.MappedStatements;
using MyBatis.DataMapper.Model.ParameterMapping;
using MyBatis.DataMapper.Model.Sql.Dynamic;
using MyBatis.DataMapper.Model.Sql.Dynamic.Elements;
using MyBatis.DataMapper.Model.Sql.External;
using MyBatis.DataMapper.Model.Sql.SimpleDynamic;
using MyBatis.DataMapper.Model.Sql.Static;
using MyBatis.DataMapper.Model.Statements;
using MyBatis.DataMapper.Session;
using MyBatis.Common.Configuration;
using MyBatis.Common.Contracts;
using MyBatis.Common.Contracts.Constraints;
using MyBatis.Common.Exceptions;

namespace MyBatis.DataMapper.Configuration
{
    /// <summary>
    /// This implementation of <see cref="IConfigurationStore"/>, builds all statement.
    /// </summary>
    public partial class DefaultModelBuilder
    {
        private readonly InlineParameterMapParser paramParser = new InlineParameterMapParser();

        /// <summary>
        /// Builds the mapped statements.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="configurationSetting"></param>
        private void BuildMappedStatements(IConfigurationStore store, ConfigurationSetting configurationSetting)
        {
            for (int i = 0; i < store.Statements.Length; i++)
            {
                //对应statement  select update delete insert procedure节点配置信息
                IConfiguration statementConfig = store.Statements[i];
                IMappedStatement mappedStatement = null;

                switch (statementConfig.Type)//statements节点下的6大类型节点信息
                {
                    case ConfigConstants.ELEMENT_STATEMENT:
                        mappedStatement = BuildStatement(statementConfig, configurationSetting);
                        break;
                    case ConfigConstants.ELEMENT_SELECT:
                        mappedStatement = BuildSelect(statementConfig, configurationSetting);
                        break;
                    case ConfigConstants.ELEMENT_INSERT:
                        mappedStatement = BuildInsert(statementConfig, configurationSetting);
                        break;
                    case ConfigConstants.ELEMENT_UPDATE:
                        mappedStatement = BuildUpdate(statementConfig, configurationSetting);
                        break;
                    case ConfigConstants.ELEMENT_DELETE:
                        mappedStatement = BuildDelete(statementConfig, configurationSetting);
                        break;
                    case ConfigConstants.ELEMENT_PROCEDURE:
                        mappedStatement = BuildProcedure(statementConfig, configurationSetting);
                        break;
                    case ConfigConstants.ELEMENT_SQL:
                        break;
                    default:
                        throw new ConfigurationException("Cannot build the statement, cause invalid statement type '" + statementConfig.Type + "'.");

                }
                if (mappedStatement!=null)
                {
                    //最后的结果是加入到了DefaultModleStore中
                    modelStore.AddMappedStatement(mappedStatement);
                }
            }
        }

        /// <summary>
        /// Builds a Mapped Statement for a statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <param name="mappedStatement">The mapped statement.</param>
        /// <returns></returns>
        private IMappedStatement BuildCachingStatement(IStatement statement, MappedStatement mappedStatement)
        {
            IMappedStatement mapStatement = mappedStatement;
            if (statement.CacheModel != null && isCacheModelsEnabled)
            {
                mapStatement = new CachingStatement(mappedStatement);
            }
            return mapStatement;
        }

        /// <summary>
        /// Builds a <see cref="Statement"/> for a statement configuration.
        /// </summary>
        /// <param name="statementConfig">The statement config.</param>
        /// <param name="configurationSetting"></param>
        private IMappedStatement BuildStatement(IConfiguration statementConfig, ConfigurationSetting configurationSetting)
        {
            BaseStatementDeSerializer statementDeSerializer = new StatementDeSerializer();
            //解析statement节点属性到类中
            IStatement statement = statementDeSerializer.Deserialize(modelStore, statementConfig, configurationSetting);
            //估计是处理statement节点对应的文本内容  ----->确实如此 此处是个核心 一个statement节点的配置信息 和 对应的内存类
            //处理的结果放在了statement中 包括了要执行的sql语句和参数
            ProcessSqlStatement(statementConfig, statement);

            //具体封装了IDbcommand IDataReader的一些类
            MappedStatement mappedStatement = new MappedStatement(modelStore, statement);

            //放入缓存类中 并返回mappedStatement
            return BuildCachingStatement(statement, mappedStatement);
        }

        /// <summary>
        /// Builds an <see cref="Insert"/> for a insert configuration.
        /// </summary>
        /// <param name="statementConfig">The statement config.</param>
        /// <param name="configurationSetting"></param>
        private IMappedStatement BuildInsert(IConfiguration statementConfig, ConfigurationSetting configurationSetting)
        {
            BaseStatementDeSerializer insertDeSerializer = new InsertDeSerializer();
            IStatement statement = insertDeSerializer.Deserialize(modelStore, statementConfig, configurationSetting);
            ProcessSqlStatement(statementConfig, statement);
            MappedStatement mappedStatement = new InsertMappedStatement(modelStore, statement);
            Insert insert = (Insert)statement;
            if (insert.SelectKey != null)
            {
                ConfigurationCollection selectKeys = statementConfig.Children.Find(ConfigConstants.ELEMENT_SELECTKEY);
                IConfiguration selectKeyConfig = selectKeys[0];

                ProcessSqlStatement(selectKeyConfig, insert.SelectKey);
                MappedStatement mapStatement = new MappedStatement(modelStore, insert.SelectKey);
                modelStore.AddMappedStatement(mapStatement);
            }

            return BuildCachingStatement(statement, mappedStatement);
        }

        /// <summary>
        /// Builds an <see cref="Statement"/> for a statement configuration.
        /// </summary>
        /// <param name="statementConfig">The statement config.</param>
        /// <param name="configurationSetting"></param>
        private IMappedStatement BuildUpdate(IConfiguration statementConfig, ConfigurationSetting configurationSetting)
        {
            BaseStatementDeSerializer updateDeSerializer = new UpdateDeSerializer();
            IStatement statement = updateDeSerializer.Deserialize(modelStore, statementConfig, configurationSetting);
            ProcessSqlStatement(statementConfig, statement);
            MappedStatement mappedStatement = new UpdateMappedStatement(modelStore, statement);

            return BuildCachingStatement(statement, mappedStatement);

        }

        /// <summary>
        /// Builds an <see cref="Delete"/> for a delete configuration.
        /// </summary>
        /// <param name="statementConfig">The statement config.</param>
        /// <param name="configurationSetting"></param>
        private IMappedStatement BuildDelete(IConfiguration statementConfig, ConfigurationSetting configurationSetting)
        {
            BaseStatementDeSerializer deleteDeSerializer = new DeleteDeSerializer();
            IStatement statement = deleteDeSerializer.Deserialize(modelStore, statementConfig, configurationSetting);
            ProcessSqlStatement(statementConfig, statement);
            MappedStatement mappedStatement = new DeleteMappedStatement(modelStore, statement);

            return BuildCachingStatement(statement, mappedStatement);

        }

        /// <summary>
        /// Builds an <see cref="Select"/> for a select configuration.
        /// </summary>
        /// <param name="statementConfig">The statement config.</param>
        /// <param name="configurationSetting"></param>
        private IMappedStatement BuildSelect(IConfiguration statementConfig, ConfigurationSetting configurationSetting)
        {
            BaseStatementDeSerializer selectDeSerializer = new SelectDeSerializer();
            IStatement statement = selectDeSerializer.Deserialize(modelStore, statementConfig, configurationSetting);
            ProcessSqlStatement(statementConfig, statement);
            MappedStatement mappedStatement = new SelectMappedStatement(modelStore, statement);

            return BuildCachingStatement(statement, mappedStatement);

        }

        /// <summary>
        /// Builds an <see cref="Procedure"/> for a procedure configuration.
        /// </summary>
        /// <param name="statementConfig">The statement config.</param>
        /// <param name="configurationSetting"></param>
        private IMappedStatement BuildProcedure(IConfiguration statementConfig, ConfigurationSetting configurationSetting)
        {
            BaseStatementDeSerializer procedureDeSerializer = new ProcedureDeSerializer();
            IStatement statement = procedureDeSerializer.Deserialize(modelStore, statementConfig, configurationSetting);
            ProcessSqlStatement(statementConfig, statement);
            MappedStatement mappedStatement = new MappedStatement(modelStore, statement);

            return BuildCachingStatement(statement, mappedStatement);
        }

        /// <summary>
        /// Process the Sql cpmmand text statement (Build ISql)
        /// </summary>
        /// <param name="statementConfiguration">The statement configuration.</param>
        /// <param name="statement">The statement.</param>
        private void ProcessSqlStatement(IConfiguration statementConfiguration, IStatement statement)
        {
            //判断sqlSource属性值的存在与否 对应不同的处理方法
            if(dynamicSqlEngine!=null)
            {
                statement.SqlSource = dynamicSqlEngine;
            }

            if (statement.SqlSource!=null)//外部处理SQL语句方式
            {
                #region sqlSource - external processor
                string commandText = string.Empty;

                if (statementConfiguration.Children.Count > 0)
                {
                    IConfiguration child = statementConfiguration.Children[0];
                    if (child.Type == ConfigConstants.ELEMENT_TEXT || child.Type == ConfigConstants.ELEMENT_CDATA)
                    {
                        // pass the unformated sql to the external processor
                        commandText = child.Value;
                    }
                }

                ExternalSql externalSql = new ExternalSql(modelStore, statement, commandText);
                //最终的处理结果
                statement.Sql = externalSql; //(赋值到ISql类对象)
                #endregion
            }
            else//当前默认处理SQL语句的方式
            {
                #region default - internal processor
                bool isDynamic = false;

                //初始化DynamicSql的成员变量而已
                DynamicSql dynamic = new DynamicSql(
                    modelStore.SessionFactory.DataSource.DbProvider.UsePositionalParameters,
                    modelStore.DBHelperParameterCache,
                    modelStore.DataExchangeFactory,
                    statement);
                StringBuilder sqlBuffer = new StringBuilder();
                //解析SQL文本语句  这个函数使用了递归处理 结果保存到了 sqlBuffer和dynamic中  
                //注意：sqlBuffer中应该是一个完整的SQL语句,dynamic最后将当前的select update insert等节点信息保存在了内部类SqlText和SqlTag类中了
                isDynamic = ParseDynamicTags(statementConfiguration, dynamic, sqlBuffer, isDynamic, false, statement);

                if (isDynamic)//如果是动态语句
                {
                    statement.Sql = dynamic;
                }
                else
                {
                    //此处的sqlText得到的是一个查询语句的完整状态 但包含参数符号信息
                    string sqlText = sqlBuffer.ToString();
                    string newSqlCommandText = string.Empty;
                    //估计是实现将参数属性类 合并到 参数集合中去----->根据完整的语句中的参数符号标志分析到一个参数ParameterMap类中
                   //确实如此 参数放入ParameterMap中  参数类没有细分 都放到了map中 当前节点完整SQL放入到newSqlCommandText中
                    ParameterMap map = inlineParemeterMapBuilder.BuildInlineParemeterMap(statement, sqlText, out newSqlCommandText);
                    if (map != null)
                    {
                        statement.ParameterMap = map;
                    }

                    //如果包含$字符 就是简单SQL
                    if (SimpleDynamicSql.IsSimpleDynamicSql(newSqlCommandText))
                    {
                        statement.Sql = new SimpleDynamicSql(
                            modelStore.DataExchangeFactory,
                            modelStore.DBHelperParameterCache,
                            newSqlCommandText,
                            statement);
                    }
                    else
                    {
                        //如果是存储过程
                        if (statement is Procedure)
                        {
                            statement.Sql = new ProcedureSql(
                                modelStore.DataExchangeFactory,
                                modelStore.DBHelperParameterCache,
                                newSqlCommandText,
                                statement);
                            // Could not call BuildPreparedStatement for procedure because when NUnit Test
                            // the database is not here (but in theory procedure must be prepared like statement)
                            // It's even better as we can then switch DataSource.
                        }
                        else if (statement is Statement)//如果是update delete insert select等类型
                        {
                            statement.Sql = new StaticSql(
                                modelStore.DataExchangeFactory,
                                modelStore.DBHelperParameterCache,
                                statement);

                            // this does not open a connection to the database
                            //获取的是DefaultSessionFactory中的DataMapperSession对象
                            ISession session = modelStore.SessionFactory.OpenSession();

                            //完成对StaticSql对象中的PreparedStatement对SQL语句参数到数据库参数的对应解析
                            ((StaticSql)statement.Sql).BuildPreparedStatement(session, newSqlCommandText);

                            session.Close();
                        }
                    } 
                }                
                #endregion
            }                

            Contract.Ensure.That(statement.Sql, Is.Not.Null).When("process Sql statement.");
        }


        /// <summary>
        /// Parse dynamic tags
        /// </summary>
        /// <param name="statementConfig">The statement config.</param>
        /// <param name="dynamic">The dynamic.</param>
        /// <param name="sqlBuffer">The SQL buffer.</param>
        /// <param name="isDynamic">if set to <c>true</c> [is dynamic].</param>
        /// <param name="postParseRequired">if set to <c>true</c> [post parse required].</param>
        /// <param name="statement">The statement.</param>
        /// <returns></returns>
        private bool ParseDynamicTags(
            IConfiguration statementConfig, 
            IDynamicParent dynamic,
            StringBuilder sqlBuffer, 
            bool isDynamic, 
            bool postParseRequired, 
            IStatement statement)
        {
            //具体文本的集合 可能是需要拼接成完整的SQL语句
            ConfigurationCollection children = statementConfig.Children;
            int count = children.Count;
            for (int i = 0; i < count; i++)
            {
                IConfiguration child = children[i];
                if (child.Type == ConfigConstants.ELEMENT_TEXT || child.Type == ConfigConstants.ELEMENT_CDATA)
                {
                    //第一步处理    获取当前文本的一个值 并处理"\r\n"
                    string childValueString = child.Value;
                    if (statement.PreserveWhitespace)
                    {
                        childValueString = childValueString .Replace('\n', ' ').Replace('\r', ' ').Replace('\t', ' ').Trim(); 
                    }

                    //第二部处理   将处理的当前一个部分SQL语句放入到SqlText类中
                    SqlText sqlText = null;
                    if (postParseRequired)
                    {
                        sqlText = new SqlText();
                        sqlText.Text = childValueString;
                    }
                    else
                    {
                        //分析SQL语句中的参数 例如# # ,@{}等内部参数 最后的结果是一个语句保存 和 参数列表在SqlText类中
                        sqlText = InlineParameterMapParser.ParseInlineParameterMap(modelStore.DataExchangeFactory, statementConfig.Id, null, childValueString);
                    }
                    //将此次分析的SQL语句结果保存到dynamic类对象中 可能需要添加多次 
                    dynamic.AddChild(sqlText);//按顺序放置 最后拼接即可

                    //sqlBuffer字符串拼接每一部分的sql语句 最后是当前节点的完整语句
                    sqlBuffer.Append(" " + childValueString);
                }
                else if (child.Type == ConfigConstants.ELEMENT_SELECTKEY || child.Type == ConfigConstants.ELEMENT_INCLUDE)
                {
                    //此处没有处理代码
                }
                else
                {
                    //从deSerializerFactory工厂类的字典serializerMap中获取对应的处理IDeSerializer类
                    IDeSerializer serializer = deSerializerFactory.GetDeSerializer(child.Type);//child.Type就是节点的名

                    if (serializer != null)
                    {
                        isDynamic = true;
                        SqlTag tag;

                        //当前child是一个element 进行解析
                        tag = serializer.Deserialize(child);

                        //添加到IList<ISqlChild> children中
                        dynamic.AddChild(tag);

                        if (child.Children.Count > 0)
                        {
                            //递归处理子节点 注意是子节点Tag了 递归下去 但sqlBuffer字符串一直是同一个
                            isDynamic = ParseDynamicTags(child, tag, sqlBuffer, isDynamic, tag.Handler.IsPostParseRequired, statement);
                        }
                    }
                }
            }

            return isDynamic;
        }




    }
}
