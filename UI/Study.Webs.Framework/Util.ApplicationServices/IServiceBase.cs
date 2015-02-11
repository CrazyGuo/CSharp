using System.Collections.Generic;
using Util.Domains.Repositories;

namespace Util.ApplicationServices 
{
    /// <summary>
    /// 应用服务
    /// </summary>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQuery">查询实体类型</typeparam>
    public interface IServiceBase<TDto, in TQuery> where TDto : new()
    {
        /// <summary>
        /// 创建实体
        /// </summary>
        TDto Create();
        /// <summary>
        /// 通过编号获取
        /// </summary>
        /// <param name="id">实体编号</param>
        TDto Get( object id );
        /// <summary>
        /// 获取全部列表
        /// </summary>
        List<TDto> GetAll();
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query">查询实体</param>
        PagerList<TDto> Query( TQuery query );
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        void Save( TDto dto );
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">Id集合字符串，多个Id用逗号分隔</param>
        void Delete( string ids );
    }
}
