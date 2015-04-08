using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Study.MongoDB.Core;
using Newtonsoft.Json;
using Study.BootStrap.Models;

namespace Study.BootStrap.Controllers
{
    public class FirstController : Controller
    {
        private MongoDbRemoteRepository<OpcModel> mongoDbRemoteRepository;

        public ActionResult Index()
        {
            return View();
        }

        public string TagData(string tagNo)
        {
            this.mongoDbRemoteRepository = new MongoDbRemoteRepository<OpcModel>();
            mongoDbRemoteRepository.Connect();
            var opcModels = mongoDbRemoteRepository.List(i => i.TagNo == tagNo).OrderBy(i => i.Time).ToList();
            string sd = "[";
            int k = 1;
            foreach (var modle in opcModels)
            {
                sd = sd + "{" + "\"Time\":\"" + modle.Id.Created.ToString("yyyy-MM-dd hh:mm:ss") + "\"," + "\"Value\"" + ":" + modle.Value + "},";
                k++;
                if(k>=100)
                {
                    break;
                }
            }
            sd = sd.Substring(0, sd.Length - 1) + "]";
            string content = JsonConvert.SerializeObject(opcModels);
            mongoDbRemoteRepository.CloseConnect();
            return sd;
        }
    }
}
