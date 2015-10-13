using Study.Webs.EayUI.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Study.Webs.EayUI.Controllers
{
    //[CustomAuthorize(Roles = "Test")]
    public class HomeController : ManageControllerBase
    {
        public ActionResult GetTree()
        {
            List<MenuItem> menus = new List<MenuItem>();

            //模块一菜单
            List<MenuItem> fuctionOneChilden = new List<MenuItem>();
            fuctionOneChilden.Add(new MenuItem { id = 14, text = "SportRecord", attributes = new Attributes { url = "systems/SportRecord" } });
            fuctionOneChilden.Add(new MenuItem { id = 5, text = "SportKind", attributes = new Attributes { url = "systems/SportKind" } });
            MenuItem fuctionOne = new MenuItem { id = 1, text = "目录菜单",  children = fuctionOneChilden };

            menus.Add(fuctionOne);
            //模块二菜单
            menus.Add( new MenuItem { id = 1, text = "应用程序管理", attributes = new Attributes { url = "systems/application" }});
            menus.Add(new MenuItem { id = 2, text = "MoneyOut", attributes = new Attributes { url = "systems/moneyout" } });
            menus.Add(new MenuItem { id = 3, text = "D3Report", attributes = new Attributes { url = "D3Report/FirstReport" } });            
            menus.Add(new MenuItem { id = 5, text = "MySql", attributes = new Attributes { url = "OtherDB/User" } });

            return Json(menus,JsonRequestBehavior.AllowGet);
        }
    }

    public class MenuItem
    {
        /*这些字段都是小写标志 easyUI tree的要求*/
        public int id { get; set; }
        public string text { get; set; }
        public Attributes attributes { get; set; }
        public List<MenuItem> children{get;set;}
    }
    public class Attributes
    {
        public string url { get; set; }
    }


}