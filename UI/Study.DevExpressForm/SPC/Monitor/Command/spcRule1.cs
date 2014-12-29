using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor.Command
{
    public class spcRule1 : SPCCommandbase
    {
        public override int ID
        {
            get { return 1; }
        }
        public override int WarningCount
        {
            get { return 1; }
        }
        public override string Title
        {
            get { return "规则1"; }
        }

        public override string Description
        {
            get { return "1个点落在A区以外"; }
        }

        public override bool Excute(double data, double ucl, double lcl, double standard)
        {
            if (data > ucl + standard || data < lcl + standard)
                return true;
            return false;
        }
    }
}
