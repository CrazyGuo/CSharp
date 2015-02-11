using System.Web;
using Study.EasyUIFramework.Base;

namespace Study.EasyUIFramework.Layouts
{
    /// <summary>
    /// 区域面板选项
    /// </summary>
    public interface IRegionOption : IOption<IRegionOption> {
        /// <summary>
        /// 设置为顶部区域
        /// </summary>
        IRegionOption Top();
        /// <summary>
        /// 设置为底部区域
        /// </summary>
        IRegionOption Bottom();
        /// <summary>
        /// 设置为左侧区域
        /// </summary>
        IRegionOption Left();
        /// <summary>
        /// 设置为右侧区域
        /// </summary>
        IRegionOption Right();
        /// <summary>
        /// 设置为中间内容区域
        /// </summary>
        IRegionOption Center();
        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title">标题</param>
        IRegionOption Title( string title );
        /// <summary>
        /// 显示边框
        /// </summary>
        /// <param name="isShow">是否显示边框</param>
        IRegionOption Border( bool isShow = true );
        /// <summary>
        /// 显示分隔条
        /// </summary>
        /// <param name="isShow">是否显示分隔条</param>
        IRegionOption Split( bool isShow = true );
        /// <summary>
        /// 设置图标
        /// </summary>
        /// <param name="iconClass">图标class</param>
        IRegionOption IconClass( string iconClass );
        /// <summary>
        /// 设置Url
        /// </summary>
        /// <param name="url">Url</param>
        IRegionOption Href( string url );
        /// <summary>
        /// 允许折叠
        /// </summary>
        /// <param name="collapsible">是否允许折叠</param>
        IRegionOption Collapsible( bool collapsible = true );
        /// <summary>
        /// 设置最小宽度
        /// </summary>
        /// <param name="minWidth">最小宽度</param>
        IRegionOption MinWidth( int minWidth);
        /// <summary>
        /// 设置最小高度
        /// </summary>
        /// <param name="minHeight">最小高度</param>
        IRegionOption MinHeight( int minHeight );
        /// <summary>
        /// 设置最大宽度
        /// </summary>
        /// <param name="maxWidth">最大宽度</param>
        IRegionOption MaxWidth( int maxWidth );
        /// <summary>
        /// 设置最大高度
        /// </summary>
        /// <param name="maxHeight">最大高度</param>
        IRegionOption MaxHeight( int maxHeight );
        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        IRegionOption Width( int width );
        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        IRegionOption Height( int height );
    }
}
