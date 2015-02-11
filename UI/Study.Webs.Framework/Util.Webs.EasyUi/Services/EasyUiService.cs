using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Util.Webs.EasyUi.Buttons;
using Util.Webs.EasyUi.Forms.Comboxs;
using Util.Webs.EasyUi.Forms.TextBoxs;
using Util.Webs.EasyUi.Grids;
using Util.Webs.EasyUi.Layouts;
using Util.Webs.EasyUi.Menus;
using Study.Webs.Framework;

namespace Util.Webs.EasyUi.Services 
{
    /// <summary>
    /// EasyUi服务
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class EasyUiService<TEntity> : IEasyUiService<TEntity> 
    {
        /// <summary>
        /// 初始化EasyUi服务
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public EasyUiService( HtmlHelper<TEntity> helper ) 
        {
            _helper = helper;
        }

        /// <summary>
        /// HtmlHelper
        /// </summary>
        private HtmlHelper<TEntity> _helper;
        /// <summary>
        /// 布局
        /// </summary>
        public ILayout Layout() 
        {
            return EasyUiFactory.CreateLayout();
        }

        /// <summary>
        /// 链接按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        public ILinkButton LinkButton( string text ) 
        {
            return EasyUiFactory.CreateLinkButton( text );
        }

        /// <summary>
        /// 弹出窗口按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="url">弹出窗口网址</param>
        public IDialogButton DialogButton( string text, string url = "" ) 
        {
            return EasyUiFactory.CreateDialogButton( text, url );
        }

        /// <summary>
        /// 文本框
        /// </summary>
        public ITextBox TextBox() 
        {
            return EasyUiFactory.CreateTextBox();
        }

        /// <summary>
        /// 组合框
        /// </summary>
        public ICombox Combox() 
        {
            return EasyUiFactory.CreateCombox();
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="id">Id</param>
        public IMenu Menu( string id ) 
        {
            return EasyUiFactory.CreateMenu( id );
        }

        /// <summary>
        /// 菜单项
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="text">文本</param>
        /// <param name="iconClass">图标class</param>
        public IMenuItem MenuItem( string id = "", string text = "", string iconClass = "" ) 
        {
            return EasyUiFactory.CreateMenuItem().Id( id ).Text( text ).Icon( iconClass );
        }

        /// <summary>
        /// 表格
        /// </summary>
        /// <param name="id">Id</param>
        public IDataGrid Grid( string id = "" ) 
        {
            return EasyUiFactory.CreateDataGrid().Id( id ).RowNumber().Fit().FitColumns()
                .Pagination().CheckOnSelect( false ).SelectOnCheck( false ).SingleSelect().Strip();
        }

        /// <summary>
        /// 表格列
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="text">文本</param>
        /// <param name="width">宽度</param>
        public IDataGridColumn GridColumn( string field = "", string text = "", int? width = null ) 
        {
            return EasyUiFactory.CreateDataGridColumn().Field( field ).Text( text ).Width( width );
        }

        /// <summary>
        /// 文本框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        public IEntityTextBox<TEntity, TProperty> TextBox<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression ) 
        {
            return EasyUiFactory<TEntity>.CreateTextBox( propertyExpression, _helper );
        }

        /// <summary>
        /// 组合框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        public IEntityCombox<TEntity, TProperty> Combox<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression ) 
        {
            return EasyUiFactory<TEntity>.CreateCombox( propertyExpression, _helper );
        }
    }
}
