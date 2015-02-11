using System.Collections.Generic;
using System.Linq;
using Util.Datas;
using Util.Domains;
using Util.Domains.Repositories;
using Study.MyBatis.Service;

namespace Util.ApplicationServices 
{
    /// <summary>
    /// 应用服务
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQuery">查询实体类型</typeparam>
    public abstract class ServiceBase<TEntity, TKey, TDto, TQuery> :SqlServerSevice, IServiceBase<TDto, TQuery>
        where TEntity : class, IAggregateRoot<TKey>
        where TDto : new()
        where TQuery : IPager
    {

        #region 构造方法

        /// <summary>
        /// 初始化应用服务
        /// </summary>
        protected ServiceBase( ) 
        {
            //UnitOfWork = unitOfWork;
            //_repository = repository;
            _logBefore = string.Empty;
        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 更新前日志
        /// </summary>
        private string _logBefore;

        /// <summary>
        /// 仓储
        /// </summary>
        private readonly IRepository<TEntity, TKey> _repository;

        /// <summary>
        /// 工作单元
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; private set; }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 记录更新前实体状态
        /// </summary>
        /// <param name="entity">实体</param>
        protected void LogBefore( TEntity entity ) 
        {
            _logBefore = entity.ToString();
        }

        /// <summary>
        /// 转换为数据传输对象
        /// </summary>
        /// <param name="entity">实体</param>
        protected abstract TDto ToDto( TEntity entity );

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        protected abstract TEntity ToEntity( TDto dto );

        #endregion

        #region Create(创建实体)

        /// <summary>
        /// 创建实体
        /// </summary>
        public virtual TDto Create() 
        {
            return new TDto();
        }

        #endregion

        #region Get(通过编号获取实体)

        /// <summary>
        /// 通过编号获取实体
        /// </summary>
        /// <param name="id">实体编号</param>
        public virtual TDto Get( object id ) 
        {
            return ToDto( _repository.Find( Conv.To<TKey>( id ) ) );
        }

        #endregion

        #region GetAll(获取全部列表)

        /// <summary>
        /// 获取全部列表
        /// </summary>
        public virtual List<TDto> GetAll() 
        {
            return _repository.FindAll().Select( ToDto ).ToList();
        }

        #endregion

        #region Query(查询)

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param">查询参数</param>
        public virtual PagerList<TDto> Query( TQuery param ) 
        {
            var query = GetQuery( param );
            //var queryable = _repository.Query( query ).OrderBy( query.GetOrderBy() ).Pager( query );
            //return queryable.ToPageList( query ).Convert( ToDto );
            return null;
        }

        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <param name="param">查询参数</param>
        public abstract IQueryBase<TEntity> GetQuery( TQuery param );

        #endregion

        #region Save(保存)

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        public virtual void Save( TDto dto ) 
        {
            UnitOfWork.Start();
            var entity = ToEntity( dto );
            if ( entity.Id.Equals( default( TKey ) ) )
                Add( entity );
            else
                Update( entity );
            UnitOfWork.Commit();
        }

        /// <summary>
        /// 添加
        /// </summary>
        protected virtual void Add( TEntity entity ) 
        {
            entity.Init();
            entity.Validate();
            _repository.Add( entity );
        }

        /// <summary>
        /// 修改
        /// </summary>
        protected virtual void Update( TEntity entity ) 
        {
            var dbEntity = _repository.Find( entity.Id );
            LogBefore( dbEntity );
            entity.Validate();
            //MapFactory.Create().Map( entity, dbEntity );
        }

        #endregion

        #region Delete(删除)

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">Id集合字符串，多个Id用逗号分隔</param>
        public virtual void Delete( string ids ) 
        {
            var idList = Conv.ToList<TKey>( ids );
            var entities = _repository.Find( idList );
            _repository.Remove( entities );
        }

        #endregion
    }
}