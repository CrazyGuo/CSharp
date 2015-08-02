using MyBatis.Common.Utilities.Objects;
using MyBatis.DataMapper;
using MyBatis.DataMapper.Data;
using MyBatis.DataMapper.MappedStatements;
using MyBatis.DataMapper.MappedStatements.PostSelectStrategy;
using MyBatis.DataMapper.MappedStatements.ResultStrategy;
using MyBatis.DataMapper.Model.ParameterMapping;
using MyBatis.DataMapper.Model.Statements;
using MyBatis.DataMapper.Scope;
using MyBatis.DataMapper.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Study.MyBatis
{
    public static class MappedStatementExtension
    {
        private static Regex rxColumns = new Regex(@"\A\s*SELECT\s+((?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|.)*?)(?<!,\s+)\bFROM\b", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex rxOrderBy = new Regex(@"\bORDER\s+BY\s+(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\w\(\)\.])+(?:\s+(?:ASC|DESC))?(?:\s*,\s*(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\w\(\)\.])+(?:\s+(?:ASC|DESC))?)*", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex rxDistinct = new Regex(@"\ADISTINCT\s", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        public static IList<T> QueryForListWithPage<T>(this IMappedStatement mappedStatement, ISession session, object paramObject, string orderby, int willShowedPage, int pageSize, ref int totalCount)
        {
            IStatement statement = mappedStatement.Statement;
            //statement.
            RequestScope request = statement.Sql.GetRequestScope(mappedStatement, paramObject, session);
            string statementsql = request.PreparedStatement.PreparedSql;

            //难点2 改写自PetaPoco

            return Page<T>(mappedStatement, request, session, willShowedPage, pageSize, statementsql, paramObject, ref totalCount);
        }

        public static DataTable RunQueryForDataTableWithPage(this IMappedStatement mappedStatement, ISession session, object paramObject, string orderby, int willShowedPage, int pageSize, ref int totalCount)
        {
            IStatement statement = mappedStatement.Statement;
            //statement.
            RequestScope request = statement.Sql.GetRequestScope(mappedStatement, paramObject, session);
            string statementsql = request.PreparedStatement.PreparedSql;

            //难点2 改写自PetaPoco
            return DataTablePage(mappedStatement, request, session, willShowedPage, pageSize, statementsql, paramObject, ref totalCount);
        }

        private static bool SplitSqlForPaging(string sql, out string sqlCount, out string sqlSelectRemoved, out string sqlOrderBy)
        {
            sqlSelectRemoved = null;
            sqlCount = null;
            sqlOrderBy = null;

            // Extract the columns from "SELECT <whatever> FROM"
            var m = rxColumns.Match(sql);
            if (!m.Success)
                return false;

            // Save column list and replace with COUNT(*)
            Group g = m.Groups[1];
            sqlSelectRemoved = sql.Substring(g.Index);

            if (rxDistinct.IsMatch(sqlSelectRemoved))
                sqlCount = sql.Substring(0, g.Index) + "COUNT(" + m.Groups[1].ToString().Trim() + ") " + sql.Substring(g.Index + g.Length);
            else
                sqlCount = sql.Substring(0, g.Index) + "COUNT(*) " + sql.Substring(g.Index + g.Length);


            // Look for an "ORDER BY <whatever>" clause
            m = rxOrderBy.Match(sqlCount);
            if (!m.Success)
            {
                sqlOrderBy = null;
            }
            else
            {
                g = m.Groups[0];
                sqlOrderBy = g.ToString();
                sqlCount = sqlCount.Substring(0, g.Index) + sqlCount.Substring(g.Index + g.Length);
            }

            return true;
        }

        private static void BuildPageQueries(IMappedStatement mappedStatement, RequestScope request, ISession session, long skip, long take, string sql, object args, out string sqlCount, out string sqlPage, ref int totalCount, object paramObject)
        {
            string dbType = session.SessionFactory.DataSource.DbProvider.Id.ToLower();

            // Split the SQL into the bits we need
            string sqlSelectRemoved, sqlOrderBy;
            if (!SplitSqlForPaging(sql, out sqlCount, out sqlSelectRemoved, out sqlOrderBy))
                throw new Exception("Unable to parse SQL statement for paged query");
            if (dbType.Contains("oracle") && sqlSelectRemoved.StartsWith("*"))
                throw new Exception("Query must alias '*' when performing a paged query.\neg. select t.* from table t order by t.id");

            //已经获取:查询数据总数SQL  分页SQL
            request.PreparedStatement.PreparedSql = sqlCount;
            mappedStatement.PreparedCommand.Create(request, session, mappedStatement.Statement, paramObject);
            totalCount =  GetCount(request, session, sqlCount);


            if (skip >= totalCount)
            {
                skip = totalCount - take;
            }

            // Build the SQL for the actual final result
            if (dbType.Contains("sqlserver") || dbType.Contains("oracle"))
            {
                sqlSelectRemoved = rxOrderBy.Replace(sqlSelectRemoved, "");
                if (rxDistinct.IsMatch(sqlSelectRemoved))
                {
                    sqlSelectRemoved = "peta_inner.* FROM (SELECT " + sqlSelectRemoved + ") peta_inner";
                }
                sqlPage = string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER ({0}) peta_rn, {1}) peta_paged WHERE peta_rn>{2} AND peta_rn<={3}",
                                        sqlOrderBy == null ? "ORDER BY (SELECT NULL)" : sqlOrderBy, sqlSelectRemoved, skip, skip + take);
                //args = args.Concat(new object[] { skip, skip + take }).ToArray();
            }
            else if (dbType.Contains("sqlserverce"))
            {
                sqlPage = string.Format("{0}\nOFFSET @{1} ROWS FETCH NEXT @{2} ROWS ONLY", sql, skip, skip + take);
                //args = args.Concat(new object[] { skip, take }).ToArray();
            }
            else
            {
                sqlPage = string.Format("{0}\nLIMIT @{1} OFFSET @{2}", sql, skip, skip + take);
                //args = args.Concat(new object[] { take, skip }).ToArray();
            }

        }

        private static IList<T> Page<T>(IMappedStatement mappedStatement, RequestScope request, ISession session, long page, long itemsPerPage, string sql, object paramObject, ref int totalCount)
        {
            string sqlCount, sqlPage, oldPreparedSql = "";
            oldPreparedSql = request.PreparedStatement.PreparedSql;
            BuildPageQueries(mappedStatement, request, session, (page - 1) * itemsPerPage, itemsPerPage, sql, paramObject, out sqlCount, out sqlPage, ref totalCount, paramObject);


            request.PreparedStatement.PreparedSql = sqlPage;//
            mappedStatement.PreparedCommand.Create(request, session, mappedStatement.Statement, paramObject);

            //难点三: 此处代码拷贝的MappedStatement的，就是增加了相关参数
            IList<T> result = RunQueryForList<T>(mappedStatement.Statement, request, session, paramObject, null, null);
            request.PreparedStatement.PreparedSql = oldPreparedSql;
            return result;
        }

        private static DataTable DataTablePage(IMappedStatement mappedStatement, RequestScope request, ISession session, long page, long itemsPerPage, string sql, object paramObject, ref int totalCount)
        {
            string sqlCount, sqlPage;
            BuildPageQueries(mappedStatement, request, session, (page - 1) * itemsPerPage, itemsPerPage, sql, paramObject, out sqlCount, out sqlPage, ref totalCount, paramObject);

            request.PreparedStatement.PreparedSql = sqlPage;
            mappedStatement.PreparedCommand.Create(request, session, mappedStatement.Statement, paramObject);
            //难点三: 此处代码拷贝的MappedStatement的，就是增加了相关参数
            DataTable result=RunQueryForDataTable(mappedStatement.Statement, request, session, paramObject);
            return result;
        }

        private static int GetCount(RequestScope request, ISession sqlMapSession, string cmdCountSql)
        {
            int totalCount = 0;
            object countObject = 0;
            string error;
            IDbCommand cmdCount = request.IDbCommand;
            try
            {
                cmdCount.CommandText = cmdCountSql;
                countObject = cmdCount.ExecuteScalar();
                totalCount = Convert.ToInt32(countObject);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return totalCount;
        }

        internal static IList RunQueryForList(IStatement statement, RequestScope request, ISession session, object parameterObject, IList resultObject, RowDelegate rowDelegate)
        {
            IResultStrategy resultStrategy = ResultStrategyFactory.Get(statement);
            IList list = resultObject;
            
            using (IDbCommand command = request.IDbCommand)
            {
                if (resultObject == null)
                {
                    if (statement.ListClass == null)
                    {
                        list = new ArrayList();
                    }
                    else
                    {
                        list = statement.CreateInstanceOfListClass();
                    }
                }

                IDataReader reader = command.ExecuteReader();

                try
                {
                    do
                    {
                        if (rowDelegate == null)
                        {
                            //***
                            IList currentList = null;
                            if (request.Statement.ResultsMap.Count == 1)
                            {
                                currentList = list;
                            }
                            else
                            {
                                if (request.CurrentResultMap != null)
                                {
                                    Type genericListType = typeof(List<>).MakeGenericType(new Type[] { request.CurrentResultMap.Class });
                                    currentList = (IList)Activator.CreateInstance(genericListType);
                                }
                                else
                                {
                                    currentList = new ArrayList();
                                }
                                list.Add(currentList);

                            }
                            //***
                            while (reader.Read())
                            {
                                //将reader当前行中的所有字段加入到IList对象中，即obj中
                                object obj = resultStrategy.Process(request, ref reader, null);
                                if (obj != BaseStrategy.SKIP)
                                {
                                    //list.Add(obj);
                                    currentList.Add(obj);
                                }
                            }
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                object obj = resultStrategy.Process(request, ref reader, null);
                                rowDelegate(obj, parameterObject, list);
                            }
                        }
                    }
                    while (reader.NextResult());
                }
                finally
                {
                    reader.Close();
                    reader.Dispose();
                }

                ExecuteDelayedLoad(request);
                RetrieveOutputParameters(request, session, command, parameterObject);
            }

            return list;
        }

        internal static IList<T> RunQueryForList<T>(IStatement statement, RequestScope request, ISession session, object parameterObject, IList<T> resultObject, RowDelegate<T> rowDelegate)
        {
            IResultStrategy resultStrategy = ResultStrategyFactory.Get(statement);
            IList<T> list = resultObject;

            using (IDbCommand command = request.IDbCommand)
            {
                if (resultObject == null)
                {
                    if (statement.ListClass == null)
                    {
                        list = new List<T>();
                    }
                    else
                    {
                        list = statement.CreateInstanceOfGenericListClass<T>();
                    }
                }

                IDataReader reader = command.ExecuteReader();
                try
                {
                    do
                    {
                        if (rowDelegate == null)
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    object obj = resultStrategy.Process(request, ref reader, null);
                                    if (obj != BaseStrategy.SKIP)
                                    {
                                        list.Add((T)obj);
                                    }
                                }
                                catch(Exception ed)
                                {
                                    string ms = ed.Message;
                                }
                            }
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                T obj = (T)resultStrategy.Process(request, ref reader, null);
                                rowDelegate(obj, parameterObject, list);
                            }
                        }
                    }
                    while (reader.NextResult());
                }
                finally
                {
                    reader.Close();
                    reader.Dispose();
                }

                ExecuteDelayedLoad(request);
                RetrieveOutputParameters(request, session, command, parameterObject);
            }

            return list;
        }

        internal static DataTable RunQueryForDataTable(IStatement statement, RequestScope request, ISession session, object parameterObject)
        {
            IResultStrategy resultStrategy = ResultStrategyFactory.Get(statement);
            DataTable dataTable = new DataTable("DataTable");

            using (IDbCommand command = request.IDbCommand)
            {
                IDataReader reader = command.ExecuteReader();

                try
                {
                    // Get Results
                    while (reader.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                        resultStrategy.Process(request, ref reader, dataRow);
                    }
                }
                finally
                {
                    reader.Close();
                    reader.Dispose();
                }

                // do we need ??
                //ExecuteDelayedLoad(request);

                // do we need ??
                //RetrieveOutputParameters(request, session, command, parameterObject);
            }

            return dataTable;
        }

        internal static T RunQueryForObject<T>(IStatement statement, RequestScope request, ISession session, object parameterObject, T resultObject)
        {
            IResultStrategy resultStrategy = ResultStrategyFactory.Get(statement);
            T result = resultObject;

            using (IDbCommand command = request.IDbCommand)
            {
                IDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        object obj = resultStrategy.Process(request, ref reader, resultObject);
                        if (obj != BaseStrategy.SKIP)
                        {
                            result = (T)obj;
                        }
                    }
                }
                finally
                {
                    reader.Close();
                    reader.Dispose();
                }

                ExecuteDelayedLoad(request);

                #region remark
                // If you are using the OleDb data provider, you need to close the
                // DataReader before output parameters are visible.
                #endregion

                RetrieveOutputParameters(request, session, command, parameterObject);
            }

            return result;
        }

        private static void ExecuteDelayedLoad(RequestScope request)
        {
            while (request.DelayedLoad.Count > 0)
            {
                PostBindind postSelect = request.DelayedLoad.Dequeue();

                PostSelectStrategyFactory.Get(postSelect.Method).Execute(postSelect, request);
            }
        }

        private static void RetrieveOutputParameters(RequestScope request, ISession session, IDbCommand command, object result)
        {
            if (request.ParameterMap != null && request.ParameterMap.HasOutputParameter)
            {
                int count = request.ParameterMap.PropertiesList.Count;
                for (int i = 0; i < count; i++)
                {
                    ParameterProperty mapping = request.ParameterMap.GetProperty(i);
                    if (mapping.Direction == ParameterDirection.Output ||
                        mapping.Direction == ParameterDirection.InputOutput)
                    {
                        string parameterName = string.Empty;
                        if (session.SessionFactory.DataSource.DbProvider.UseParameterPrefixInParameter == false)
                        {
                            parameterName = mapping.ColumnName;
                        }
                        else
                        {
                            parameterName = session.SessionFactory.DataSource.DbProvider.ParameterPrefix +
                                mapping.ColumnName;
                        }

                        if (mapping.TypeHandler == null) // Find the TypeHandler
                        {
                            lock (mapping)
                            {
                                if (mapping.TypeHandler == null)
                                {
                                    Type propertyType = ObjectProbe.GetMemberTypeForGetter(result, mapping.PropertyName);

                                    mapping.TypeHandler = request.DataExchangeFactory.TypeHandlerFactory.GetTypeHandler(propertyType);
                                }
                            }
                        }

                        // Fix IBATISNET-239
                        //"Normalize" System.DBNull parameters
                        IDataParameter dataParameter = (IDataParameter)command.Parameters[parameterName];
                        object dbValue = dataParameter.Value;

                        object value = null;

                        bool wasNull = (dbValue == DBNull.Value);
                        if (wasNull)
                        {
                            if (mapping.HasNullValue)
                            {
                                value = mapping.TypeHandler.ValueOf(mapping.GetAccessor.MemberType, mapping.NullValue);
                            }
                            else
                            {
                                value = mapping.TypeHandler.NullValue;
                            }
                        }
                        else
                        {
                            value = mapping.TypeHandler.GetDataBaseValue(dataParameter.Value, result.GetType());
                        }

                        request.IsRowDataFound = request.IsRowDataFound || (value != null);

                        request.ParameterMap.SetOutputParameter(ref result, mapping, value);
                    }
                }
            }
        }
    }
}
