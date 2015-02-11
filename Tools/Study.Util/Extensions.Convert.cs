using System;
using System.Collections.Generic;
using System.Linq;

namespace Study.Util 
{
    /// <summary>
    /// 类型转换扩展
    /// </summary>
    public static partial class Extensions 
    {
        /// <summary>
        /// 转换为int
        /// </summary>
        /// <param name="data">数据</param>
        public static int ToInt( this string data ) 
        {
            return Conv.ToInt( data );
        }

        /// <summary>
        /// 转换为可空int
        /// </summary>
        /// <param name="data">数据</param>
        public static int? ToIntOrNull( this string data ) 
        {
            return Conv.ToIntOrNull( data );
        }

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <param name="data">数据</param>
        public static double ToDouble( this string data ) 
        {
            return Conv.ToDouble( data );
        }

        /// <summary>
        /// 转换为可空double
        /// </summary>
        /// <param name="data">数据</param>
        public static double? ToDoubleOrNull( this string data ) 
        {
            return Conv.ToDoubleOrNull( data );
        }

        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal ToDecimal( this string data ) 
        {
            return Conv.ToDecimal( data );
        }

        /// <summary>
        /// 转换为可空decimal
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal? ToDecimalOrNull( this string data ) 
        {
            return Conv.ToDecimalOrNull( data );
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime ToDate( this string data ) 
        {
            return Conv.ToDate( data );
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime? ToDateOrNull( this string data ) 
        {
            return Conv.ToDateOrNull( data );
        }

        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="data">数据</param>
        public static Guid ToGuid( this string data ) 
        {
            return Conv.ToGuid( data );
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        /// <param name="data">数据</param>
        public static Guid? ToGuidOrNull( this string data ) 
        {
            return Conv.ToGuidOrNull( data );
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="data">数据,范例: "83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A"</param>
        public static List<Guid> ToGuidList( this string data ) 
        {
            return Conv.ToGuidList( data );
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="data">字符串集合</param>
        public static List<Guid> ToGuidList( this IList<string> data ) 
        {
            if ( data == null )
                return new List<Guid>();
            return data.Select( t => t.ToGuid() ).ToList();
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="data">对象</param>
        public static string ToStr( this object data ) 
        {
            return Conv.ToString( data );
        }
    }
}
