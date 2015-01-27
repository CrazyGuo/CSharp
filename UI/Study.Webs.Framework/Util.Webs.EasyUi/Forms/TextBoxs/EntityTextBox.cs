using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace Util.Webs.EasyUi.Forms.TextBoxs 
{
    /// <summary>
    /// 实体文本框
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class EntityTextBox<TEntity, TProperty> : TextBox<IEntityTextBox<TEntity, TProperty>>, IEntityTextBox<TEntity, TProperty> 
    {
        /// <summary>
        /// 初始化实体文本框
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        public EntityTextBox( Expression<Func<TEntity, TProperty>> propertyExpression )
            : this( propertyExpression, null ) 
        {
        }

        /// <summary>
        /// 初始化实体文本框
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        public EntityTextBox( Expression<Func<TEntity, TProperty>> expression, HtmlHelper<TEntity> helper ) 
        {
            if ( helper != null )
                _metadata = ModelMetadata.FromLambdaExpression( expression, helper.ViewData );
            _expression = expression;
            _memberInfo = Lambda.GetMember( _expression );
            Init();
            new DataAnnotationsLoader<IEntityTextBox<TEntity, TProperty>, TEntity, TProperty>( this, expression ).Load();
        }

        /// <summary>
        /// 元数据
        /// </summary>
        private readonly ModelMetadata _metadata;
        /// <summary>
        /// 属性表达式
        /// </summary>
        private readonly Expression<Func<TEntity, TProperty>> _expression;
        /// <summary>
        /// 成员
        /// </summary>
        private readonly MemberInfo _memberInfo;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init() {
            InitName();
            InitValue();
            InitType();
        }

        /// <summary>
        /// 初始化name属性
        /// </summary>
        private void InitName() {
            if ( _metadata == null ) {
                Name( Lambda.GetName( _expression ) );
                return;
            }
            Name( _metadata.PropertyName );
        }

        /// <summary>
        /// 初始值value属性
        /// </summary>
        private void InitValue() {
            if ( _metadata == null )
                return;
            Value( _metadata.Model.ToStr() );
        }

        /// <summary>
        /// 初始化类型
        /// </summary>
        private void InitType() {
            if ( Reflection.IsDate( _memberInfo ) ) {
                Date();
            }
        }
    }
}
