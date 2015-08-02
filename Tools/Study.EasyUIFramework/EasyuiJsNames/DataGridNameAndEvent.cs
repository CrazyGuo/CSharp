using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.EasyUIFramework.EasyuiJsNames
{
    public static class DataGridNameAndEvent
    {
        #region DataGrid常量属性 勿改动

        public const string EasyuiDatagrid = "easyui-datagrid";
        public const string RownumbersAtrribute = "rownumbers";
        public const string FitAtrribute = "fit";
        public const string FitColumnsAtrribute = "fitColumns";
        public const string PaginationAtrribute = "pagination";
        public const string PageSizeAtrribute = "pageSize";
        public const string SortNameAtrribute = "sortName";
        public const string SortOrderAtrribute = "sortOrder";
        public const string AscValue = "asc";
        public const string DescValue = "desc";
        public const string CheckOnSelectAtrribute = "checkOnSelect";
        public const string SelectOnCheckAtrribute = "selectOnCheck";
        public const string SingleSelectAtrribute = "singleSelect";
        public const string StripedAtrribute = "striped";
        public const string ToolbarAtrribute = "toolbar";
        public const string UrlAtrribute = "url";
        public const string QueryParamsAttribute = "queryParams";
        public const string OnDblClickRowEventAttribute = "onDblClickRow";
        public const string OnRowContextMenuEventAttribute = "onRowContextMenu";
        
        #endregion

        #region EasyUI JS 封装的DataGrid事件名称

        public const string OnDblClickRowEvent = "$.easyui.showEditDialog";
        public const string OnRowContextMenuEvent = "$.easyui.fnRightClickGridRow";
        public const string GetDefaultValuesEvent = "$.easyui.getDefaultParameters()";
        #endregion

        #region EasyUI JS 封装的DataGrids固定的Id

        public const string ToolbarId = "divQuery";
        public const string GridId = "grid";
        #endregion
        
    }
}
