using System.Text;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Menus 
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class MenuItem : ComponentBase<IMenuItem>,IMenuItem 
    {
        /// <summary>
        /// 文本
        /// </summary>
        private string _text;
        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text">文本</param>
        public IMenuItem Text( string text ) 
        {
            _text = text;
            return This();
        }

        /// <summary>
        /// 设置图标class
        /// </summary>
        /// <param name="iconClass">图标class</param>
        public IMenuItem Icon( string iconClass )
        {
            AddDataOption( "iconCls", GetValue( iconClass ) );
            return This();
        }

        /// <summary>
        /// 设置Url
        /// </summary>
        /// <param name="url">Url</param>
        public IMenuItem Href( string url ) 
        {
            AddDataOption( "href", GetValue( url ) );
            return This();
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public IMenuItem Disable() 
        {
            AddDataOption( "disabled", true );
            return This();
        }

        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">单击事件处理函数</param>
        public IMenuItem Click( string handler ) 
        {
            AddAttribute( "onclick", handler );
            return This();
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() 
        {
            var result = new StringBuilder();
            result.AppendFormat( "<div {0}>{1}</div>", GetOptions(),_text );
            return result.ToString();
        }
    }
}
