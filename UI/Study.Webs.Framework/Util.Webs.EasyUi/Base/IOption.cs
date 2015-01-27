using System.Web;

namespace Util.Webs.EasyUi.Base 
{
    /// <summary>
    /// 选项
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    public interface IOption<out T> : IHtmlString where T : IOption<T> 
    {
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        T AddAttribute( string name, string value );
        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="name">样式名称</param>
        /// <param name="value">样式值</param>
        T AddStyle( string name, string value );
        /// <summary>
        /// 添加class属性
        /// </summary>
        /// <param name="class">class属性</param>
        T AddClass( string @class );
        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        T AddDataOption( string name, string value );
        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        T AddDataOption( string name, bool value );
    }
}
