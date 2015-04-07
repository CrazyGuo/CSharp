using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace SeedWork
{
    public static class InfraUtils
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            var result = "";
            if (expression.Body is UnaryExpression)
            {
                result = ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name;
            }
            else if (expression.Body is MemberExpression)
            {
                result = ((MemberExpression)expression.Body).Member.Name;
            }
            else if (expression.Body is ParameterExpression)
            {
                result = ((ParameterExpression)expression.Body).Type.Name;
            }
            return result;
        }
    }
}
