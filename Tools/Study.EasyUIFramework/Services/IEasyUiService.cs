using System;
using System.Linq.Expressions;
using Study.EasyUIFramework.Buttons;
using Study.EasyUIFramework.Forms.Comboxs;
using Study.EasyUIFramework.Forms.TextBoxs;
using Study.EasyUIFramework.Grids;
using Study.EasyUIFramework.Layouts;
using Study.EasyUIFramework.Menus;
using Study.EasyUIFramework.EasyuiJsNames;

namespace Study.EasyUIFramework.Services
{
    /// <summary>
    /// EasyUi服务
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IEasyUiService<TEntity> {
        /// <summary>
        /// 布局
        /// </summary>
        ILayout Layout();
        /// <summary>
        /// 链接按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        ILinkButton LinkButton( string text );
        /// <summary>
        /// 弹出窗口按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="url">弹出窗口网址</param>
        IDialogButton DialogButton( string text, string url = "" );
        /// <summary>
        /// 文本框
        /// </summary>
        ITextBox TextBox();
        /// <summary>
        /// 组合框
        /// </summary>
        ICombox Combox();
        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="id">Id</param>
        IMenu Menu(string id = MenuNameAndEvent.DataGridMenu);
        /// <summary>
        /// 菜单项
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="text">文本</param>
        /// <param name="iconClass">图标class</param>
        IMenuItem MenuItem( string id = "", string text = "", string iconClass = "" );
        /// <summary>
        /// 表格
        /// </summary>
        /// <param name="id">Id</param>
        IDataGrid Grid(string id = DataGridNameAndEvent.GridId);
        /// <summary>
        /// 表格列
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="text">文本</param>
        /// <param name="width">宽度</param>
        IDataGridColumn GridColumn( string field = "", string text = "", int? width = null );
        /// <summary>
        /// 文本框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        IEntityTextBox<TEntity, TProperty> TextBox<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression );
        /// <summary>
        /// 组合框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        IEntityCombox<TEntity, TProperty> Combox<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression );
    }
}
