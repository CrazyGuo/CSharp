using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor.Command
{
    public class spcRule2 : SPCCommandbase
    {
        public override int ID
        {
            get { return 2; }
        }
        public override int WarningCount
        {
            get { return 9; }
        }
        public override string Title
        {
            get { return "规则2"; }
        }

        public override string Description
        {
            get { return "连续9个点落在中心线同一侧"; }
        }
        private int count = 0;
        private bool? up = true;
        public override bool Excute(double data, double ucl, double lcl, double standard)
        {
            if (data == standard)
            {
                up = null;
                count = 0;
                return false;
            }
            bool temp = data > standard;
            if (count > 0 && temp == up)
            {
                count++;
                if (count >= 9)
                    return true;
            }
            else
            {
                up = temp;
                count = 1;
            }
            return false;
        }
    }
}
