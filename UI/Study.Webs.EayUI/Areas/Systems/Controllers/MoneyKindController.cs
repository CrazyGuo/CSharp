using Study.Webs.EayUI.Base;
using Study.Entity;
using Study.BusinessService;

namespace Study.Webs.EayUI.Areas.Systems.Controllers
{
    public class MoneyKindController :CrudControllerBase <MoneyKindDto, MoneyKindQuery>
    {
        IMoneyKindService MoneyKindService { get; set;}
        public MoneyKindController(IMoneyKindService service)
            :base(service)
        {
            this.MoneyKindService = service;
        }
    }
}