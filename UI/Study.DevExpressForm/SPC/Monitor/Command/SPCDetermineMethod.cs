using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor.Command
{
    public class SPCDetermineMethod
    {
        public double UCL { get; set; }
        public double LCL { get; set; }
        public double Standard { get; set; }
        public List<SPCCommandbase> Commands = new List<SPCCommandbase>();
        public SPCDetermineMethod(double ucl, double lcl, double standard, params SPCCommandbase[] commands)
        {
            this.UCL = ucl;
            this.LCL = lcl;
            this.Standard = standard;
            foreach (var command in commands)
            {
                this.Commands.Add(command);
            }
        }
        public SPCDetermineMethod(double ucl, double lcl, double standard, List<SPCCommandbase> commands)
        {
            this.UCL = ucl;
            this.LCL = lcl;
            this.Standard = standard;
            this.Commands = commands;
        }
        public List<SPCCommandbase> Excute(double data)
        {
            List<SPCCommandbase> result = new List<SPCCommandbase>();
            foreach (var command in Commands)
            {
                if (command.Excute(data, this.UCL, this.LCL, this.Standard))
                    result.Add(command);
            }
            return result;
        }
    }
}
