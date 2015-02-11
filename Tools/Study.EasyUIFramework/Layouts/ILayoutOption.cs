using System.Web;
using Study.EasyUIFramework.Base;

namespace Study.EasyUIFramework.Layouts 
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
