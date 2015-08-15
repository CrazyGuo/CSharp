using System.Web.Mvc;

namespace Study.Webs.EayUI.Base
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // grant all actions to "ManageAll" permission
            if (Roles.Trim().Length > 0)
            {
                Roles = Roles.Insert(0, "ManageAll, ");
            }
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new PartialViewResult { ViewName = "_AccessDenied" };
            }
            else
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
                else
                {
                    filterContext.Result = new ViewResult { ViewName = "AccessDenied" };
                }
            }
        }
    }
}