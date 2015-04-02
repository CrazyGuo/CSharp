using System.Web.Mvc;

namespace Study.Webs.EayUI.Areas.D3Report
{
    public class D3ReportAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "D3Report";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "D3Report_default",
                "D3Report/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}