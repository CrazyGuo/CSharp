using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Study.Entity;
using Study.Business;
using Study.MongoDB.Core;

namespace Study.WebApp.Controllers
{
    public class StaffController : Controller
    {
        //
        // GET: /Staff/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStaffs(int page, int rows, string birthBegin, string birthEnd)
        {
            MongoDbRemoteRepository<Staff> repository = new MongoDbRemoteRepository<Staff>();
            repository.Connect();
            List<Staff> list = repository.List(i => i.GRADUATION_SCHOOL=="四川大学");
            repository.CloseConnect();
            var json2 = new
            {
                total = list.Count,
                rows = list.ToArray(),
            };

            return Json(json2, JsonRequestBehavior.AllowGet);
        }

    }
}
