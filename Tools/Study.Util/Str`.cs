using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace Study.Util 
{
    /// <summary>
    /// 字符串操作 - 工具方法
    /// </summary>
    public sealed partial class Str 
    {

        #region Empty(空字符串)

        /// <summary>
        /// 空字符串
        /// </summary>
        public static string Empty 
        {
            get { return string.Empty; }
        }

        #endregion

        #region Splice(拼接集合元素)

        /// <summary>
        /// 拼接集合元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Splice<T>( IEnumerable<T> list, string quotes = "", string separator = "," ) 
        {
            var result = new StringBuilder();
            foreach( var each in list )
                result.AppendFormat( "{0}{1}{0}{2}", quotes, each, separator );
            return result.ToString().TrimEnd( separator.ToCharArray() );
        }

        #endregion

        #region FirstUpper(将值的首字母大写)

        /// <summary>
        /// 将值的首字母大写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstUpper( string value ) 
        {
            string firstChar = value.Substring( 0, 1 ).ToUpper();
            return firstChar + value.Substring( 1, value.Length - 1 );
        }

        #endregion

        #region ToCamel(将字符串转成驼峰形式)

        /// <summary>
        /// 将字符串转成驼峰形式
        /// </summary>
        /// <param name="value">原始字符串</param>
        public static string ToCamel( string value ) 
        {
            return FirstUpper( value.ToLower() );
        }

        #endregion

        #region ContainsChinese(是否包含中文)

        /// <summary>
        /// 是否包含中文
        /// </summary>
        /// <param name="text">文本</param>
        public static bool ContainsChinese( string text ) 
        {
            const string pattern = "[\u4e00-\u9fa5]+";
            return Regex.IsMatch( text, pattern );
        }

        #endregion

        #region ContainsNumber(是否包含数字)

        /// <summary>
        /// 是否包含数字
        /// </summary>
        /// <param name="text">文本</param>
        public static bool ContainsNumber( string text ) 
        {
            const string pattern = "[0-9]+";
            return Regex.IsMatch( text, pattern );
        }

        #endregion

        #region Distinct(去除重复)

        /// <summary>
        /// 去除重复
        /// </summary>
        /// <param name="value">值，范例1："5555",返回"5",范例2："4545",返回"45"</param>
        public static string Distinct( string value ) 
        {
            var array = value.ToCharArray();
            return new string( array.Distinct().ToArray() );
        }

        #endregion

        #region Truncate(截断字符串)

        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="length">返回长度</param>
        /// <param name="endCharCount">添加结束符号的个数，默认0，不添加</param>
        /// <param name="endChar">结束符号，默认为省略号</param>
        public static string Truncate( string text, int length, int endCharCount = 0, string endChar = "." ) 
        {
            if ( string.IsNullOrWhiteSpace( text ) )
                return string.Empty;
            if ( text.Length < length )
                return text;
            return text.Substring( 0, length ) + GetEndString( endCharCount, endChar );
        }

        /// <summary>
        /// 获取结束字符串
        /// </summary>
        private static string GetEndString( int endCharCount, string endChar ) 
        {
            var result = new StringBuilder();
            for ( int i = 0; i < endCharCount; i++ )
                result.Append( endChar );
            return result.ToString();
        }

        #endregion

        #region ToSimplifiedChinese(转换为简体中文)

        /// <summary>
        /// 转换为简体中文
        /// </summary>
        /// <param name="text">繁体中文</param>
        public static string ToSimplifiedChinese( string text ) 
        {
            return Strings.StrConv( text, VbStrConv.SimplifiedChinese );
        }

        #endregion

        #region ToSimplifiedChinese(转换为繁体中文)

        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        /// <param name="text">简体中文</param>
        public static string ToTraditionalChinese( string text ) 
        {
            return Strings.StrConv( text, VbStrConv.TraditionalChinese );
        }

        #endregion

        #region Unique(获取全局唯一值)

        /// <summary>
        /// 获取全局唯一值
        /// </summary>
        public static string Unique() 
        {
            return Guid.NewGuid().ToString().Replace( "-", "" );
        }

        #endregion
    }
}
