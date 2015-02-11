using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DynamicExpression = Study.Util.Lambdas.Dynamics.DynamicExpression;

namespace Study.Util 
{
    /// <summary>
    /// Lambda表达式操作
    /// </summary>
    public class Lambda 
    {

        #region GetName(获取成员名称)

        /// <summary>
        /// 获取成员名称，范例：t => t.Name,返回 Name
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name</param>
        public static string GetName( LambdaExpression expression ) 
        {
            var memberExpression = GetMemberExpression( expression );
            if ( memberExpression == null )
                return string.Empty;
            string result = memberExpression.ToString();
            return result.Substring( result.IndexOf( ".", StringComparison.Ordinal ) + 1 );
        }

        /// <summary>
        /// 获取成员表达式
        /// </summary>
        private static MemberExpression GetMemberExpression( LambdaExpression expression ) 
        {
            if ( expression == null )
                return null;
            var unaryExpression = expression.Body as UnaryExpression;
            if ( unaryExpression == null )
                return expression.Body as MemberExpression;
            return unaryExpression.Operand as MemberExpression;
        }

        #endregion

        #region GetMember(获取成员)

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name</param>
        public static MemberInfo GetMember( LambdaExpression expression ) 
        {
            var memberExpression = GetMemberExpression( expression );
            if ( memberExpression == null )
                return null;
            return memberExpression.Member;
        }

        #endregion

        #region GetValue(获取值)

        /// <summary>
        /// 获取值,范例：t => t.Name == "A",返回 A
        /// </summary>
        /// <param name="expression">表达式,范例：t => t.Name == "A"</param>
        public static object GetValue( LambdaExpression expression ) 
        {
            if ( expression == null )
                return null;
            var memberExpression = expression.Body as MemberExpression;
            if ( memberExpression != null )
                return GetMemberValue( memberExpression );
            BinaryExpression binaryExpression = GetBinaryExpression( expression );
            if ( binaryExpression != null )
                return GetBinaryValue( binaryExpression );
            var callExpression = expression.Body as MethodCallExpression;
            if ( callExpression != null )
                return GetMethodValue( callExpression );
            return null;
        }

        /// <summary>
        /// 获取二元表达式
        /// </summary>
        private static BinaryExpression GetBinaryExpression( LambdaExpression expression ) 
        {
            var binaryExpression = expression.Body as BinaryExpression;
            if ( binaryExpression != null )
                return binaryExpression;
            var unaryExpression = expression.Body as UnaryExpression;
            if ( unaryExpression == null )
                return null;
            return unaryExpression.Operand as BinaryExpression;
        }

        /// <summary>
        /// 获取二元表达式的值
        /// </summary>
        private static object GetBinaryValue( BinaryExpression binaryExpression ) 
        {
            var unaryExpression = binaryExpression.Right as UnaryExpression;
            if ( unaryExpression != null )
                return GetConstantValue( unaryExpression.Operand );
            var memberExpression = binaryExpression.Right as MemberExpression;
            if ( memberExpression != null )
                return GetMemberValue( memberExpression );
            return GetConstantValue( binaryExpression.Right );
        }

        /// <summary>
        /// 获取属性表达式的值
        /// </summary>
        private static object GetMemberValue( MemberExpression expression ) 
        {
            if ( expression == null )
                return null;
            var field = expression.Member as FieldInfo;
            if ( field != null ) {
                var constValue = GetConstantValue( expression.Expression );
                return field.GetValue( constValue );
            }
            var property = expression.Member as PropertyInfo;
            if ( property == null )
                return null;
            var value = GetMemberValue( expression.Expression as MemberExpression );
            return property.GetValue( value );
        }

        /// <summary>
        /// 获取常量值
        /// </summary>
        private static object GetConstantValue( Expression expression ) 
        {
            var constantExpression = expression as ConstantExpression;
            if ( constantExpression == null )
                return null;
            return constantExpression.Value;
        }

        /// <summary>
        /// 获取方法调用表达式的值
        /// </summary>
        private static object GetMethodValue( MethodCallExpression callExpression ) 
        {
            var argumentExpression = callExpression.Arguments.FirstOrDefault();
            var memberExpression = argumentExpression as MemberExpression;
            if ( memberExpression != null )
                return GetMemberValue( memberExpression );
            return GetConstantValue( argumentExpression );
        }

        #endregion

        #region GetParameter(获取参数)

        /// <summary>
        /// 获取参数，范例：t.Name,返回 t
        /// </summary>
        /// <param name="expression">表达式，范例：t.Name</param>
        public static ParameterExpression GetParameter( LambdaExpression expression ) 
        {
            if ( expression == null )
                return null;
            BinaryExpression binaryExpression = GetBinaryExpression( expression );
            if ( binaryExpression == null )
                return null;
            return GetParameterByMember( binaryExpression.Left );
        }

        /// <summary>
        /// 递归获取参数
        /// </summary>
        private static ParameterExpression GetParameterByMember( Expression expression ) 
        {
            if ( expression == null )
                return null;
            ParameterExpression result = expression as ParameterExpression;
            if ( result != null )
                return result;
            MemberExpression memberExpression = expression as MemberExpression;
            if ( memberExpression == null )
                return null;
            return GetParameterByMember( memberExpression.Expression );
        }

        #endregion

        #region GetAttribute(获取特性)

        /// <summary>
        /// 获取特性
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        public static TAttribute GetAttribute<TEntity, TProperty, TAttribute>( Expression<Func<TEntity, TProperty>> propertyExpression )
            where TAttribute : Attribute 
        {
            var memberInfo = GetMember( propertyExpression );
            return memberInfo.GetCustomAttribute<TAttribute>();
        }

        #endregion

        #region GetAttributes(获取特性列表)

        /// <summary>
        /// 获取特性列表
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        public static IEnumerable<TAttribute> GetAttributes<TEntity, TProperty, TAttribute>( Expression<Func<TEntity, TProperty>> propertyExpression ) where TAttribute : Attribute 
        {
            var memberInfo = GetMember( propertyExpression );
            return memberInfo.GetCustomAttributes<TAttribute>();
        }

        #endregion

        #region Constant(获取常量)

        /// <summary>
        /// 获取常量表达式，自动转换值的类型
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="value">值</param>
        public static ConstantExpression Constant( Expression expression, object value ) 
        {
            var memberExpression = expression as MemberExpression;
            if ( memberExpression == null )
                return Expression.Constant( value );
            return Expression.Constant( value, memberExpression.Type );
        }

        #endregion

        #region GetCriteriaCount(获取谓词条件的个数)

        /// <summary>
        /// 获取谓词条件的个数
        /// </summary>
        /// <param name="expression">谓词表达式,范例：t => t.Name == "A"</param>
        public static int GetCriteriaCount( LambdaExpression expression ) 
        {
            if ( expression == null )
                return 0;
            var result = expression.ToString().Replace( "AndAlso", "|" ).Replace( "OrElse", "|" );
            return result.Split( '|' ).Count();
        }

        #endregion

        #region Equal(等于表达式)

        /// <summary>
        /// 创建等于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static Expression<Func<T, bool>> Equal<T>( string propertyName, object value ) 
        {
            var parameter = CreateParameter<T>();
            return parameter.Property( propertyName )
                    .Equal( value )
                    .ToLambda<Func<T, bool>>( parameter );
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        private static ParameterExpression CreateParameter<T>() 
        {
            return Expression.Parameter( typeof( T ), "t" );
        }

        #endregion

        #region NotEqual(不等于表达式)

        /// <summary>
        /// 创建不等于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static Expression<Func<T, bool>> NotEqual<T>( string propertyName, object value ) 
        {
            var parameter = CreateParameter<T>();
            return parameter.Property( propertyName )
                    .NotEqual( value )
                    .ToLambda<Func<T, bool>>( parameter );
        }

        #endregion

        #region Greater(大于表达式)

        /// <summary>
        /// 创建大于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static Expression<Func<T, bool>> Greater<T>( string propertyName, object value ) 
        {
            var parameter = CreateParameter<T>();
            return parameter.Property( propertyName )
                    .Greater( value )
                    .ToLambda<Func<T, bool>>( parameter );
        }

        #endregion

        #region Less(小于表达式)

        /// <summary>
        /// 创建小于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static Expression<Func<T, bool>> Less<T>( string propertyName, object value ) 
        {
            var parameter = CreateParameter<T>();
            return parameter.Property( propertyName )
                    .Less( value )
                    .ToLambda<Func<T, bool>>( parameter );
        }

        #endregion

        #region GreaterEqual(大于等于表达式)

        /// <summary>
        /// 创建大于等于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static Expression<Func<T, bool>> GreaterEqual<T>( string propertyName, object value ) 
        {
            var parameter = CreateParameter<T>();
            return parameter.Property( propertyName )
                    .GreaterEqual( value )
                    .ToLambda<Func<T, bool>>( parameter );
        }

        #endregion

        #region LessEqual(小于等于表达式)

        /// <summary>
        /// 创建小于等于运算lambda表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static Expression<Func<T, bool>> LessEqual<T>( string propertyName, object value ) 
        {
            var parameter = CreateParameter<T>();
            return parameter.Property( propertyName )
                    .LessEqual( value )
                    .ToLambda<Func<T, bool>>( parameter );
        }

        #endregion

        #region Contains(调用Contains方法)

        /// <summary>
        /// 调用Contains方法
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static Expression<Func<T, bool>> Contains<T>( string propertyName, object value ) {
            return Call<T>( propertyName, "Contains", value );
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        private static Expression<Func<T, bool>> Call<T>( string propertyName, string methodName, object value ) 
        {
            var parameter = CreateParameter<T>();
            return parameter.Property( propertyName )
                .Call( methodName, value )
                .ToLambda<Func<T, bool>>( parameter );
        }

        #endregion

        #region Starts(调用StartsWith方法)

        /// <summary>
        /// 调用StartsWith方法
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static Expression<Func<T, bool>> Starts<T>( string propertyName, string value ) 
        {
            var parameter = CreateParameter<T>();
            var property = parameter.Property( propertyName );
            var call = Expression.Call( property, property.Type.GetMethod( "StartsWith", new Type[] { typeof( string ) } ),
                Expression.Constant( value ) );
            return call.ToLambda<Func<T, bool>>( parameter );
        }

        #endregion

        #region Ends(调用EndsWith方法)

        /// <summary>
        /// 调用EndsWith方法
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static Expression<Func<T, bool>> Ends<T>( string propertyName, string value ) 
        {
            var parameter = CreateParameter<T>();
            var property = parameter.Property( propertyName );
            var call = Expression.Call( property, property.Type.GetMethod( "EndsWith", new Type[] { typeof( string ) } ),
                Expression.Constant( value ) );
            return call.ToLambda<Func<T, bool>>( parameter );
        }

        #endregion

        #region ParsePredicate(解析为谓词表达式)

        /// <summary>
        /// 解析为谓词表达式
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        /// <param name="operator">运算符</param>
        public static Expression<Func<T, bool>> ParsePredicate<T>( string propertyName, object value, Operator @operator ) 
        {
            var parameter = Expression.Parameter( typeof( T ), "t" );
            return parameter.Property( propertyName ).Operation( @operator, value ).ToLambda<Func<T, bool>>( parameter );
        }

        /// <summary>
        /// 解析为谓词表达式
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicateExpression">谓词表达式字符串,参数占位符为@0,@1,@2 ...</param>
        /// <param name="values">值</param>
        public static Expression<Func<T, bool>> ParsePredicate<T>( string predicateExpression, params object[] values ) 
        {
            return DynamicExpression.ParseLambda( typeof( T ), typeof( bool ), predicateExpression, values ) as Expression<Func<T, bool>>;
        }

        #endregion
    }
}
