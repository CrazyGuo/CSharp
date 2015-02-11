using System.Web.Optimization;

namespace Study.Webs.EayUI
{
    /// <summary>
    /// 资源打包配置
    /// </summary>
    public class BundleConfig 
    {
        /// <summary>
        /// 注册资源打包
        /// </summary>
        public static void RegisterBundles( BundleCollection bundles ) 
        {
            //启用打包压缩
            //BundleTable.EnableOptimizations = true;
            //EasyUi样式
            bundles.Add( new StyleBundle( "~/Scripts/EasyUi/themes/default/css" ).Include(
                "~/Scripts/EasyUi/themes/default/easyui.css" ) );
            //css样式
            bundles.Add( new StyleBundle( "~/Css/css" ).Include(
                "~/Css/icon.css",
                "~/Css/common.css" ) );
            //js扩展
            bundles.Add( new ScriptBundle( "~/Scripts/Utils/js" ).Include(
                "~/Scripts/Utils/util.js",
                "~/Scripts/Utils/jquery.util.js",
                "~/Scripts/Utils/jquery.util.easyui.extension.js",
                "~/Scripts/Utils/jquery.util.easyui.js",
                "~/Scripts/Utils/jquery.util.easyui.crud.js" ) );
        }
    }
}