using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor.Command
{
    public class spcRule6 : SPCCommandbase
    {
        public override int ID
        {
            get { return 6; }
        }
        public override int WarningCount
        {
            get { return 5; }
        }
        public override string Title
        {
            get { return "规则6"; }
        }

        public override string Description
        {
            get { return "连续5个点中有4个点落在中心线同一侧的C区以外"; }
        }
        private List<double> old = new List<double>();
        private int count;
        public override bool Excute(double data, double ucl, double lcl, double standard)
        {
            double bu = standard + ucl / 3;
            double bl = standard + lcl / 3;
            int u = 0;
            int l = 0;
            old.Add(data);
            count++;
            if (count > 5)
            {
                count--;
                old.RemoveAt(0);
            }
            foreach (double o in old)
            {
                if (o > bu)
                    u++;
                else if (o < bl)
                    l++;
            }
            if (u >= 4 || l >= 4)
                return true;
            return false;
        }
    }
}
