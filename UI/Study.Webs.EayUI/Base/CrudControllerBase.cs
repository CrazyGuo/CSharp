using System.Web.Mvc;
using Study.ApplicationServices;
using Study.Domains.Framework.Repositories;

namespace Study.Webs.EayUI.Base
{
    public abstract class CrudControllerBase<TDto, TQuery> : ManageControllerBase
    where TQuery : IPager
    where TDto : new()
    {
        protected IServiceStudyBase<TDto, TQuery> Service { get; private set; }

        protected CrudControllerBase(IServiceStudyBase<TDto, TQuery> service)
        {
            Service = service;
        }

        [HttpPost]
        public JsonResult QueryAll()
        {
            LogOuts.Info("Query");
            var list = Service.FetchAll();
            return Json(list);
        }

        [HttpPost]
        public virtual ActionResult Query(TQuery query)
        {
            LogOuts.Info("Query");
            SetPage(query);
            var list = Service.FetchPages(query);
            return ToDataGridResult(list, list.TotalCount);
        }

        protected void SetPage(IPager query)
        {
            query.Page = GetPageIndex();
            query.PageSize = GetPageSize();
            query.Order = GetOrder();
        }

        [HttpGet]
        public virtual PartialViewResult Add()
        {
            LogOuts.Info("Add");
            return PartialView("Parts/Form", Service.Create());
        }

        [HttpPost]
        public virtual ActionResult Save(TDto dto)
        {
            LogOuts.Info("Save");
            Service.Add(dto);
            return Ok("SaveSuccess");
        }

        [HttpGet]
        public virtual PartialViewResult Edit(string id)
        {
            LogOuts.Info("Edit");
            return PartialView("Parts/UpdateForm", Service.FetchOne(id));
        }

        [HttpPost]
        public virtual ActionResult Update(TDto dto)
        {
            LogOuts.Info("Update");
            Service.Update(dto);
            return Ok("UpdateSuccess");
        }

        [HttpPost]
        public virtual ActionResult Delete(string ids)
        {
            LogOuts.Info("Delete");
            if(ids.Contains(","))
            {
                Service.BatchDelete(ids);
            }
            else
            {
                Service.Delete(ids);
            }
            
            return Ok("Sucess");
        }

        public virtual PartialViewResult Look(string id)
        {
            LogOuts.Info("Look");
            return PartialView("Parts/Detail", Service.FetchOne(id));
        }
    }
}