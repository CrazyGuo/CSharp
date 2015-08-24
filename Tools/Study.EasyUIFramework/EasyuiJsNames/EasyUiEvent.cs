using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.EasyUIFramework.EasyuiJsNames
{
    public static class EasyUiEvent
    {
        #region 外部调用的框架常量属性 勿改动

        /// <summary>
        /// 新增确定事件
        /// </summary>
        public const string AddEvent = "$.easyui.submit()";
        public const string AddIcon = "icon-add";

        /// <summary>
        /// 查询事件
        /// </summary>
        public const string QueryEvent = "$.easyui.query()";
        public const string QueryIcon = "icon-search";

        /// <summary>
        /// 弹出编辑对话框事件
        /// </summary>
        public const string EditDialogEvent = "$.easyui.fnInitEdit";
        public const string EditIcon = "icon-edit";
        /// <summary>
        /// 删除数据事件
        /// </summary>
        public const string DeleteDialogEvent = "$.easyui.delete({0}{1}{2})";
        public const string DeleteIcon = "icon-delete";

        //菜单项对应的删除事件
        public const string MenuItemDeleteEvent = "$.easyui.delete({0})";
        public const string MenuItemEditEvent = "$.easyui.showEditDialog()";
        public const string MenuItemLookEvent = "$.easyui.showLookDialog()";

        /// <summary>
        /// 弹出查看对话框事件
        /// </summary>
        public const string LookDialogEvent = "$.easyui.fnInitLook";
        public const string LookIcon = "icon-look";

        /// <summary>
        /// 刷新事件
        /// </summary>
        public const string RefreshEvent = "$.easyui.refresh()";
        public const string RefreshIcon = "icon-refresh";

        /// <summary>
        /// 下载数据事件
        /// </summary>
        public const string DownloadEvent = "$.easyui.download({0})";
        public const string DownloadIcon = "icon-delete";

        public const string OKIcon = "icon-ok";
        public const string CancelIcon = "icon-cancel";

        public const string ButtonEdit = "btnEdit";
        public const string ButtonDetail = "btnLook";

        public const string MenuItemEdit = "menuItem_Edit";
        public const string MenuItemDelete = "menuItem_Delete";
        public const string MenuItemDetail = "menuItem_Look";

        public const string QueryFormUrl = "Parts/QueryForm";

        #endregion

        #region 框架默认取值 用户可修改

        public const string QueryOperation = "查 询";
        public const string AddOperation = "添加";
        public const string EditOperation = "编辑";
        public const string DeleteOperation = "删除";
        public const string DetailOperation = "查看详细";
        public const string RefreshOperation = "刷 新";
        public const string SaveOperation = "保 存";
        public const string CloseOperation = "关 闭";
        public const string DownloadOperation = "下 载";

        #endregion
    }
}
