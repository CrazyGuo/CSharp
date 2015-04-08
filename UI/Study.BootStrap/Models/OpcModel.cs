using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;

namespace Study.BootStrap.Models
{
    public class OpcModel
    {
        public Oid Id { get; set; }
        public string TagNo { get; set; }
        public double Value { get; set; }
        public string Time { get; set; }
    }
}