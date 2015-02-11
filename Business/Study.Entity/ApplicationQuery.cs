using Study.Domains.Framework.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Study.Entity
{
    public class ApplicationQuery : Pager
    {
        /// <summary>
        /// 应用程序编号
        /// </summary>
        [Display(Name = "应用程序编号")]
        public System.Guid? ApplicationId { get; set; }

        private string _code = string.Empty;
        /// <summary>
        /// 应用程序编码
        /// </summary>
        [Display(Name = "应用程序编码")]
        public string Code
        {
            get { return _code == null ? string.Empty : _code.Trim(); }
            set { _code = value; }
        }
        private string _name = string.Empty;
        /// <summary>
        /// 应用程序名称
        /// </summary>
        [Display(Name = "应用程序名称")]
        public string Name
        {
            get { return _name == null ? string.Empty : _name.Trim(); }
            set { _name = value; }
        }
        private string _note = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Note
        {
            get { return _note == null ? string.Empty : _note.Trim(); }
            set { _note = value; }
        }
        /// <summary>
        /// 启用
        /// </summary>
        [Display(Name = "启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 起始创建时间
        /// </summary>
        [Display(Name = "起始创建时间")]
        public System.DateTime? BeginCreateTime { get; set; }

        /// <summary>
        /// 结束创建时间
        /// </summary>
        [Display(Name = "结束创建时间")]
        public System.DateTime? EndCreateTime { get; set; }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("应用程序编号", ApplicationId);
            AddDescription("应用程序编码", Code);
            AddDescription("应用程序名称", Name);
            AddDescription("备注", Note);
            AddDescription("启用", "True");
            AddDescription("起始创建时间", BeginCreateTime);
            AddDescription("结束创建时间", EndCreateTime);
        }
    }
}
