using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Study.Webs.EayUI.Base
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            string[] users = Users.Split(',');
            string[] roles = Roles.Split(',');
            if (!httpContext.User.Identity.IsAuthenticated)//未登录的话 跳转到登录界面
                return false;
            if (roles.Length != 0)//检查当前页面的角色用户是否拥有
            {
                //List<Role> rightRoles = 
                //foreach (var role in roles)
                //{
                //    if (rightRoles.Where(x => x.Code == role).Count() > 0)
                //    {
                //        result = true;
                //        break;
                //    }
                //}
                result = true;
            }
            else
            {
                result = true;
            }
            if (!result)
            {
                httpContext.Response.StatusCode = 403;
            }
            return result; 
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }   
            //// grant all actions to "ManageAll" permission
            //if (Roles.Trim().Length > 0)
            //{
            //    Roles = Roles.Insert(0, "ManageAll, ");
            //}
            //base.OnAuthorization(filterContext);
        }

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    if (filterContext.HttpContext.Request.IsAjaxRequest())
        //    {
        //        filterContext.Result = new PartialViewResult { ViewName = "_AccessDenied" };
        //    }
        //    else
        //    {
        //        if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
        //        {
        //            base.HandleUnauthorizedRequest(filterContext);
        //        }
        //        else
        //        {
        //            filterContext.Result = new ViewResult { ViewName = "AccessDenied" };
        //        }
        //    }
        //}
    }
}