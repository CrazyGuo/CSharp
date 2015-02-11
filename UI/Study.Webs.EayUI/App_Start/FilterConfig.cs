using System.Web.Mvc;

namespace Study.Webs.EayUI
{
    public class FilterConfig 
    {
        public static void RegisterGlobalFilters( GlobalFilterCollection filters ) 
        {
            filters.Add( new HandleErrorAttribute() );
        }
    }
}