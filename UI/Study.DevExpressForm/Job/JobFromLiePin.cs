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
    public class JobFromLiePin : IJob
    {
        #region * 私有字段
        private string url = @"http://www.liepin.com/zhaopin/?";

        //基本信息
        private string basicInfoRegexStr = "<a title=[\\s\\S]+?</a>";

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

        public JobFromLiePin(string workAddress, string workAddressId, string keyWord, string mustKey)
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
                condition.AppendFormat("dqs={0}", workAddressId);//地区
                condition.Append("&searchField=3");//行业
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = HttpUtility.UrlEncode(keyWord, Encoding.GetEncoding("utf-8"));
                    condition.Append("&key=" + keyWord);//职位名关键词
                }
                condition.Append("&pubTime=30");//发布时间
                string xurl = string.Empty;
                for (int i = 0; i < 100; i++)
                {
                    if (i > 0)
                    {
                        xurl = url + condition.ToString() + "&curPage=" + i;//页数
                    }
                    else
                    {
                        xurl = url + condition.ToString();
                    }
                    string html = GetHtmlCode.GetByget(xurl, "utf-8");
                    if (string.IsNullOrEmpty(html))
                    {
                        break;
                    }
                    GetJobInfoFromPage(html);
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
                MatchCollection ms = Regex.Matches(pageStr, basicInfoRegexStr);
                //--url
                string urlRegex = "(?<=href=\")([\\w.:+?()/%=#&]+)";
                //--
                foreach (Match m in ms)
                {
                    if (m.Value.Contains(workAddress))
                    {
                        string url0 = Regex.Match(m.Value, urlRegex).Value;
                        GetJobInfoFromUrl(url0);
                    }
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
            new[] { @"(?is)<!--.*?-->", "" },    // 过滤Html代码中的注释
            new[] { @"(?is)<footer.*?>.*?</footer>",""},
            //new[] { "(?is)<div class=\"job-require bottom-job-require\">.*?</div></div>",""}
            new[] { @"(?is)<h3>常用链接：.*?</ul>",""}
        };

        private void GetJobInfoFromUrl(string url)
        {
            try
            {
                JobInfo info = new JobInfo();
                //--
                string pageStr = GetHtmlCode.GetByget(url, "utf-8");
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

                string basicInfoRegexStr0 = "<h1title=([\\s\\S]+?)>(.*?)</h1>"; //职位名称
                string position = Regex.Match(body, basicInfoRegexStr0).Value;
                info.Position = string.IsNullOrEmpty(position) ? "" : position.Substring(position.IndexOf(">") + 1, position.IndexOf("</") - position.IndexOf(">") - 1);//职位名称

                string basicInfoRegexStr1 = "</h1><h3>(.*?)</h3>";//公司名称
                string company = Regex.Match(body, basicInfoRegexStr1).Value;
                info.Company = string.IsNullOrEmpty(company) ? "" : company.Substring(company.IndexOf("<h3>") + 4, company.IndexOf("</h3>") - company.IndexOf("<h3>") - 4);//公司名称

                string basicInfoRegexStr2 = "<divclass=\"resumeclearfix\"><span>(.*?)</span>";//工作地点
                string address = Regex.Match(body, basicInfoRegexStr2).Value;
                info.Address = string.IsNullOrEmpty(address) ? "" : address.Substring(address.IndexOf("<span>") + 6, address.IndexOf("</") - address.IndexOf("<span>") - 6);//工作地点

                string basicInfoRegexStr3 = "<li><span>企业性质：</span>(.*?)</li>";//公司性质
                string nature = Regex.Match(body, basicInfoRegexStr3).Value;
                info.Nature = string.IsNullOrEmpty(nature) ? "" : nature.Substring(nature.IndexOf("</span>") + 7, nature.IndexOf("</li>") - nature.IndexOf("</span>") - 7);//公司性质

                if (string.IsNullOrEmpty(info.Nature))
                {
                    string basicInfoRegexStr3_1 = "<br><span>性质：</span>(.*?)<br>";
                    string nature_1 = Regex.Match(body, basicInfoRegexStr3_1).Value;
                    info.Nature = string.IsNullOrEmpty(nature_1) ? "" : nature_1.Substring(nature_1.IndexOf("</span>") + 7, nature_1.LastIndexOf("<br>") - nature_1.IndexOf("</span>") - 7);//公司性质
                }

                string basicInfoRegexStr4 = "<li><span>企业规模：</span>(.*?)</li>";//公司规模
                string scale = Regex.Match(body, basicInfoRegexStr4).Value;
                info.Scale = string.IsNullOrEmpty(scale) ? "" : scale.Substring(scale.IndexOf("</span>") + 7, scale.IndexOf("</li>") - scale.IndexOf("</span>") - 7);//公司规模

                if (string.IsNullOrEmpty(info.Scale))
                {
                    string basicInfoRegexStr4_1 = "<br><span>规模：</span>(.*?)<br>";
                    string scale_1 = Regex.Match(body, basicInfoRegexStr4_1).Value;
                    info.Scale = info.Nature = string.IsNullOrEmpty(scale_1) ? "" : scale_1.Substring(scale_1.IndexOf("</span>") + 7, scale_1.LastIndexOf("<br>") - scale_1.IndexOf("</span>") - 7);//公司规模
                }

                string basicInfoRegexStr5 = "<spanclass=\"noborder\">(.*?)</span>";//工作经验
                string experience = Regex.Match(body, basicInfoRegexStr5).Value;
                info.Experience = string.IsNullOrEmpty(experience) ? "" : experience.Substring(experience.IndexOf(">") + 1, experience.IndexOf("</") - experience.IndexOf(">") - 1);//工作经验

                string basicInfoRegexStr6 = "</span><span>(.*?)</span><spanclass=\"noborder\">";//最低学历
                string education = Regex.Match(body, basicInfoRegexStr6).Value;
                info.Education = string.IsNullOrEmpty(education) ? "" : education.Substring(education.IndexOf("<span>") + 6, education.IndexOf("</span><spanclass=") - education.IndexOf("<span>") - 6);//最低学历

                string basicInfoRegexStr7 = "<pclass=\"job-main-title\">(.*?)<";//月薪
                string salary = Regex.Match(body, basicInfoRegexStr7).Value;
                info.Salary = string.IsNullOrEmpty(salary) ? "" : salary.Substring(salary.IndexOf(">") + 1, salary.LastIndexOf("<") - salary.IndexOf(">") - 1);//月薪

                string timeInfoRegexStr = "<pclass=\"release-time\">发布时间：<em>(.*?)</em></p>";//发布时间
                string time = Regex.Match(body, timeInfoRegexStr).Value;
                info.Time = string.IsNullOrEmpty(time) ? "" : time.Substring(time.IndexOf("<em>") + 4, time.IndexOf("</em>") - time.IndexOf("<em>") - 4);//发布时间

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
