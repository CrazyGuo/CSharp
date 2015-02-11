using System;

namespace Study.Domains.Framework
{
    /// <summary>
    /// 领域实体基类
    /// </summary>
    public abstract class EntityBase : EntityBase<Guid> 
    {
        /// <summary>
        /// 初始化领域实体
        /// </summary>
        /// <param name="id">标识</param>
        protected EntityBase( Guid id )
            : base( id ) 
        {
        }
    }
}