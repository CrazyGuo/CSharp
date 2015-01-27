using System.Text;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Grids 
{
    /// <summary>
    /// 表格列
    /// </summary>
    public class DataGridColumn : ComponentBase<IDataGridColumn>, IDataGridColumn 
    {
        /// <summary>
        /// 文本
        /// </summary>
        private string _text;

        /// <summary>
        /// 设置字段名
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public IDataGridColumn Field( string fieldName ) 
        {
            AddDataOption( "field", GetValue( fieldName ) );
            return This();
        }

        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text">文本</param>
        public IDataGridColumn Text( string text ) 
        {
            _text = text;
            return This();
        }

        /// <summary>
        /// 是否允许排序
        /// </summary>
        /// <param name="isSort">是否允许排序</param>
        public IDataGridColumn Sort( bool isSort = true ) 
        {
            AddDataOption( "sortable", isSort );
            return This();
        }

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        /// <param name="isShow">是否显示复选框</param>
        public IDataGridColumn CheckBox( bool isShow = true ) 
        {
            AddDataOption( "checkbox", isShow );
            return This();
        }

        /// <summary>
        /// 设置格式化
        /// </summary>
        /// <param name="fn">格式化函数</param>
        public IDataGridColumn Format( string fn ) 
        {
            AddDataOption( "formatter", fn );
            return This();
        }

        /// <summary>
        /// 格式化布尔值
        /// </summary>
        public IDataGridColumn FormatBool() 
        {
            return Format( "$.easyui.formatBool" );
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        public IDataGridColumn FormatDate() 
        {
            return Format( "$.easyui.formatDate" );
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() 
        {
            var result = new StringBuilder();
            result.AppendFormat( "<th {0}>", GetOptions() );
            result.Append( _text );
            result.Append( "</th>" );
            return result.ToString();
        }
    }
}
