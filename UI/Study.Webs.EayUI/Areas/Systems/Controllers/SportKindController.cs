using System;
using System.Collections.Generic;
using System.Linq;
using Study.Webs.EayUI.Base;
using Study.Entity;
using Study.BusinessService;

namespace Study.Webs.EayUI.Areas.Systems.Controllers
{
    public class SportKindController : CrudControllerBase<SportKindDto, SportKindQuery>
    {
        ISportKindService SportKindService { get; set; }
        public SportKindController(ISportKindService service)
            : base(service)
        {
            this.SportKindService = service;
        }
    }
}