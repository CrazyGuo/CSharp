using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor.Command
{
    public abstract class SPCCommandbase
    {
        public abstract int ID { get; }
        public abstract int WarningCount { get; }
        public abstract string Title { get; }
        public abstract string Description { get; }
        public abstract bool Excute(double data, double ucl, double lcl, double standard);
        public override string ToString()
        {
            return this.Title + ":" + this.Description;
        }
    }
}
