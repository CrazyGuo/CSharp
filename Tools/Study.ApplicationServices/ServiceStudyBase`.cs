using System;
using Study.Domains.Framework;
using Study.Domains.Framework.Repositories;

namespace Study.ApplicationServices
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
