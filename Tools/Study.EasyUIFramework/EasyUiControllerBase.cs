using System.Collections.Generic;
using System.Web.Mvc;
using Study.Util;
using Study.EasyUIFramework.Forms.Comboxs;
using Study.EasyUIFramework.Forms.Results;
using Study.EasyUIFramework.Grids;

namespace Study.EasyUIFramework 
{
    /// <summary>
    /// EasyUi基控制器
    /// </summary>
    public abstract class EasyUiControllerBase : Study.UtilWebs.ControllerBase 
    {
        /// <summary>
        /// 转换为DataGrid输出结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="data">实体列表</param>
        /// <param name="totalCount">总行数</param>
        protected ActionResult ToDataGridResult<T>( IList<T> data, int totalCount = 0 ) 
        {
            return new DataGridResult( data, GetTotalCount( data, totalCount ) ).GetResult();
        }

        /// <summary>
        /// 获取总行数
        /// </summary>
        private int GetTotalCount<T>( IList<T> data, int totalCount ) 
        {
            if ( totalCount == 0 )
                return data.Count;
            return totalCount;
        }

        /// <summary>
        /// 转换为Combox输出结果
        /// </summary>
        /// <param name="items">组合框项集合</param>
        protected ActionResult ToComboxResult( IEnumerable<ComboxItem> items ) 
        {
            return new ContentResult() { Content = Combox.ToJson( items ) };
        }

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        protected ActionResult Ok( string message ) 
        {
            return new EasyUiResult( StateCode.Ok, message ).GetResult();
        }

        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        protected ActionResult Fail( string message ) 
        {
            return new EasyUiResult( StateCode.Fail, message ).GetResult();
        }

        /// <summary>
        /// 远程验证成功
        /// </summary>
        protected ActionResult RemoteOk() 
        {
            return new ContentResult { Content = "true" };
        }

        /// <summary>
        /// 远程验证失败
        /// </summary>
        /// <param name="message">消息</param>
        protected ActionResult RemoteFail( string message ) 
        {
            return new ContentResult { Content = message };
        }

        /// <summary>
        /// 获取分页的页索引
        /// </summary>
        protected int GetPageIndex() 
        {
            return Request["page"].ToInt();
        }

        /// <summary>
        /// 获取分页大小
        /// </summary>
        protected int GetPageSize() 
        {
            return Request["rows"].ToInt();
        }

        /// <summary>
        /// 获取排序
        /// </summary>
        protected string GetOrder() 
        {
            return string.Format( "{0} {1}", Request["sort"].ToStr(), Request["order"].ToStr() );
        }
    }
}
