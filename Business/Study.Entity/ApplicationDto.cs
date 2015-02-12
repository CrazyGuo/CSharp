using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Study.ApplicationServices;

namespace Study.Entity
{
    [DataContract]
    public class ApplicationDto : DtoBase
    {
        /// <summary>
        /// 应用程序编码
        /// </summary>
        [Required(ErrorMessage = "应用程序编码不能为空")]
        [StringLength(10, ErrorMessage = "应用程序编码输入过长，不能超过10位")]
        [Display(Name = "应用程序编码")]
        [DataMember]
        public string Code { get; set; }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        [Required(ErrorMessage = "应用程序名称不能为空")]
        [StringLength(30, ErrorMessage = "应用程序名称输入过长，不能超过30位")]
        [Display(Name = "应用程序名称")]
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(100, ErrorMessage = "备注输入过长，不能超过100位")]
        [Display(Name = "备注")]
        [DataMember]
        public string Note { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        [Required(ErrorMessage = "启用不能为空")]
        [Display(Name = "启用")]
        [DataMember]
        public bool Enabled { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "创建时间不能为空")]
        [Display(Name = "创建时间")]
        [DataMember]
        public System.DateTime CreateTime { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [Display(Name = "版本号")]
        [DataMember]
        public System.Byte[] Version { get; set; }

        /// <summary>
        /// 输出应用程序状态
        /// </summary>
        public override string ToString()
        {
            //return this.ToEntity().ToString();
            return string.Empty;
        }
    }
}
