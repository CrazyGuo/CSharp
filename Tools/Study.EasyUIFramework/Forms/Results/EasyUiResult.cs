using System.Web.Mvc;
using Study.Util;

namespace Study.EasyUIFramework.Forms.Results 
{
    /// <summary>
    /// EasyUi提交表单返回结果
    /// </summary>
    public class EasyUiResult 
    {
        /// <summary>
        /// 初始化EasyUi提交表单返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">消息</param>
        public EasyUiResult( StateCode code, string message ) 
        {
            _code = code;
            _message = message;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        private readonly StateCode _code;
        /// <summary>
        /// 消息
        /// </summary>
        private readonly string _message;

        /// <summary>
        /// 获取结果
        /// </summary>
        public ActionResult GetResult() 
        {
            return new JsonResult { Data = new { Code = _code.Value(), Message = _message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
