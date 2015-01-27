namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 布局
    /// </summary>
    public class Layout : ILayout {
        /// <summary>
        /// 布局选项
        /// </summary>
        /// <param name="fit">自适应布局</param>
        public ILayoutOption LayoutOptions( bool fit = false ) {
            return EasyUiFactory.CreateLayoutOption( fit );
        }

        /// <summary>
        /// 区域面板选项
        /// </summary>
        public IRegionOption RegionOptions() {
            return EasyUiFactory.CreateRegionOption();
        }
    }
}
