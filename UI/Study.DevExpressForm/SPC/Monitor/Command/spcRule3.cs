using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor.Command
{
    public class spcRule3 : SPCCommandbase
    {
        public override int ID
        {
            get { return 3; }
        }
        public override int WarningCount
        {
            get { return 6; }
        }
        public override string Title
        {
            get { return "规则3"; }
        }

        public override string Description
        {
            get { return "连续6个点递增或递减"; }
        }
        private int count = 0;
        private double last = 0;
        private bool? up = true;
        public override bool Excute(double data, double ucl, double lcl, double standard)
        {
            if (data == last)
            {
                last = data;
                up = null;
                count = 0;
                return false;
            }
            bool temp = data > last;
            if (count > 0 && temp == up)
            {
                count++;
                last = data;
                if (count >= 6)
                    return true;
            }
            else
            {
                up = temp;
                count = 1;
                last = data;
            }
            return false;
        }
    }
}
