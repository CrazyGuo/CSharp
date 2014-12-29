using Study.Entity.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.Job
{
    public class JobFactory
    {
        #region * 私有字段
        /// <summary>
        /// 网站链接
        /// </summary>
        private string url;

        /// <summary>
        /// 工作地点
        /// </summary>
        private string workAddress;

        /// <summary>
        /// 工作地点ID
        /// </summary>
        private string workAddressId;

        /// <summary>
        /// 必须宝航
        /// </summary>
        private string keyWord;

        /// <summary>
        /// 薪水范围
        /// </summary>
        private string upperSalary;

        /// <summary>
        /// 薪水范围
        /// </summary>
        private string lowerSalary;

        /// <summary>
        /// 包含词
        /// </summary>
        private string mustKey;
        #endregion

        public JobFactory(string url, string workAddress, string workAddressId, string keyWord, string upperSalary, string lowerSalary, string mustKey)
        {
            this.url = url;
            this.workAddress = workAddress;
            this.workAddressId = workAddressId;
            this.keyWord = keyWord;
            this.upperSalary = upperSalary;
            this.lowerSalary = lowerSalary;
            this.mustKey = mustKey;
        }

        public IJob GetJob()
        {
            try
            {
                IJob job = null;
                switch (this.url)
                {
                    case "猎聘":
                        job = new JobFromLiePin(workAddress, workAddressId, keyWord, mustKey);
                        break;
                    case "51Job":
                        job = new JobFrom51Job(workAddress, workAddressId, keyWord, mustKey);
                        break;
                    case "智联":
                        job = new JobFromZhiLian(workAddress, keyWord, upperSalary, lowerSalary, mustKey);
                        break;
                }
                return job;
            }
            catch (Exception exMsg)
            {
                throw new Exception(exMsg.Message);
            }
        }
    }

    public delegate void GetJobEndEventHandler(object o, JobInfo e);
    public interface IJob
    {
        event GetJobEndEventHandler GetJobEnd;
        void GetJobInfoList();
    }
}
