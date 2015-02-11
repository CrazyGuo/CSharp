using System.Collections;
using System.Web.Mvc;
using Study.Util;
using Study.UtilWebs;

namespace Study.EasyUIFramework.Grids 
{
    /// <summary>
    /// DataGrid数据输出结果
    /// </summary>
    public class DataGridResult 
    {
        /// <summary>
        /// 初始化DataGrid数据输出结果
        /// </summary>
        /// <param name="data">数据列表</param>
        /// <param name="totalCount">总行数</param>
        public DataGridResult( IEnumerable data, int totalCount ) 
        {
            _data = data;
            _totalCount = totalCount;
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        private readonly IEnumerable _data;
        /// <summary>
        /// 总行数
        /// </summary>
        private readonly int _totalCount;

        /// <summary>
        /// 获取输出结果
        /// </summary>
        public ActionResult GetResult() 
        {
            return new ContentResult{ Content = Json.ToJson( new { total = _totalCount, rows = _data } ) };
        }
    }
}
