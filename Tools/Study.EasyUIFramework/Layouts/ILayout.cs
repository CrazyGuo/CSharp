namespace Study.EasyUIFramework.Layouts 
{
    /// <summary>
    /// 布局
    /// </summary>
    public interface ILayout 
    {
        /// <summary>
        /// 布局选项
        /// </summary>
        /// <param name="fit">自适应布局</param>
        ILayoutOption LayoutOptions( bool fit = false );
        /// <summary>
        /// 区域面板选项
        /// </summary>
        IRegionOption RegionOptions();
    }
}
