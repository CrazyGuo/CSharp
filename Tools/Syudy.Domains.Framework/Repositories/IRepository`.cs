using System;

namespace Study.Domains.Framework.Repositories 
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : class, IAggregateRoot<Guid> 
    {
    }
}
