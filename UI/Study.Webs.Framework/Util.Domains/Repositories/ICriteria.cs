using System;
using System.Linq.Expressions;

namespace Util.Domains.Repositories 
{
    /// <summary>
    /// 查询条件
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface ICriteria<TEntity> where TEntity : class,IAggregateRoot 
    {
        /// <summary>
        /// 获取谓词
        /// </summary>
        Expression<Func<TEntity, bool>> GetPredicate();
    }
}
