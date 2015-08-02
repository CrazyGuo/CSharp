using System;
using System.Collections.Generic;
using System.Linq;
using Study.Webs.EayUI.Base;
using Study.Entity;
using Study.BusinessService;

namespace Study.Webs.EayUI.Areas.Systems.Controllers
{
    public class SportRecordController :CrudControllerBase <SportRecordDto, SportRecordQuery>
    {
        ISportRecordService SportRecordService { get; set;}
        public SportRecordController(ISportRecordService service):base(service)
        {
            this.SportRecordService = service;
        }
    }
}