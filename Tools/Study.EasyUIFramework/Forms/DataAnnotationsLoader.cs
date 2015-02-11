using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Study.Util;
using Study.EasyUIFramework.Forms.TextBoxs;

namespace Study.EasyUIFramework.Forms 
{
    /// <summary>
    /// 数据注解加载器
    /// </summary>
    /// <typeparam name="TControl">控件类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class DataAnnotationsLoader<TControl, TEntity, TProperty> where TControl : ITextBox<TControl> 
    {
        /// <summary>
        /// 初始化表达式加载器
        /// </summary>
        /// <typeparam name="TControl">控件类型</typeparam>
        /// <param name="control">控件</param>
        /// <param name="expression">属性表达式</param>
        public DataAnnotationsLoader( ITextBox<TControl> control, Expression<Func<TEntity, TProperty>> expression ) 
        {
            _control = control;
            _expression = expression;
        }

        /// <summary>
        /// 控件
        /// </summary>
        private readonly ITextBox<TControl> _control;
        /// <summary>
        /// 属性表达式
        /// </summary>
        private readonly Expression<Func<TEntity, TProperty>> _expression;

        /// <summary>
        /// 从表达式加载数据到控件
        /// </summary>
        public void Load() 
        {
            var attributes = Lambda.GetAttributes<TEntity, TProperty, ValidationAttribute>( _expression );
            foreach ( var attribute in attributes ) 
            {
                InitValidation( attribute );
            }
        }

        /// <summary>
        /// 初始化验证
        /// </summary>
        private void InitValidation( ValidationAttribute validationAttribute ) 
        {
            if ( validationAttribute is RequiredAttribute ) 
            {
                InitRequired( validationAttribute as RequiredAttribute );
                return;
            }
            if ( validationAttribute is StringLengthAttribute ) 
            {
                InitStringLength( validationAttribute as StringLengthAttribute );
                return;
            }
        }

        /// <summary>
        /// 初始化必填项验证
        /// </summary>
        private void InitRequired( RequiredAttribute attribute ) 
        {
            _control.Required( attribute.GetErrorMessage() );
        }

        /// <summary>
        /// 初始化字符串长度验证
        /// </summary>
        private void InitStringLength( StringLengthAttribute attribute ) 
        {
            if ( attribute.MinimumLength <= 0 ) 
            {
                _control.MaxLength( attribute.MaximumLength );
                return;
            }
            _control.Length( attribute.MinimumLength, attribute.MaximumLength );
        }
    }
}
