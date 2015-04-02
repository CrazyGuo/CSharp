using Study.Webs.EayUI.Base;
using Study.Entity;
using Study.BusinessService;

namespace Study.Webs.EayUI.Areas.Systems.Controllers
{
    public class MoneyOutController : CrudControllerBase<MoneyOutDto, MoneyOutQuery>
    {
        IMoneyOutService MoneyOutService { get; set; }
        public MoneyOutController(IMoneyOutService service)
            : base(service)
        {
            this.MoneyOutService = service;
        }
    }
}