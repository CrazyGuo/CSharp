using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Study.Entity.WebJobs;
using System.Web;
using NetUtility;

namespace Study.DevExpressForm.Job
{
    public class JobFromZhiLian : IJob
    {
        #region 私有字段
        private string url = @"http://sou.zhaopin.com/Jobs/SearchResult.ashx?";
        /// <summary>
        /// 工作地点
        /// </summary>
        private string workAddress;
        /// <summary>
        /// 关键词
        /// </summary>
        private string keyWord;
        /// <summary>
        /// 工资范围
        /// </summary>
        private string upperSalary;
        /// <summary>
        /// 工资范围
        /// </summary>
        private string lowerSalary;
        /// <summary>
        /// 包含词
        /// </summary>
        private string mustKey;
        #endregion

        public JobFromZhiLian(string workAddress, string keyWord, string upperSalary, string lowerSalary, string mustKey)
        {
            this.workAddress = workAddress;
            this.keyWord = keyWord;
            this.upperSalary = upperSalary;
            this.lowerSalary = lowerSalary;
            this.mustKey = mustKey;
        }

        public event GetJobEndEventHandler GetJobEnd;
        public void GetJobInfoList()
        {
            try
            {
                StringBuilder condition = new StringBuilder();
                workAddress = HttpUtility.UrlEncode(workAddress, Encoding.GetEncoding("utf-8"));
                condition.Append("jl=" + workAddress);
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = HttpUtility.UrlEncode(keyWord, Encoding.GetEncoding("utf-8"));
                    condition.Append("&kw=" + keyWord);
                }
                condition.Append("&sm=1");
                if (!string.IsNullOrEmpty(upperSalary))
                {
                    condition.Append("&sf=" + upperSalary);
                }
                if (!string.IsNullOrEmpty(lowerSalary))
                {
                    condition.Append("&st=" + lowerSalary);
                }

                url = url + condition.ToString();
                string html = GetHtmlCode.GetByget(url, "utf-8");
                GetJobInfoFromPage(html);

                //页面数量
                string pageCountRegexStr = "(?<=onkeypress=\"zlapply.searchjob.enter2Page\\(this,event,)\\d+";
                Regex pageCountRegex = new Regex(pageCountRegexStr);
                string pageCountStr = pageCountRegex.Match(html).Groups[0].Value;
                int pageCount = 0;
                int.TryParse(pageCountStr, out pageCount);

                for (int i = 2; i <= pageCount; i++)
                {
                    string url0 = url + string.Format("&p={0}", i);
                    html = GetHtmlCode.GetByget(url0, "utf-8");
                    GetJobInfoFromPage(html);
                }
                if (GetJobEnd != null)
                {
                    GetJobEnd(null, null);
                }
            }
            catch (Exception exMsg)
            {
                throw new Exception(exMsg.Message);
            }
        }


        // 正则表达式过滤：正则表达式，要替换成的文本
        private static readonly string[][] Filters =
        {
            new[] { @"(?is)<script.*?>.*?</script>", "" },
            new[] { @"(?is)<style.*?>.*?</style>", "" },
            new[] { @"(?is)<!--.*?-->", "" }    // 过滤Html代码中的注释
        };

        private void GetJobInfoFromPage(string pageStr)
        {
            try
            {
                JobInfo info = new JobInfo();
                //--
                if (string.IsNullOrEmpty(pageStr))
                {
                    return;
                }
                //--
                pageStr = pageStr.Replace("\r\n", "");//替换换行符
                // 获取html，body标签内容
                string body = string.Empty;
                string bodyFilter = @"(?is)<body.*?</body>";
                Match m = Regex.Match(pageStr, bodyFilter);
                if (m.Success)
                {
                    body = m.ToString().Replace("<tr >", "<tr>").Replace("\r\n", "");
                }
                // 过滤样式，脚本等不相干标签
                foreach (var filter in Filters)
                {
                    body = Regex.Replace(body, filter[0], filter[1]);
                }
                ////--
                //if (!string.IsNullOrEmpty(mustKey) && !body.Contains(mustKey))
                //{
                //    return;
                //}
                body = Regex.Replace(body, "\\s", "");
                bodyFilter = "(?is)<divclass=\"newlist_list_content\"id=\"newlist_list_content_table\">.*?</dd></dl></div></div></div>";
                Match m1 = Regex.Match(body, bodyFilter);
                if (m1.Success)
                {
                    body = m1.ToString();
                }




                //info.Url = xurl;

                if (GetJobEnd != null)
                {
                    GetJobEnd(pageStr, info);
                }

                //pageStr = Regex.Replace(pageStr, "\\s|&nbsp;|<br>|<strong>|</strong>|<b>|</b>", "");
                ////职位所有信息
                //string jobInfoRegexStr = "(?<=<tableclass=\"search-result-tab\">)[\\S\\s]+?(?=</table>)";
                //Regex jobInfoRegex = new Regex(jobInfoRegexStr);
                //MatchCollection jobInfoMC = jobInfoRegex.Matches(pageStr);
                //foreach (Match m in jobInfoMC)
                //{
                //    if (!string.IsNullOrEmpty(mustKey) && !m.Value.Contains(mustKey))
                //    {
                //        return;
                //    }

                //    JobInfo info = new JobInfo();

                //    //职位名称，url和公司名称
                //    string basicInfoRegexStr = "(?<=<ahref=\")([\\w.:+?()/%=#&]+)\"target=\"_blank\".*?>([\\s\\S]+?)(?=</a>)";
                //    //地点、公司性质、公司规模、经验、学历、职位月薪
                //    string basicInfoRegexStr0 = "(?<=地点：)[-/\\w]+(?=</span>)";
                //    string basicInfoRegexStr1 = "(?<=公司性质：)[-/\\w]+(?=</span>)";
                //    string basicInfoRegexStr2 = "(?<=公司规模：)[-/\\w]+(?=</span>)";
                //    string basicInfoRegexStr3 = "(?<=经验：)[-/\\w]+(?=</span>)";
                //    string basicInfoRegexStr4 = "(?<=学历：)[-/\\w]+(?=</span>)";
                //    string basicInfoRegexStr5 = "(?<=职位月薪：)[-/\\w]+(?=</span>)";
                //    //发布时间
                //    string timeInfoRegexStr = "(?<=releasetime\">)\\d{1,2}-\\d{1,2}-\\d{1,2}";

                //    Regex basicInfoRegex = new Regex(basicInfoRegexStr);
                //    MatchCollection basicInfoMC = basicInfoRegex.Matches(m.Value);
                //    info.Url = basicInfoMC[0].Groups[1].Value;
                //    info.Position = basicInfoMC[0].Groups[2].Value;
                //    info.Company = basicInfoMC[1].Groups[2].Value;
                //    Regex basicInfoRegex0 = new Regex(basicInfoRegexStr0);
                //    info.Address = new Regex(basicInfoRegexStr0).Match(m.Value).Value;
                //    info.Nature = new Regex(basicInfoRegexStr1).Match(m.Value).Value;
                //    info.Scale = new Regex(basicInfoRegexStr2).Match(m.Value).Value;
                //    info.Experience = new Regex(basicInfoRegexStr3).Match(m.Value).Value;
                //    info.Education = new Regex(basicInfoRegexStr4).Match(m.Value).Value;
                //    info.Salary = new Regex(basicInfoRegexStr5).Match(m.Value).Value;
                //    Regex timeInfoRegex = new Regex(timeInfoRegexStr);
                //    info.Time = timeInfoRegex.Match(m.Value).Value;


                //    if (GetJobEnd != null)
                //    {
                //        GetJobEnd(pageStr, info);
                //    }
                //}
            }
            catch (Exception exMsg)
            {
                throw new Exception(exMsg.Message);
            }
        }
    }
}
