using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Study.EasyUIFramework.Forms.Comboxs;
using Study.EasyUIFramework.Forms.TextBoxs;
using Study.EasyUIFramework.Services;

namespace Study.EasyUIFramework 
{
    /// <summary>
    /// EasyUi工厂
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class EasyUiFactory<TEntity>
    {
        /// <summary>
        /// 创建EasyUi服务
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public static IEasyUiService<TEntity> CreateEasyUiService( HtmlHelper<TEntity> helper ) 
        {
            return new EasyUiService<TEntity>( helper );
        }

        /// <summary>
        /// 创建文本框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        public static IEntityTextBox<TEntity, TProperty> CreateTextBox<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, HtmlHelper<TEntity> helper ) 
        {
            return new EntityTextBox<TEntity, TProperty>( propertyExpression, helper );
        }

        /// <summary>
        /// 创建组合框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        public static IEntityCombox<TEntity, TProperty> CreateCombox<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, HtmlHelper<TEntity> helper ) 
        {
            return new EntityCombox<TEntity, TProperty>( propertyExpression, helper );
        }
    }
}
