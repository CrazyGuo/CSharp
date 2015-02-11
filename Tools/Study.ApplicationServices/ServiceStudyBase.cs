using System.Collections.Generic;
using Study.Util;
using Study.Domains.Framework;
using Study.Domains.Framework.Repositories;
using Study.MyBatis;
using Study.MyBatis.Service;

namespace Study.ApplicationServices
{
    public abstract class ServiceStudyBase<TEntity, TKey, TDto, TQuery> : SqlServerSevice, IServiceStudyBase<TDto, TQuery>
        where TEntity : class, IAggregateRoot<TKey>
        where TDto : new()
        where TQuery : IPager
    {
        //protected IUnitOfWork UnitOfWork { get; private set; }

        protected ServiceStudyBase( ) 
        {
            //UnitOfWork = unitOfWork;
        }

        protected abstract TDto ToDto(TEntity entity);

        protected abstract TEntity ToEntity(TDto dto);

        public virtual TDto Create()
        {
            return new TDto();
        }

        #region Default
        
        
        public virtual void Add(TDto parameter)
        {
            //var entity = ToEntity(parameter);
            string insertId = GetAddId();
            DataMapper.Insert(insertId, parameter);
        }

        public virtual void Delete(string id)
        {
            string deleteId = GetDeleteId();
            DataMapper.Delete(deleteId, id);
        }

        public virtual int Update(TDto parameter)
        {
            //var entity = ToEntity(parameter);
            string updateId = GetUpdateId();
            return DataMapper.Update(updateId, parameter);
        }

        public virtual void BatchAdd(IList<TDto> parameters)
        {
            foreach(var item in parameters)
            {
                Add(item);
            }
        }

        public virtual void BatchDelete(string ids)
        {
            var idList = Conv.ToList<string>(ids);
            foreach(var id in idList)
            {
                Delete(id);
            }
        }

        public virtual TDto FetchOne(string id)
        {
            string fetchId = GetFetchId();
            return DataMapper.QueryForObject<TDto>(fetchId, id);
        }

        public virtual IList<TDto> FetchMany(string ids)
        {
            string fetchIds = GetFetchIds();
            return DataMapper.QueryForList<TDto>(fetchIds, ids);
        }

        public virtual PagerList<TDto> FetchPages(TQuery query)
        {
            string fetchQuery = GetFetchQueryId();
            int total = 0;
            int wiilShowedPage = query.Page;
            int pageSize = query.PageSize;
            IList<TDto> list =DataMapper.QueryForListWithPage<TDto>(fetchQuery, query, query.Order, wiilShowedPage, pageSize, ref total);
            query.TotalCount = total;
            PagerList<TDto> pagers = new PagerList<TDto>(query);
            pagers.AddRange(list);
            return pagers;
        }

        public virtual IList<TDto> FetchAll(TQuery query)
        {
            string fetchQuery = GetFetchQueryId();
            return DataMapper.QueryForList<TDto>(fetchQuery, query);
        }

        #endregion

        public virtual string GetAddId()
        {
            return string.Empty;
        }

        public virtual string GetDeleteId()
        {
            return string.Empty;
        }

        public virtual string GetUpdateId()
        {
            return string.Empty;
        }

        public virtual string GetFetchId()
        {
            return string.Empty;
        }

        public virtual string GetFetchIds()
        {
            return string.Empty;
        }

        public virtual string GetFetchQueryId()
        {
            return string.Empty;
        }
    }
}
