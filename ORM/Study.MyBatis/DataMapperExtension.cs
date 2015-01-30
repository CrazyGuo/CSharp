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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace Study.MyBatis
{
    public static class DataMapperExtension
    {
        //private const string PageSql = "with cte as( select id0=row_number() over(order by {0}),* from  ({1}) as cte1) select * from cte where id0 between @beginNo and @endNo";

        //private const string CountSql = "select count(*) {0}";

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="mapper">mapper</param>
        /// <param name="tag">SQL Statement的id</param>
        /// <param name="paramObject">参数</param>
        /// <param name="orderby">查询条件，必须确保数据库中有这一列</param>
        /// <param name="willShowedPage">开始行数</param>
        /// <param name="pageSize">结束行数</param>
        /// <param name="totalCount">总条数</param>
        /// <returns>查询结果</returns>
        public static IList<T> QueryForListWithPage<T>(this IDataMapper mapper, string tag, object paramObject, string orderby, int willShowedPage, int pageSize, ref int totalCount)
        {
            DataMapper dataMaper = (DataMapper)mapper;
            IMappedStatement mappedStatement = dataMaper.GetMappedStatement(tag);
            using (var sessionScope = new DataMapperLocalSessionScope(mappedStatement.ModelStore.SessionStore, mappedStatement.ModelStore.SessionFactory))
            {
                return mappedStatement.QueryForListWithPage<T>(sessionScope.Session, paramObject, orderby, willShowedPage, pageSize, ref totalCount);
            }
        }

        public static DataTable RunQueryForDataTableWithPage(this IDataMapper mapper, string tag, object paramObject, string orderby, int willShowedPage, int pageSize, ref int totalCount)
        {
            DataMapper dataMaper = (DataMapper)mapper;
            IMappedStatement mappedStatement = dataMaper.GetMappedStatement(tag);
            using (var sessionScope = new DataMapperLocalSessionScope(mappedStatement.ModelStore.SessionStore, mappedStatement.ModelStore.SessionFactory))
            {
                return mappedStatement.RunQueryForDataTableWithPage(sessionScope.Session, paramObject, orderby, willShowedPage, pageSize, ref totalCount);
            }
        }   
    }
}
