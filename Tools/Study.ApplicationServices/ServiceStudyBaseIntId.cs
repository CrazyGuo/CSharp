using System;
using Study.Domains.Framework;
using Study.Domains.Framework.Repositories;

namespace Study.ApplicationServices
{
    public abstract class ServiceStudyBaseIntId<TEntity, TDto, TQuery> : ServiceStudyBase<TEntity, int, TDto, TQuery>
        where TEntity : AggregateRoot<int>
        where TDto : new()
        where TQuery : IPager
    {
        protected ServiceStudyBaseIntId(): base()
        {

        }
    }
}
