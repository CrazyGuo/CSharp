using Study.EasyUIFramework.Base;

namespace Study.EasyUIFramework.Buttons 
{
    /// <summary>
    /// 链接按钮
    /// </summary>
    /// <typeparam name="T">链接按钮类型</typeparam>
    public interface ILinkButton<out T> : IComponent<T> where T : ILinkButton<T> 
    {
        /// <summary>
        /// 禁用按钮
        /// </summary>
        /// <param name="disabled">true为禁用</param>
        T Disable( bool disabled = true );
        /// <summary>
        /// 启用平滑效果
        /// </summary>
        /// <param name="isPlain">true为启用平滑效果</param>
        T Plain( bool isPlain = true );
        /// <summary>
        /// 设置图标class
        /// </summary>
        /// <param name="iconClass">图标class</param>
        T Icon( string iconClass );
        /// <summary>
        /// 设置图标对齐方式
        /// </summary>
        /// <param name="align">图标对齐方式</param>
        T IconAlign( Align align );
        /// <summary>
        /// 设置为小按钮
        /// </summary>
        T Small();
        /// <summary>
        /// 设置为大按钮
        /// </summary>
        T Large();
        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">事件处理函数或Js代码,范例: fun() 或 alert('1');</param>
        T Click( string handler, string url = "", string callback = "" );
    }
}
