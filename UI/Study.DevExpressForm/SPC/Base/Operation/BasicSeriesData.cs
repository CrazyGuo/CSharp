using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Base.Operation
{
    public class BasicSeriesData
    {
        public string SeriesName;
        public List<string> X = new List<string>();
        public List<double> Y = new List<double>();
        public BasicSeriesData()
        {

        }
        public BasicSeriesData(string name)
        {
            this.SeriesName = name;
        }
        public override string ToString()
        {
            return SeriesName;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
