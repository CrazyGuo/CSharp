using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor.Command
{
    public class spcRule7 : SPCCommandbase
    {
        public override int ID
        {
            get { return 7; }
        }
        public override int WarningCount
        {
            get { return 15; }
        }
        public override string Title
        {
            get { return "规则7"; }
        }

        public override string Description
        {
            get { return "连续15个点落在中心线两侧的C区内"; }
        }
        private int count;
        public override bool Excute(double data, double ucl, double lcl, double standard)
        {
            double bu = standard + ucl / 3;
            double bl = standard + lcl / 3;
            if (data < bu && data > bl)
                count++;
            else
                count = 0;
            if (count >= 15)
                return true;
            return false;
        }
    }
}
