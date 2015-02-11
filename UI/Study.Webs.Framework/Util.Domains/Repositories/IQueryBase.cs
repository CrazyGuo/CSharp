using System;
using System.Linq.Expressions;

namespace Util.Domains.Repositories 
{
    /// <summary>
    /// 查询对象
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IQueryBase<TEntity> : IPager where TEntity : class, IAggregateRoot 
    {
        /// <summary>
        /// 获取谓词
        /// </summary>
        Expression<Func<TEntity, bool>> GetPredicate();
        /// <summary>
        /// 获取排序
        /// </summary>
        string GetOrderBy();
    }
}
