using Study.Webs.EayUI.Base;
using Study.BusinessService;
using Study.Entity;

namespace Study.Webs.EayUI.Areas.Systems.Controllers
{
    public class ApplicationController : CrudControllerBase<ApplicationDto, ApplicationQuery>
    {
        public IApplicationService ApplicationService { get; set; }

        public ApplicationController(IApplicationService applicationService)
            : base(applicationService)
        {
            ApplicationService = applicationService;
        }       
    }
}