using Study.Util.Validations;
using Study.Validations.EntLib;

namespace Study.Domains.Framework 
{
    /// <summary>
    /// 验证工厂
    /// </summary>
    public class ValidationFactory 
    {
        /// <summary>
        /// 创建验证操作
        /// </summary>
        public static IValidation Create() 
        {
            return new Validation();
        }
    }
}
