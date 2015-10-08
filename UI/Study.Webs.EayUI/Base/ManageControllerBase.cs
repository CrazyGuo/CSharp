using System.Web.Mvc;
using Study.EasyUIFramework;

namespace Study.Webs.EayUI.Base
{
    public class ManageControllerBase : EasyUiControllerBase
    {
        #region 控制器默认的路由页面

        public ActionResult Index(string path = "trd")
        {
            ViewData["Path"] = path;
            return View();
        }

        #endregion
        
    }
}