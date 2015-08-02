using System.Text;
using Study.Util;
using Study.EasyUIFramework.Base;

namespace Study.EasyUIFramework.Buttons 
{
    /// <summary>
    /// 链接按钮
    /// </summary>
    public abstract class LinkButton<T> : ComponentBase<T>, ILinkButton<T> where T : ILinkButton<T> 
    {
        /// <summary>
        /// 初始化链接按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        protected LinkButton( string text ) 
        {
            Text = text;
            AddClass( "easyui-linkbutton" );
        }

        /// <summary>
        /// 按钮文本
        /// </summary>
        protected string Text { get; set; }

        /// <summary>
        /// 禁用按钮
        /// </summary>
        /// <param name="disabled">true为禁用</param>
        public T Disable( bool disabled = true ) 
        {
            AddDataOption( "disabled", disabled );
            return This();
        }

        /// <summary>
        /// 启用平滑效果
        /// </summary>
        /// <param name="isPlain">true为启用平滑效果</param>
        public T Plain( bool isPlain = true ) 
        {
            AddDataOption( "plain", isPlain );
            return This();
        }

        /// <summary>
        /// 设置图标class
        /// </summary>
        /// <param name="iconClass">图标class</param>
        public T Icon( string iconClass ) 
        {
            AddDataOption( "iconCls", GetValue( iconClass ) );
            return This();
        }

        /// <summary>
        /// 设置图标对齐方式
        /// </summary>
        /// <param name="align">图标对齐方式</param>
        public T IconAlign( Align align ) 
        {
            AddDataOption( "iconAlign", GetValue( align.Description() ) );
            return This();
        }

        /// <summary>
        /// 设置为小按钮
        /// </summary>
        public T Small() 
        {
            AddDataOption( "size", GetValue( "small" ) );
            return This();
        }

        /// <summary>
        /// 设置为大按钮
        /// </summary>
        public T Large() 
        {
            AddDataOption( "size", GetValue( "large" ) );
            return This();
        }

        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">事件处理函数或Js代码</param>
        public T Click(string handler, string url = "", string callback = "") 
        {
            string split = ",";
            
            if (string.IsNullOrEmpty(url) == false && string.IsNullOrEmpty(callback) == false)
            {
                url = "'" + url + "'";
                handler = string.Format(handler, url, split, callback);
            }
            else if (string.IsNullOrEmpty(url) == false && string.IsNullOrEmpty(callback) == true)
            {
                url = "'" + url + "'";
                handler = string.Format(handler, url, string.Empty, string.Empty);
            }
            else 
            {
                handler = string.Format(handler, string.Empty, string.Empty, string.Empty);
            }
            AddAttribute( "onClick", handler );
            return This();
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() 
        {
            var result = new StringBuilder();
            result.AppendFormat( "<a href=\"javascript:void(0)\" {0}>", GetOptions() );
            result.AppendFormat( "{0}</a>", Text );
            return result.ToString();
        }
    }
}
