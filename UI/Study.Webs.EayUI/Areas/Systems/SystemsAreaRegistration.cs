using System.Web.Mvc;

namespace Study.Webs.EayUI.Areas.Systems
{
    public class SystemsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Systems";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Systems_default",
                "Systems/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Study.Webs.EayUI.Areas.Systems.Controllers" }
            );
        }
    }
}