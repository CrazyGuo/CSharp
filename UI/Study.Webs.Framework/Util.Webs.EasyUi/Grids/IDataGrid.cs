using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Grids 
{
    /// <summary>
    /// 表格
    /// </summary>
    /// <typeparam name="T">表格类型</typeparam>
    public interface IDataGrid<out T> : IComponent<T> where T : IDataGrid<T> 
    {
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="isShow">是否显示行号</param>
        T RowNumber( bool isShow = true );
        /// <summary>
        /// 设置自适应布局
        /// </summary>
        /// <param name="isFit">是否自适应</param>
        T Fit( bool isFit = true );
        /// <summary>
        /// 设置列为自适应布局
        /// </summary>
        /// <param name="isFit">是否自适应</param>
        T FitColumns( bool isFit = true );
        /// <summary>
        /// 设置分页
        /// </summary>
        /// <param name="pageSize">每页显示行数</param>
        /// <param name="isPagination">是否分页</param>
        T Pagination( int pageSize = 20,bool isPagination = true );
        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="sortName">排序名</param>
        /// <param name="isAsc">是否正排序</param>
        T Sort( string sortName,bool isAsc = true );
        /// <summary>
        /// 选择表格行时是否同时选中复选框
        /// </summary>
        /// <param name="isCheck">是否选中</param>
        T CheckOnSelect( bool isCheck = true );
        /// <summary>
        /// 选中复选框时是否同时选中表格行
        /// </summary>
        /// <param name="isSelect">是否选中</param>
        T SelectOnCheck( bool isSelect = true );
        /// <summary>
        /// 是否只能选中一行
        /// </summary>
        /// <param name="isSingle">是否只能选中一行</param>
        T SingleSelect( bool isSingle = true );
        /// <summary>
        /// 设置是否显示条纹
        /// </summary>
        /// <param name="isShow">是否显示条纹</param>
        T Strip( bool isShow = true );
        /// <summary>
        /// 设置工具栏
        /// </summary>
        /// <param name="toolbarId">工具栏Id</param>
        T Toolbar( string toolbarId );
        /// <summary>
        /// 设置加载数据的Url
        /// </summary>
        /// <param name="url">Url</param>
        T Url( string url );
        /// <summary>
        /// 设置双击行事件处理函数
        /// </summary>
        /// <param name="handler">双击行事件处理函数</param>
        T OnDblClickRow( string handler );
        /// <summary>
        /// 设置右键单击行事件处理函数
        /// </summary>
        /// <param name="handler">右键单击行事件处理函数</param>
        T OnRowContextMenu( string handler );
        /// <summary>
        /// 设置列集合
        /// </summary>
        /// <param name="columns">列集合</param>
        T Columns( params IDataGridColumn[] columns );
    }
}
