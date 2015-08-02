using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Study.DI.Autofac;
using Log;
using System;
using System.Web;
using Study.Webs.EayUI.Controllers;

namespace Study.Webs.EayUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~") + @"\log4net.config"));
            LogOuts.Info("Application_Start");

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            Container.RegisterMvc(typeof(MvcApplication).Assembly,new SecurityConfig());
            LogOuts.Info("Application_End");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            /*捕捉异常的最后一道防线*/
            var ex = Server.GetLastError();
            LogOuts.Error(ex.ToString()); //记录日志信息  
            var httpStatusCode = (ex is HttpException) ? (ex as HttpException).GetHttpCode() : 500; //这里仅仅区分两种错误  
            var httpContext = ((MvcApplication)sender).Context;
            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = httpStatusCode;
            var shouldHandleException = true;
            HandleErrorInfo errorModel;

            var routeData = new RouteData();
            routeData.Values["controller"] = "AppError";

            switch (httpStatusCode)
            {
                case 404:
                    routeData.Values["action"] = "PageNotFound";
                    errorModel = new HandleErrorInfo(new Exception(string.Format("No page Found", httpContext.Request.UrlReferrer), ex), "AppError", "PageNotFound");
                    break;

                default:
                    routeData.Values["action"] = "Error";
                    Exception exceptionToReplace = null; //这里使用了EntLib的异常处理模块的一些功能  
                    shouldHandleException = true;
                    errorModel = new HandleErrorInfo(exceptionToReplace, "AppError", "Error");
                    break;
            }

            if (shouldHandleException)
            {
                var controller = new AppErrorController();
                controller.ViewData.Model = errorModel; //通过代码路由到指定的路径  
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }  
        }
    }
}
