using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Study.Domains.Framework.Repositories 
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public interface IRepository<TEntity, in TKey> where TEntity : class, IAggregateRoot<TKey> 
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Add( TEntity entity );
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entities">实体</param>
        void Add( IEnumerable<TEntity> entities );
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update( TEntity entity );
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">实体标识</param>
        void Remove( TKey id );
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Remove( TEntity entity );
        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="ids">实体编号集合</param>
        void Remove( IEnumerable<TKey> ids );
        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Remove( IEnumerable<TEntity> entities );
        /// <summary>
        /// 查找实体集合
        /// </summary>
        List<TEntity> FindAll();
        /// <summary>
        /// 查找实体集合
        /// </summary>
        IQueryable<TEntity> Find();
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">实体标识</param>
        TEntity Find( params object[] id );
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">实体标识列表</param>
        List<TEntity> Find( IEnumerable<TKey> ids );
        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        bool Exists( Expression<Func<TEntity, bool>> predicate );
        /// <summary>
        /// 索引器查找，获取指定标识的实体
        /// </summary>
        /// <param name="id">实体标识</param>
        TEntity this[TKey id] { get; }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query">查询对象</param>
        IQueryable<TEntity> Query( IQueryBase<TEntity> query );
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询对象</param>
        PagerList<TEntity> PagerQuery( IQueryBase<TEntity> query );
        /// <summary>
        /// 保存
        /// </summary>
        void Save();
        /// <summary>
        /// 清空实体
        /// </summary>
        void Clear();
        /// <summary>
        /// 清空缓存
        /// </summary>
        void ClearCache();
        /// <summary>
        /// 获取工作单元
        /// </summary>
        //IUnitOfWork GetUnitOfWork();
    }
}
