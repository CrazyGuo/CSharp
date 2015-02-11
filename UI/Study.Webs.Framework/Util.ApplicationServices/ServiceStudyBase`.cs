using System;
using Util.Datas;
using Util.Domains;
using Util.Domains.Repositories;

namespace Util.ApplicationServices
{
    public abstract class ServiceStudyBase<TEntity, TDto, TQuery> : ServiceStudyBase<TEntity, Guid, TDto, TQuery>
        where TEntity : AggregateRoot<Guid>
        where TDto : new()
        where TQuery : IPager
    {
        protected ServiceStudyBase()
            : base( ) 
        {
        }
    }
}
