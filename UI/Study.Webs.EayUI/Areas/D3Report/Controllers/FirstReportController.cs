using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Study.MongoDB.Core;
using Study.Entity;
using Newtonsoft.Json;
using MongoDB;

namespace Study.Webs.EayUI.Areas.D3Report.Controllers
{
    public class FirstReportController : Controller
    {
        private MongoDbRemoteRepository<OpcModel> mongoDbRemoteRepository;
        public FirstReportController(MongoDbRemoteRepository<OpcModel> remote)
        {
            this.mongoDbRemoteRepository = remote;
        }

        public ActionResult Index()
        {
            return View();
        }

        public string D3Json()
        {
            if (mongoDbRemoteRepository != null)
            {
                mongoDbRemoteRepository.Connect();
                var opcModels = mongoDbRemoteRepository.List(i => i.TagNo == "1TIC516JRQ-5A").OrderBy(i=>i.Time).ToList();
                if(opcModels.Count>0)
                {
                   string time = opcModels.ElementAt(0).Id.Created.ToString("yyyy-MM-dd hh:mm:ss");
                   string times = opcModels.ElementAt(0).Time;
                }
                string sd = "[";
                int k=1;
                foreach(var modle in opcModels)
                {
                    sd = sd + "{" +"\"Time\":"+ k + "," + "\"Value\"" + ":" + modle.Value + "},";
                    k++;
                }
                sd = sd.Substring(0, sd.Length - 1) + "]";
                string content = JsonConvert.SerializeObject(opcModels);
                mongoDbRemoteRepository.CloseConnect();
                return sd;
            }
            List<D3JsonC> list = new List<D3JsonC>();
            for(int i=0;i<3;i++)
            {
                D3JsonC json = new D3JsonC();
                json.date = i + 1;
                json.close = 20 + i;
                list.Add(json);
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(@"[{" + "\"date\"" + ":" + "\"1\"" + "," + "\"close\"" + ":" + "\"582.13\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"2\"" + "," + "\"close\"" + ":" + "\"583.98\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"3\"" + "," + "\"close\"" + ":" + "\"603\"" + "}]");
            var s = Json(list,JsonRequestBehavior.AllowGet);
            //return Line();
            return Dc();

        }

        public string Line()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"[{" + "\"date\"" + ":" + "\"1-May-12\"" + "," + "\"close\"" + ":" + "\"582.13\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"30-Apr-12\"" + "," + "\"close\"" + ":" + "\"583.98\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"27-Apr-12\"" + "," + "\"close\"" + ":" + "\"603\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"26-Apr-12\"" + "," + "\"close\"" + ":" + "\"607.7\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"25-Apr-12\"" + "," + "\"close\"" + ":" + "\"610\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"24-Apr-12\"" + "," + "\"close\"" + ":" + "\"560.28\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"23-Apr-12\"" + "," + "\"close\"" + ":" + "\"571.7\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"20-Apr-12\"" + "," + "\"close\"" + ":" + "\"572.98\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"19-Apr-12\"" + "," + "\"close\"" + ":" + "\"587.44\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"18-Apr-12\"" + "," + "\"close\"" + ":" + "\"608.34\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"17-Apr-12\"" + "," + "\"close\"" + ":" + "\"609.7\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"16-Apr-12\"" + "," + "\"close\"" + ":" + "\"580.13\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"13-Apr-12\"" + "," + "\"close\"" + ":" + "\"605.23\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"12-Apr-12\"" + "," + "\"close\"" + ":" + "\"622.77\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"11-Apr-12\"" + "," + "\"close\"" + ":" + "\"626.2\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"10-Apr-12\"" + "," + "\"close\"" + ":" + "\"628.44\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"09-Apr-12\"" + "," + "\"close\"" + ":" + "\"636.23\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"05-Apr-12\"" + "," + "\"close\"" + ":" + "\"633.68\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"04-Apr-12\"" + "," + "\"close\"" + ":" + "\"624.31\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"03-Apr-12\"" + "," + "\"close\"" + ":" + "\"629.32\"" + "},");
            builder.Append(@"{" + "\"date\"" + ":" + "\"02-Apr-12\"" + "," + "\"close\"" + ":" + "\"618.63\"" + "}]");
            return builder.ToString();
        }

        public string Dc()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T01:17:54Z\"" + "," + "\"quantity\"" + ":" + "2," + "\"total\"" + ":" + "190," + "\"tip\"" + ":" + "100," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T02:20:19Z\"" + "," + "\"quantity\"" + ":" + "2," + "\"total\"" + ":" + "190," + "\"tip\"" + ":" + "100," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T02:28:54Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "300," + "\"tip\"" + ":" + "200," + "\"type\"" + ":" + "\"visa\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T03:30:43Z\"" + "," + "\"quantity\"" + ":" + "3," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T05:48:46Z\"" + "," + "\"quantity\"" + ":" + "2," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T06:53:41Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T06:54:06Z\"" + "," + "\"quantity\"" + ":" + "4," + "\"total\"" + ":" + "100," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"cash\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T06:58:03Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T07:07:21Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T07:22:59Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T07:25:45Z\"" + "," + "\"quantity\"" + ":" + "5," + "\"total\"" + ":" + "200," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"cash\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T07:29:52Z\"" + "," + "\"quantity\"" + ":" + "5," + "\"total\"" + ":" + "200," + "\"tip\"" + ":" + "100," + "\"type\"" + ":" + "\"visa\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T08:17:54Z\"" + "," + "\"quantity\"" + ":" + "4," + "\"total\"" + ":" + "190," + "\"tip\"" + ":" + "100," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T09:20:19Z\"" + "," + "\"quantity\"" + ":" + "3," + "\"total\"" + ":" + "190," + "\"tip\"" + ":" + "100," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T10:28:54Z\"" + "," + "\"quantity\"" + ":" + "6," + "\"total\"" + ":" + "300," + "\"tip\"" + ":" + "200," + "\"type\"" + ":" + "\"visa\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T11:30:43Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T12:48:46Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T13:53:41Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T13:54:06Z\"" + "," + "\"quantity\"" + ":" + "2," + "\"total\"" + ":" + "100," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"cash\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T16:58:03Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "10," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T17:07:21Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "20," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T17:22:59Z\"" + "," + "\"quantity\"" + ":" + "1," + "\"total\"" + ":" + "90," + "\"tip\"" + ":" + "0," + "\"type\"" + ":" + "\"tab\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T17:25:45Z\"" + "," + "\"quantity\"" + ":" + "3," + "\"total\"" + ":" + "200," + "\"tip\"" + ":" + "50," + "\"type\"" + ":" + "\"cash\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T17:29:52Z\"" + "," + "\"quantity\"" + ":" + "4," + "\"total\"" + ":" + "200," + "\"tip\"" + ":" + "100," + "\"type\"" + ":" + "\"visa\"" + "},");

            builder.Append("{" + "\"date\"" + ":" + "\"2011-11-14T18:09:52Z\"" + "," + "\"quantity\"" + ":" + "4," + "\"total\"" + ":" + "200," + "\"tip\"" + ":" + "100," + "\"type\"" + ":" + "\"visa\"" + "}");
            builder.Append("]");
            return builder.ToString();
        }
    }

    public class D3JsonC
    {
        public int date;
        public int close;
    }

}