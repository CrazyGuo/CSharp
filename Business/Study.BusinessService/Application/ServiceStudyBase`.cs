using Study.Domains.Framework;
using Study.Domains.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.BusinessService.Application
{
    public abstract class ServiceStudyBase<TEntity, TDto, TQuery> : ServiceStudyBase<TEntity, Guid, TDto, TQuery>
        where TEntity : AggregateRoot<Guid>
        where TDto : new()
        where TQuery : IPager
    {
        protected ServiceStudyBase()
            : base()
        {
        }
    }

}
