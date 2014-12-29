using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NetUtility
{
    /// <summary>
    /// 获取网页源码的基本类
    /// </summary>
    public class GetHtmlCode
    {

        //public static string GetHtmlByCom(string url)
        //{
        //    XMLHTTP xmlhttp = new XMLHTTPClass();
        //    xmlhttp.open("get", url, false, null, null);
        //    xmlhttp.send("");
        //    while (xmlhttp.readyState != 4) Thread.Sleep(1);
        //    return xmlhttp.responseText;
        //}   

        /// <summary>
        /// 用Get方法返回网页源代码
        /// </summary>
        /// <param name="url">标识Internet资源的uri</param>
        /// <param name="encoding">网页编码</param>
        /// <returns>网页源代码</returns>
        public static string GetByget(string url, string encoding)
        {
            Stream sr = null;
            StreamReader sReader = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "Get";
                request.Timeout = 30000;

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                {
                    sr = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                }
                else
                {
                    sr = response.GetResponseStream();
                }
                sReader = new StreamReader(sr, System.Text.Encoding.GetEncoding(encoding));
                return sReader.ReadToEnd();
            }
            catch
            {
                return null;
            }
            finally
            {
                if (sReader != null)
                    sReader.Close();
                if (sr != null)
                    sr.Close();
            }
        }
        /// <summary>
        /// 用Get方法返回网页文件并保存
        /// </summary>
        /// <param name="url">标识Internet资源的uri</param>
        /// <param name="path">保存文件路径</param>
        /// <returns>网页源代码</returns>
        public static void GetStreamByget(string url, string path)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadFile(url, path);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 用POST方法返回网页源代码
        /// </summary>
        /// <param name="url">标识Internet资源的uri</param>
        /// <param name="postData">POST参数</param>
        /// <param name="encoding">网页编码</param>
        /// <returns>网页源代码</returns>
        public static string GetBypost(string url, string postData, string encoding)
        {
            string str = null;
            HttpWebResponse myResponse = null;
            try
            {
                byte[] POST = Encoding.GetEncoding(encoding).GetBytes(postData);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";
                myRequest.Timeout = 30000;
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = POST.Length;

                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(POST, 0, POST.Length);
                newStream.Close();
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding(encoding));
                str = reader.ReadToEnd();
                reader.Close();
            }
            catch (WebException e)
            {
                //myResponse = (HttpWebResponse)e.Response;
                Console.WriteLine(e.Message);
            }

            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="charSet"></param>
        /// <returns></returns>
        public static string getHtml(string url, string charSet)
        //url是要访问的网站地址，charSet是目标网页的编码，如果传入的是null或者""，那就自动分析网页的编码
        {
            WebClient myWebClient = new WebClient();
            //创建WebClient实例myWebClient 
            // 需要注意的：
            //有的网页可能下不下来，有种种原因比如需要cookie,编码问题等等
            //这是就要具体问题具体分析比如在头部加入cookie 
            // webclient.Headers.Add("Cookie", cookie); 
            //这样可能需要一些重载方法。根据需要写就可以了
            //获取或设置用于对向 Internet 资源的请求进行身份验证的网络凭据。
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
            //如果服务器要验证用户名,密码 
            //NetworkCredential mycred = new NetworkCredential(struser, strpassword);
            //myWebClient.Credentials = mycred; 
            //如果服务器需要referer头
            //myRequest.Referer = "http://flight.mangocity.com/flights-list.shtml";
            //从资源下载数据并返回字节数组。（加@是因为网址中间有"/"符号）
            byte[] myDataBuffer = myWebClient.DownloadData(url);
            string strWebData = Encoding.Default.GetString(myDataBuffer);
            //获取网页字符编码描述信息
            Match charSetMatch = Regex.Match(strWebData, "<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string webCharSet = charSetMatch.Groups[2].Value; if (charSet == null || charSet == "") charSet = webCharSet;
            if (charSet != null && charSet != "" && Encoding.GetEncoding(charSet) != Encoding.Default)
                strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
            return strWebData;
        }
        /// <summary>
        /// 通过Soap获取WebService的XML源码
        /// </summary>
        /// <param name="soap">Soap字符串</param>
        /// <param name="url">标识Internet资源的url</param>
        /// <returns></returns>
        public static string GetXMLBySoap(string soap, string url)
        {
            try
            {
                //发起请求
                Uri uri = new Uri(url);
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.CookieContainer = new CookieContainer();

                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(soap.ToString());
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }
                //响应
                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    return myStreamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
