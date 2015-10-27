using System.Web.Mvc;
using Study.Domains.Framework.Repositories;
using Study.BusinessService.Application;
using Log;
using System;
using System.IO;
using System.Web;
using System.Threading;

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
            if(!ModelState.IsValid)
            {
                return View(dto);
            }
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

        #region 上传/下载

        [HttpGet]
        public FileResult Download(TQuery query)
        {
            LogOuts.Info("Download");
            var list = Service.FetchAll(query);
            var stream = Service.ExportExcel(list);
            stream.Seek(0, SeekOrigin.Begin);
            string name = DateTime.Now.Millisecond.ToString();
            return File(stream.ToArray(), "application/vnd.ms-excel", name + ".xls"); ;
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase berkas, string guid)
        {
            bool result = false;
            string filePath = Server.MapPath("~/Temporary/") + berkas.FileName;

            int fileLength = berkas.ContentLength;
            HttpContext.Cache[guid + "_total"] = fileLength;
            HttpContext.Cache[guid + "_current"] = 0;
            byte[] fileContent = new byte[fileLength];
            int bufferLength = 5 * 1024;
            byte[] buffer = new byte[bufferLength];
            int bytesRead = 0;

            FileStream outputFileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            using (Stream inputFileStream = berkas.InputStream)
            {
                while ((bytesRead = inputFileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputFileStream.Write(buffer, 0, bytesRead);
                    outputFileStream.Flush();
                    int current = 0;
                    int.TryParse(HttpContext.Cache[guid + "_current"].ToString(), out current);
                    HttpContext.Cache[guid + "_current"] = current + bytesRead;
                    Thread.Sleep(100);
                }

                inputFileStream.Close();
                inputFileStream.Dispose();
            }

            outputFileStream.Close();
            outputFileStream.Dispose();
            result = true;
            
            return Json(result);
        }

        [HttpPost]
        public ActionResult TrackProgress(string guid)
        {
            var current = HttpContext.Cache[guid + "_current"];
            
            var total = HttpContext.Cache[guid + "_total"];
            int paramCurrentFileSize = 0;
            int paramTotalFileSize = 1;
            
            if (current != null)
            {
                int.TryParse(current.ToString(), out paramCurrentFileSize);
            }
            else
            {
                paramCurrentFileSize = 100;
            }
            if (total != null)
            {
                int.TryParse(total.ToString(), out paramTotalFileSize);
            }
            else
            {
                paramTotalFileSize = 100;
            }
            int uploadProgress = paramCurrentFileSize * 100 / paramTotalFileSize;
            return Json(uploadProgress);
        }

        #endregion                 
    }
}