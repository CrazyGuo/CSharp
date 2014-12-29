using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Entity.WebJobs
{
    public class JobInfo
    {
        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 公司性质
        /// </summary>
        public string Nature { get; set; }
        /// <summary>
        /// 公司规模
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// 月薪
        /// </summary>
        public string Salary { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 工作经验
        /// </summary>
        public string Experience { get; set; }
        /// <summary>
        /// 最低学历
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string Time { get; set; }

        public bool IsEmpty()
        {
            if (string.IsNullOrEmpty(Url) && string.IsNullOrEmpty(Position)
             && string.IsNullOrEmpty(Company) && string.IsNullOrEmpty(Nature)
             && string.IsNullOrEmpty(Scale) && string.IsNullOrEmpty(Salary)
             && string.IsNullOrEmpty(Address) && string.IsNullOrEmpty(Experience)
             && string.IsNullOrEmpty(Education) && string.IsNullOrEmpty(Time))
            {
                return true;
            }
            return false;
        }
    }
}
