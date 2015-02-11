using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Study.Webs.EayUI.Base;
using Study.BusinessService;
using Study.Entity;

namespace Study.Webs.EayUI.Areas.Systems.Controllers
{
    public class ApplicationController : CrudControllerBase<ApplicationDto, ApplicationQuery>
    {
        /// <summary>
        /// 初始化应用程序控制器
        /// </summary>
        /// <param name="applicationService">应用程序服务</param>
        public ApplicationController(IApplicationService applicationService)
            : base(applicationService)
        {
            ApplicationService = applicationService;
        }

        /// <summary>
        /// 应用程序服务
        /// </summary>
        public IApplicationService ApplicationService { get; set; }
    }
}