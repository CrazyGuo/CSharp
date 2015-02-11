using System.Text.RegularExpressions;

namespace Study.Util 
{
    /// <summary>
    /// 正则表达式操作
    /// </summary>
    public class Regex 
    {
        /// <summary>
        /// 验证输入与模式是否匹配
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>        
        public static bool IsMatch( string input, string pattern ) 
        {
            return IsMatch( input, pattern, RegexOptions.IgnoreCase );
        }

        /// <summary>
        /// 验证输入与模式是否匹配
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件,比如是否忽略大小写</param>
        public static bool IsMatch( string input, string pattern, RegexOptions options ) 
        {
            return Regex.IsMatch( input, pattern, options );
        } 
    }
}
