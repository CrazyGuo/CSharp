using System.Web.Mvc;

namespace Study.Webs.EayUI.Areas.OtherDB
{
    public class OtherDBAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OtherDB";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OtherDB_default",
                "OtherDB/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Study.Webs.EayUI.Areas.OtherDB.Controllers" }
            );
        }
    }
}