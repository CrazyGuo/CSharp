using System.Collections.Generic;
using System.Text;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Grids 
{
    /// <summary>
    /// 表格
    /// </summary>
    /// <typeparam name="T">表格类型</typeparam>
    public abstract class DataGrid<T> : ComponentBase<T>, IDataGrid<T> where T : IDataGrid<T> 
    {
        /// <summary>
        /// 初始化表格
        /// </summary>
        protected DataGrid() 
        {
            AddClass( "easyui-datagrid" );
            _columns = new List<IDataGridColumn>();
        }

        /// <summary>
        /// 列集合
        /// </summary>
        private readonly List<IDataGridColumn> _columns; 

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="isShow">是否显示行号</param>
        public T RowNumber( bool isShow = true ) 
        {
            AddDataOption( "rownumbers", isShow );
            return This();
        }

        /// <summary>
        /// 设置自适应布局
        /// </summary>
        /// <param name="isFit">是否自适应</param>
        public T Fit( bool isFit = true ) 
        {
            AddDataOption( "fit", isFit );
            return This();
        }

        /// <summary>
        /// 设置列为自适应布局
        /// </summary>
        /// <param name="isFit">是否自适应</param>
        public T FitColumns( bool isFit = true ) 
        {
            AddDataOption( "fitColumns", isFit );
            return This();
        }

        /// <summary>
        /// 设置分页
        /// </summary>
        /// <param name="pageSize">每页显示行数</param>
        /// <param name="isPagination">是否分页</param>
        public T Pagination( int pageSize = 20, bool isPagination = true ) 
        {
            AddDataOption( "pagination", isPagination ).AddDataOption( "pageSize", pageSize.ToString() );
            return This();
        }

        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="sortName">排序名</param>
        /// <param name="isAsc">是否正排序</param>
        public T Sort( string sortName, bool isAsc = true ) 
        {
            AddDataOption( "sortName", GetValue( sortName ) );
            string sort = isAsc ? "asc" : "desc";
            AddDataOption( "sortOrder", GetValue( sort ) );
            return This();
        }

        /// <summary>
        /// 选择表格行时是否同时选中复选框
        /// </summary>
        /// <param name="isCheck">是否选中</param>
        public T CheckOnSelect( bool isCheck = true ) 
        {
            AddDataOption( "checkOnSelect", isCheck );
            return This();
        }

        /// <summary>
        /// 选中复选框时是否同时选中表格行
        /// </summary>
        /// <param name="isSelect">是否选中</param>
        public T SelectOnCheck( bool isSelect = true ) 
        {
            AddDataOption( "selectOnCheck", isSelect );
            return This();
        }

        /// <summary>
        /// 是否只能选中一行
        /// </summary>
        /// <param name="isSingle">是否只能选中一行</param>
        public T SingleSelect( bool isSingle = true ) 
        {
            AddDataOption( "singleSelect", isSingle );
            return This();
        }

        /// <summary>
        /// 设置是否显示条纹
        /// </summary>
        /// <param name="isShow">是否显示条纹</param>
        public T Strip( bool isShow = true ) 
        {
            AddDataOption( "striped", isShow );
            return This();
        }

        /// <summary>
        /// 设置工具栏
        /// </summary>
        /// <param name="toolbarId">工具栏Id</param>
        public T Toolbar( string toolbarId ) 
        {
            AddDataOption( "toolbar", GetValue( "#" + toolbarId ) );
            return This();
        }

        /// <summary>
        /// 设置加载数据的Url
        /// </summary>
        /// <param name="url">Url</param>
        public T Url( string url ) 
        {
            AddDataOption( "url", GetValue( url ) );
            return This();
        }

        /// <summary>
        /// 设置双击行事件处理函数
        /// </summary>
        /// <param name="handler">双击行事件处理函数</param>
        public T OnDblClickRow( string handler ) 
        {
            AddDataOption( "onDblClickRow", handler );
            return This();
        }

        /// <summary>
        /// 设置右键单击行事件处理函数
        /// </summary>
        /// <param name="handler">右键单击行事件处理函数</param>
        public T OnRowContextMenu( string handler ) 
        {
            AddDataOption( "onRowContextMenu", handler );
            return This();
        }

        /// <summary>
        /// 设置列集合
        /// </summary>
        /// <param name="columns">列集合</param>
        public T Columns( params IDataGridColumn[] columns )
        {
            if ( columns == null )
                return This();
            _columns.AddRange( columns );
            return This();
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() 
        {
            var result = new StringBuilder();
            result.AppendFormat( "<table {0}>", GetOptions() );
            result.Append( "<thead><tr>" );
            foreach( var column in _columns ) 
            {
                result.Append( column );
            }
            result.Append( "</tr></thead>" );
            result.Append( "</table>" );
            return result.ToString();
        }
    }
}
