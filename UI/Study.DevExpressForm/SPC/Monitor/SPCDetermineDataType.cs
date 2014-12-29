using Study.DevExpressForm.SPC.Monitor.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor
{
    public class SPCDetermineDataType
    {
        //public SPC.Base.Control.CanChooseDataGridView View;
        public IList<double> Items;
        public String Param;
        public double UCL;
        public double LCL;
        public double Standard;
        public List<SPCCommandbase> Commands;
        public SPCDetermineDataType(IList<double> items, string param, double ucl, double lcl, double standard, List<SPCCommandbase> commands)
        {
            this.Items = items;
            this.Param = param;
            this.UCL = ucl;
            this.LCL = lcl;
            this.Standard = standard;
            this.Commands = commands;
        }
    }
}
