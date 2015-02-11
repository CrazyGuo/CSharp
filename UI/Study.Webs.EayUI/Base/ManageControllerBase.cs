using System.Web.Mvc;
using Study.EasyUIFramework;

namespace Study.Webs.EayUI.Base
{
    public class ManageControllerBase : EasyUiControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}