using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study.Webs.EayUI.Controllers
{
    public class AppErrorController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }  
    }
}