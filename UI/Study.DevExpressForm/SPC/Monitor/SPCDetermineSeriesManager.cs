using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraCharts;

namespace Study.DevExpressForm.SPC.Monitor
{
    public class SPCDetermineSeriesManager : SingleSeriesManager<SPCDetermineDataType, ChartControl>
    {
        protected override void Init()
        {
            this.SeriesMaker = new SPCDetermineSeriesMaker();
            this.SeriesDrawer = new SPCDetermineSeriesDrawer();
        }
    }
}
