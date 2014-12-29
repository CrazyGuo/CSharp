using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Study.Business;
using Study.MongoDB.Core;
using Resources;
using Study.Entity.Common;

namespace Study.WebApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>树</returns>
        public JsonResult GetTree(string id)
        {
            MenuService sevice = new MenuService();
            IList<Menu> menus = sevice.GetMenus(id);
            var jsonData = from menu in menus
                           select new
                           {
                               id = menu.Id,
                               text = menu.Text,
                               value = menu.Value,
                               icon = menu.Icon,
                               showcheck = menu.Showcheck,
                               isexpand = menu.Isexpand,
                               hasChildren = menu.HasChildren,
                               complete = menu.Complete
                           };
            
            return Json(jsonData.ToArray(), JsonRequestBehavior.AllowGet);

        }
    }
}
