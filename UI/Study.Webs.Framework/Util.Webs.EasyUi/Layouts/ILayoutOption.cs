using System.Web;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Layouts 
{
    /// <summary>
    /// 布局选项
    /// </summary>
    public interface ILayoutOption : IOption<ILayoutOption> 
    {
        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        ILayoutOption Width( int width );
        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        ILayoutOption Height( int height );
    }
}
