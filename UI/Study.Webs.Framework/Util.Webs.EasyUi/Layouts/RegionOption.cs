using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Layouts 
{
    /// <summary>
    /// 区域面板选项
    /// </summary>
    public class RegionOption : OptionBase<IRegionOption>, IRegionOption 
    {
        /// <summary>
        /// 设置为顶部区域
        /// </summary>
        public IRegionOption Top() 
        {
            AddDataOption( "region", "'north'" );
            return this;
        }

        /// <summary>
        /// 设置为底部区域
        /// </summary>
        public IRegionOption Bottom() 
        {
            AddDataOption( "region", "'south'" );
            return this;
        }

        /// <summary>
        /// 设置为左侧区域
        /// </summary>
        public IRegionOption Left() 
        {
            AddDataOption( "region", "'west'" );
            return this;
        }

        /// <summary>
        /// 设置为右侧区域
        /// </summary>
        public IRegionOption Right() 
        {
            AddDataOption( "region", "'east'" );
            return this;
        }

        /// <summary>
        /// 设置为中间内容区域
        /// </summary>
        public IRegionOption Center() 
        {
            AddDataOption( "region", "'center'" );
            return this;
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title">标题</param>
        public IRegionOption Title( string title ) 
        {
            AddDataOption( "title", GetValue( title ) );
            return this;
        }

        /// <summary>
        /// 显示边框
        /// </summary>
        /// <param name="isShow">是否显示边框，默认为显示</param>
        public IRegionOption Border( bool isShow = true ) 
        {
            AddDataOption( "border", isShow );
            return this;
        }

        /// <summary>
        /// 显示分隔条
        /// </summary>
        /// <param name="isShow">是否显示分隔条</param>
        public IRegionOption Split( bool isShow = true )
        {
            AddDataOption( "split", isShow );
            return this;
        }

        /// <summary>
        /// 设置图标
        /// </summary>
        /// <param name="iconClass">图标class</param>
        public IRegionOption IconClass( string iconClass ) 
        {
            AddDataOption( "iconCls", GetValue( iconClass ) );
            return this;
        }

        /// <summary>
        /// 设置Url
        /// </summary>
        /// <param name="url">Url</param>
        public IRegionOption Href( string url ) 
        {
            AddDataOption( "href", GetValue( url ) );
            return this;
        }

        /// <summary>
        /// 允许折叠
        /// </summary>
        /// <param name="collapsible">是否允许折叠</param>
        public IRegionOption Collapsible( bool collapsible = true ) 
        {
            AddDataOption( "collapsible", collapsible );
            return this;
        }

        /// <summary>
        /// 设置最小宽度
        /// </summary>
        /// <param name="minWidth">最小宽度</param>
        public IRegionOption MinWidth( int minWidth ) 
        {
            AddDataOption( "minWidth", minWidth.ToString() );
            return this;
        }

        /// <summary>
        /// 设置最小高度
        /// </summary>
        /// <param name="minHeight">最小高度</param>
        public IRegionOption MinHeight( int minHeight ) 
        {
            AddDataOption( "minHeight", minHeight.ToString() );
            return this;
        }

        /// <summary>
        /// 设置最大宽度
        /// </summary>
        /// <param name="maxWidth">最大宽度</param>
        public IRegionOption MaxWidth( int maxWidth ) 
        {
            AddDataOption( "maxWidth", maxWidth.ToString() );
            return this;
        }

        /// <summary>
        /// 设置最大高度
        /// </summary>
        /// <param name="maxHeight">最大高度</param>
        public IRegionOption MaxHeight( int maxHeight ) 
        {
            AddDataOption( "maxHeight", maxHeight.ToString() );
            return this;
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        public IRegionOption Width( int width )
        {
            AddStyle( "width", width + "px" );
            return this;
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        public IRegionOption Height( int height )
        {
            AddStyle( "height", height + "px" );
            return this;
        }
    }
}
