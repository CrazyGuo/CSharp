using System.Collections.Generic;
using Study.EasyUIFramework.Forms.TextBoxs;

namespace Study.EasyUIFramework.Forms.Comboxs 
{
    /// <summary>
    /// 组合框
    /// </summary>
    /// <typeparam name="T">组合框控件</typeparam>
    public interface ICombox<out T> : ICombo<T> where T : ICombox<T>
    {
        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        T Add( string text, object value = null );
        /// <summary>
        /// 添加项集合
        /// </summary>
        /// <param name="items">项集合</param>
        T Add( IEnumerable<ComboxItem> items );
        /// <summary>
        /// 添加默认项
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        T AddDefault( string text, object value = null );
        /// <summary>
        /// 选中项
        /// </summary>
        /// <param name="value">值</param>
        T Select( object value );
        /// <summary>
        /// 绑定bool值
        /// </summary>
        T Bool();
        /// <summary>
        /// 绑定bool值
        /// </summary>
        /// <param name="text">默认项的文本</param>
        /// <param name="value">默认项的值</param>
        T Bool( string text, object value = null );
        /// <summary>
        /// 绑定枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        T Enum<TEnum>();
        /// <summary>
        /// 绑定枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="text">默认项的文本</param>
        /// <param name="value">默认项的值</param>
        T Enum<TEnum>( string text, object value = null );
        /// <summary>
        /// 从远程加载数据
        /// </summary>
        /// <param name="url">远程Url，返回Json数据</param>
        /// <param name="valueField">值字段名，默认为"value"</param>
        /// <param name="textField">文本字段名，默认为"text"</param>
        /// <param name="groupField">组字段名，默认为"group"</param>
        T Load( string url, string valueField = "value", string textField = "text", string groupField = "group" );
    }
}
