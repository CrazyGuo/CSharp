using Study.DevExpressForm.SPC.Base.Interface;
using Study.DevExpressForm.SPC.Base.Operation;
using Study.DevExpressForm.SPC.Monitor.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor
{
    public class SPCDetermineSeriesMaker : ISeriesMaker<SPCDetermineDataType>
    {
        public BasicSeriesData Make(SPCDetermineDataType sourcedata)
        {
            IList<double> items = sourcedata.Items;
            var param = sourcedata.Param;
            BasicSeriesData result = new BasicSeriesData();
            SPCDetermineMethod method = new SPCDetermineMethod(sourcedata.UCL, sourcedata.LCL, sourcedata.Standard, sourcedata.Commands);
            result.Y.Add(sourcedata.Standard + sourcedata.UCL);
            result.Y.Add(sourcedata.Standard);
            result.Y.Add(sourcedata.Standard + sourcedata.LCL);
            result.X.Add(null);
            result.X.Add(null);
            result.X.Add(null);
            List<SPCCommandbase> excuteresult;
            double x = 0, y = 0;
            for (int i = 0; i < items.Count; i++)
            {
                y = items.ElementAt(i);
                excuteresult = method.Excute(y);
                x++;
                result.X.Add(x.ToString());
                result.Y.Add(y);
                foreach (var command in excuteresult)
                {
                    result.X.Add(null);
                    result.Y.Add(command.ID);
                }

            }
            return result;
        }
    }
}
