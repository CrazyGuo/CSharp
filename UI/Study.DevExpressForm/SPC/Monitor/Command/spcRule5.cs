using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor.Command
{
    public class spcRule5 : SPCCommandbase
    {
        public override int ID
        {
            get { return 5; }
        }
        public override int WarningCount
        {
            get { return 3; }
        }
        public override string Title
        {
            get { return "规则5"; }
        }

        public override string Description
        {
            get { return "连续3个点中有2个点落在中心线同一侧的B区以外"; }
        }
        private List<double> old = new List<double>();
        private int count;
        public override bool Excute(double data, double ucl, double lcl, double standard)
        {
            double bu = standard + ucl / 3 * 2;
            double bl = standard + lcl / 3 * 2;
            int u = 0;
            int l = 0;
            old.Add(data);
            count++;
            if (count > 3)
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
            if (u >= 2 || l >= 2)
                return true;
            return false;
        }
    }
}
