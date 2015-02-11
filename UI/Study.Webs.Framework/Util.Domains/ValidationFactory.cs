using Util.Validations;
using Util.Validations.EntLib;

namespace Util.Domains 
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
