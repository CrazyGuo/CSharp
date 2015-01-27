using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Grids 
{
    /// <summary>
    /// 表格列
    /// </summary>
    public interface IDataGridColumn : IComponent<IDataGridColumn> 
    {
        /// <summary>
        /// 设置字段名
        /// </summary>
        /// <param name="fieldName">字段名</param>
        IDataGridColumn Field( string fieldName );
        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text">文本</param>
        IDataGridColumn Text( string text );
        /// <summary>
        /// 是否允许排序
        /// </summary>
        /// <param name="isSort">是否允许排序</param>
        IDataGridColumn Sort( bool isSort = true );
        /// <summary>
        /// 是否显示复选框
        /// </summary>
        /// <param name="isShow">是否显示复选框</param>
        IDataGridColumn CheckBox( bool isShow = true );
        /// <summary>
        /// 设置格式化
        /// </summary>
        /// <param name="fn">格式化函数</param>
        IDataGridColumn Format( string fn );
        /// <summary>
        /// 格式化布尔值
        /// </summary>
        IDataGridColumn FormatBool();
        /// <summary>
        /// 格式化日期
        /// </summary>
        IDataGridColumn FormatDate();
    }
}
