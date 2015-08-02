using System.Collections.Generic;
using System.Web.Mvc;
using Study.Util;
using Study.EasyUIFramework.Forms.Comboxs;
using Study.EasyUIFramework.Forms.Results;
using Study.EasyUIFramework.Grids;

namespace Study.EasyUIFramework 
{
    public abstract class EasyUiControllerBase : Study.UtilWebs.ControllerBase 
    {

        #region 查询页面的数据表格返回数据

        protected ActionResult ToDataGridResult<T>(IList<T> data, int totalCount = 0)
        {
            return new DataGridResult(data, GetTotalCount(data, totalCount)).GetResult();
        }

        protected ActionResult ToComboxResult(IEnumerable<ComboxItem> items)
        {
            return new ContentResult() { Content = Combox.ToJson(items) };
        }

        #endregion       

        #region 界面操作结果提示

        protected ActionResult Ok(string message)
        {
            return new EasyUiResult(StateCode.Ok, message).GetResult();
        }

        protected ActionResult Fail(string message)
        {
            return new EasyUiResult(StateCode.Fail, message).GetResult();
        }

        protected ActionResult RemoteOk()
        {
            return new ContentResult { Content = "true" };
        }

        protected ActionResult RemoteFail(string message)
        {
            return new ContentResult { Content = message };
        }

        #endregion

        #region 分页与排序相关参数的获取

        protected int GetPageIndex()
        {
            return Request["page"].ToInt();
        }

        protected int GetPageSize()
        {
            return Request["rows"].ToInt();
        }

        protected string GetOrder()
        {
            return string.Format("{0} {1}", Request["sort"].ToStr(), Request["order"].ToStr());
        }

        private int GetTotalCount<T>(IList<T> data, int totalCount)
        {
            if (totalCount == 0)
                return data.Count;
            return totalCount;
        }

        #endregion
        
    }
}
