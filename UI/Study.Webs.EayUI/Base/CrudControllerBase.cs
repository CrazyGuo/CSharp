using System.Web.Mvc;
using Study.Domains.Framework.Repositories;
using Study.BusinessService.Application;
using Log;
using System;

namespace Study.Webs.EayUI.Base
{
    public abstract class CrudControllerBase<TDto, TQuery> : ManageControllerBase
    where TQuery : IPager
    where TDto : new()
    {
        #region 设置Service

        protected IServiceStudyBase<TDto, TQuery> Service { get; private set; }

        protected CrudControllerBase(IServiceStudyBase<TDto, TQuery> service)
        {
            Service = service;
        }

        #endregion
        
        #region 新增 修改 查看明细  Form页面

        [HttpGet]
        public virtual PartialViewResult Add()
        {
            LogOuts.Info("Add");
            return PartialView(CommonName.AddForm, Service.Create());
        }

        [HttpGet]
        public virtual PartialViewResult Edit(string id)
        {
            LogOuts.Info("Edit");
            return PartialView(CommonName.EditForm, Service.FetchOne(id));
        }

        public virtual PartialViewResult Look(string id)
        {
            LogOuts.Info("Look");
            return PartialView(CommonName.LookForm, Service.FetchOne(id));
        }

        #endregion

        #region 新增 修改 删除方法

        [HttpPost]
        public virtual ActionResult Save(TDto dto)
        {
            LogOuts.Info("Save");
            Service.Add(dto);
            return Ok("SaveSuccess");
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
            if (ids.Contains(","))
            {
                Service.BatchDelete(ids);
            }
            else
            {
                Service.Delete(ids);
            }
            return Ok("Sucess");
        }

        #endregion

        #region 不分页查询  分页查询方法

        [HttpPost]
        public JsonResult QueryAll()
        {
            LogOuts.Info("Query");
            var list = Service.FetchAll();
            return Json(list);
        }

        [HttpPost]
        public JsonResult QueryNoPages(TQuery query)
        {
            LogOuts.Info("Query");
            var list = Service.FetchAll(query);
            return Json(list);
        }

        [HttpPost]
        public virtual ActionResult Query(TQuery query)
        {
            LogOuts.Info("Query");
            SetPage(query);
            PagerList<TDto> list = null;
            try
            {
                list = Service.FetchPages(query);
            }
            catch(Exception e)
            {
                LogOuts.Info("FetchPages Error:"+e.Message);
            }
            
            return ToDataGridResult(list, list.TotalCount);
        }

        protected void SetPage(IPager query)
        {
            query.Page = GetPageIndex();
            query.PageSize = GetPageSize();
            query.Order = GetOrder();
        }    

        #endregion
                                   
    }
}