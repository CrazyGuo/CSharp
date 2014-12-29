using Study.Business;
using Study.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study.WebApp.Controllers
{
    public class StaffManageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStaffs(int page, int rows, string birthBegin, string birthEnd)
        {
            StaffService staffService = new StaffService();
            if (!string.IsNullOrEmpty(birthBegin))
            {
                birthBegin = Convert.ToDateTime(birthBegin).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(birthEnd))
            {
                birthEnd = Convert.ToDateTime(birthEnd).ToString("yyyy-MM-dd");
            }

            int totalCount = 0;
            IList<Staff> staffs = staffService.GetStaffs(page,rows,birthBegin,birthEnd, ref totalCount);
            var json = new
            {
                total = totalCount,
                rows = staffs.ToArray(),
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}
