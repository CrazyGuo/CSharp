using System.Collections.Generic;
using System.Text;
using Study.EasyUIFramework.Base;
using Study.EasyUIFramework.EasyuiJsNames;

namespace Study.EasyUIFramework.Menus 
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : ComponentBase<IMenu>,IMenu 
    {
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <param name="id">Id</param>
        public Menu(string id = MenuNameAndEvent.DataGridMenu) 
        {
            _items = new List<IMenuItem>();
            Id( id ).AddClass( "easyui-menu" );
        }

        /// <summary>
        /// 菜单项
        /// </summary>
        private readonly List<IMenuItem> _items; 

        /// <summary>
        /// 设置zIndex属性
        /// </summary>
        /// <param name="value">值</param>
        public IMenu ZIndex( int value )
        {
            AddDataOption( "zIndex", value.ToString() );
            return This();
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        /// <param name="left">左部偏移量</param>
        /// <param name="top">顶部偏移量</param>
        public IMenu Position( int left = 0,int top = 0 ) 
        {
            AddDataOption( "left", left.ToString() ).AddDataOption( "top", top.ToString() );
            return This();
        }

        /// <summary>
        /// 设置最小宽度
        /// </summary>
        /// <param name="width">宽度</param>
        public IMenu MinWidth( int width ) 
        {
            AddDataOption( "minWidth", width.ToString() );
            return This();
        }

        /// <summary>
        /// 设置显示持续时间
        /// </summary>
        /// <param name="time">显示持续时间，当鼠标离开菜单时，经过该时间自动隐藏，单位：毫秒</param>
        public IMenu Duration( int time ) 
        {
            AddDataOption( "duration", time.ToString() );
            return This();
        }

        /// <summary>
        /// 设置当鼠标离开菜单时是否隐藏菜单
        /// </summary>
        /// <param name="isHide">是否隐藏</param>
        public IMenu HideOnUnHover( bool isHide ) 
        {
            AddDataOption( "hideOnUnhover", isHide );
            return This();
        }

        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">单击事件处理函数</param>
        public IMenu Click(string handler = MenuNameAndEvent.ClickGridMenu) 
        {
            AddDataOption( "onClick", handler );
            return This();
        }

        /// <summary>
        /// 设置菜单项
        /// </summary>
        /// <param name="items">菜单项</param>
        public IMenu Items( params IMenuItem[] items )
        {
            if ( items == null )
                return This();
            _items.AddRange( items );
            return This();
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        protected override string GetResult()
        {
            var result = new StringBuilder();
            result.AppendFormat( "<div {0}>", GetOptions() );
            foreach( var item in _items )
                result.Append( item );
            result.Append( "</div>" );
            return result.ToString();
        }
    }
}
