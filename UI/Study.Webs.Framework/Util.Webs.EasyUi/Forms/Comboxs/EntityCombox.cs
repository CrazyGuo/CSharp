using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace Util.Webs.EasyUi.Forms.Comboxs 
{
    /// <summary>
    /// 实体组合框
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class EntityCombox<TEntity, TProperty> : Combox<IEntityCombox<TEntity, TProperty>>, IEntityCombox<TEntity, TProperty> 
    {
        /// <summary>
        /// 初始化实体组合框
        /// </summary>
        /// <param name="expression">属性表达式</param>
        public EntityCombox( Expression<Func<TEntity, TProperty>> expression )
            : this( expression,null ) 
        {
        }

        /// <summary>
        /// 初始化实体组合框
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        public EntityCombox( Expression<Func<TEntity, TProperty>> expression, HtmlHelper<TEntity> helper ) 
        {
            if ( helper != null )
                _metadata = ModelMetadata.FromLambdaExpression( expression, helper.ViewData );
            _expression = expression;
            _memberInfo = Lambda.GetMember( _expression );
            InitName();
            Load();
            InitValue();
            new DataAnnotationsLoader<IEntityCombox<TEntity, TProperty>, TEntity, TProperty>( this, _expression ).Load();
        }

        /// <summary>
        /// 属性表达式
        /// </summary>
        private readonly Expression<Func<TEntity, TProperty>> _expression;
        /// <summary>
        /// 成员
        /// </summary>
        private readonly MemberInfo _memberInfo;
        /// <summary>
        /// 元数据
        /// </summary>
        private readonly ModelMetadata _metadata;

        /// <summary>
        /// 初始化name属性
        /// </summary>
        private void InitName() 
        {
            if ( _metadata == null ) 
            {
                Name( Lambda.GetName( _expression ) );
                return;
            }
            Name( _metadata.PropertyName );
        }

        /// <summary>
        /// 初始值value属性
        /// </summary>
        private void InitValue() 
        {
            if ( _metadata == null )
                return;
            Select( _metadata.Model.ToStr() );
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void Load() 
        {
            if ( Reflection.IsBool( _memberInfo ) ) 
            {
                Bool();
                return;
            }
            if ( Reflection.IsEnum( _memberInfo ) ) 
            {
                Enum<TProperty>();
                return;
            }
        }
    }
}
