using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;

namespace Study.Entity
{
    public class OpcModel
    {
        public Oid Id { get; set; }
        public string TagNo { get; set; }
        public double Value { get; set; }
        public string Time { get; set; }
    }
}
