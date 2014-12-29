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
    public class JobFrom51Job : IJob
    {
        #region * 私有字段
        private string url = @"http://search.51job.com/jobsearch/search_result.php?";

        /// <summary>
        /// 工作地点
        /// </summary>
        private string workAddress;
        /// <summary>
        /// 工作地点ID
        /// </summary>
        private string workAddressId;
        /// <summary>
        /// 关键词
        /// </summary>
        private string keyWord;
        /// <summary>
        /// 包含词
        /// </summary>
        private string mustKey;
        #endregion

        public JobFrom51Job(string workAddress, string workAddressId, string keyWord, string mustKey)
        {
            this.workAddress = workAddress;
            this.workAddressId = workAddressId;
            this.keyWord = keyWord;
            this.mustKey = mustKey;
        }

        public event GetJobEndEventHandler GetJobEnd;
        public void GetJobInfoList()
        {
            try
            {
                StringBuilder condition = new StringBuilder();
                condition.Append("jobarea=" + workAddressId);
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = System.Web.HttpUtility.UrlEncode(keyWord, Encoding.GetEncoding("gb2312"));
                    condition.Append("&keyword=" + keyWord);
                }
                condition.Append("&keywordtype=2");

                url = url + condition.ToString();
                string html = GetHtmlCode.GetByget(url, "gb2312");
                GetJobInfoFromPage(html);

                int pageCount = 0;
                //页面数量
                string pageCountRegexStr = "(?<=name=\"jobid_count\"\\s*?value=\")\\d+(?=\">)";
                Regex pageCountRegex = new Regex(pageCountRegexStr);
                pageCount = (int.Parse(pageCountRegex.Match(html).Value) + 29) / 30;

                for (int i = 2; i <= pageCount; i++)
                {
                    string url0 = url + string.Format("&curr_page={0}", i);
                    html = GetHtmlCode.GetByget(url0, "gb2312");
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

        private void GetJobInfoFromPage(string pageStr)
        {
            try
            {
                pageStr = Regex.Replace(pageStr, "\\s", "");
                //职位所有信息
                string jobInfoRegexStr = "(?<=<trclass=\"tr0\").+?(?=</tr>)";
                Regex jobInfoRegex = new Regex(jobInfoRegexStr);
                MatchCollection jobInfoMC = jobInfoRegex.Matches(pageStr);
                //--
                foreach (Match m in jobInfoMC)
                {
                    if (m.Value.Contains(workAddress))
                    {
                        //职位URL
                        string urlRegexStr = "(?<=<aadid=\"\"href=\").+?(?=\")";
                        string url0 = Regex.Match(m.Value, urlRegexStr).Value;
                        GetJobInfoFromUrl(url0);
                    }
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
            new[] { @"(?is)<!--.*?-->", "" },    // 过滤Html代码中的注释
            new[] { @"(?is)<footer.*?>.*?</footer>",""},
            new[] { "(?is) <div style=\"width:470px; padding-left:5px;\">.*?</div>",""},
            new[] { "(?is)<div id=\"top\">.*?</iframe>	</div></div>",""},
            new[] { "(?is)<div class=\"grayline\" id=\"announcementbody\">.*?</li></ul>	</div>",""}
        };

        private void GetJobInfoFromUrl(string url)
        {
            try
            {
                JobInfo info = new JobInfo();
                //--
                string pageStr = GetHtmlCode.GetByget(url, "gb2312");
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
                //--
                if (!string.IsNullOrEmpty(mustKey) && !body.Contains(mustKey))
                {
                    return;
                }
                body = Regex.Replace(body, "\\s", "");

                info.Url = url;
                string basicInfoRegexStr0 = "<tdclass=\"sr_bt\"colspan=\"2\">(.*?)</td>"; //职位名称
                string position = Regex.Match(body, basicInfoRegexStr0).Value;
                if (string.IsNullOrEmpty(position))
                {
                    basicInfoRegexStr0 = "<tdclass=\"sr_bt\"colspan=\"3\">(.*?)</td>";
                    position = Regex.Match(body, basicInfoRegexStr0).Value;
                }
                info.Position = string.IsNullOrEmpty(position) ? "" : position.Substring(position.IndexOf(">") + 1, position.IndexOf("</") - position.IndexOf(">") - 1);

                string basicInfoRegexStr1 = ".html\">(.*?)</a>";//公司名称 
                string company = Regex.Match(body, basicInfoRegexStr1).Value;
                info.Company = string.IsNullOrEmpty(company) ? "" : company.Substring(company.IndexOf(">") + 1, company.IndexOf("</a>") - company.IndexOf(">") - 1);

                string basicInfoRegexStr2 = "工作地点：</td><tdclass=\"txt_2\">(.*?)</td>";//工作地点
                string address = Regex.Match(body, basicInfoRegexStr2).Value;
                info.Address = string.IsNullOrEmpty(address) ? "" : address.Substring(address.IndexOf("\">") + 2, address.LastIndexOf("</td>") - address.IndexOf("\">") - 2);

                string basicInfoRegexStr3 = "公司性质：</strong>&nbsp;&nbsp;(.*?)<br><br><strong>";//公司性质
                string nature = Regex.Match(body, basicInfoRegexStr3).Value;
                if (string.IsNullOrEmpty(nature))
                {
                    basicInfoRegexStr3 = "公司行业：</strong>&nbsp;&nbsp;(.*?)<br><br><strong>";
                    nature = Regex.Match(body, basicInfoRegexStr3).Value;
                }
                info.Nature = string.IsNullOrEmpty(nature) ? "" : nature.Substring(26, nature.IndexOf("<br>") - 26);//公司性质

                string basicInfoRegexStr4 = "公司规模：</strong>&nbsp;&nbsp;(.*?)</td>";//公司规模
                string scale = Regex.Match(body, basicInfoRegexStr4).Value;
                info.Scale = string.IsNullOrEmpty(scale) ? "" : scale.Substring(26, scale.IndexOf("</td>") - 26);

                string basicInfoRegexStr5 = "工作年限：</td><tdclass=\"txt_2\">(.*?)</td>";//工作经验
                string experience = Regex.Match(body, basicInfoRegexStr5).Value;
                info.Experience = string.IsNullOrEmpty(experience) ? "" : experience.Substring(experience.IndexOf("\">") + 2, experience.LastIndexOf("</td>") - experience.IndexOf("\">") - 2);

                string basicInfoRegexStr6 = "学&nbsp;&nbsp;&nbsp;&nbsp;历：</td><tdclass=\"txt_2\">(.*?)</td>";//学历
                string education = Regex.Match(body, basicInfoRegexStr6).Value;
                info.Education = string.IsNullOrEmpty(education) ? "" : education.Substring(education.IndexOf("\">") + 2, education.LastIndexOf("</td>") - education.IndexOf("\">") - 2);

                string basicInfoRegexStr7 = "薪水范围：</td><tdclass=\"txt_2\">(.*?)</td>";//月薪
                string salary = Regex.Match(body, basicInfoRegexStr7).Value;
                info.Salary = string.IsNullOrEmpty(salary) ? "" : salary.Substring(salary.IndexOf("\">") + 2, salary.LastIndexOf("</td>") - salary.IndexOf("\">") - 2);

                string basicInfoRegexStr8 = "发布日期：</td><tdclass=\"txt_2\">(.*?)</td>";//发布时间
                string time = Regex.Match(body, basicInfoRegexStr8).Value;
                info.Time = string.IsNullOrEmpty(time) ? "" : time.Substring(time.IndexOf("\">") + 2, time.LastIndexOf("</td>") - time.IndexOf("\">") - 2); ;

                if (GetJobEnd != null)
                {
                    GetJobEnd(pageStr, info);
                }
            }
            catch (Exception exMsg)
            {
                throw new Exception(exMsg.Message);
            }
        }
    }
}
