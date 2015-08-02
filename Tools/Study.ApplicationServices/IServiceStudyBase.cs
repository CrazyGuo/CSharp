using System.Collections.Generic;
using Study.Domains.Framework.Repositories;
using AopHelper;

namespace Study.ApplicationServices
{
    public interface IServiceStudyBase<TDto, in TQuery> where TDto : new()
    {
        TDto Create();

        void Add(TDto parameter);
        void Delete(string id);
        int Update(TDto parameter);
        TDto FetchOne(string id);

        void BatchAdd(IList<TDto> parameters);
        void BatchDelete(string ids);
        IList<TDto> FetchMany(string ids);

        PagerList<TDto> FetchPages(TQuery query);
        IList<TDto> FetchAll(TQuery query);
        IList<TDto> FetchAll( );
    }
}
