using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraCharts;
using Study.DevExpressForm.SPC.Base.Interface;
using Study.DevExpressForm.SPC.Monitor.Command;

namespace Study.DevExpressForm.SPC.Monitor
{
    public class SPCDetermineData
    {
        private List<SingleSeriesManager<SPCDetermineDataType, ChartControl>> SeriesManagers = new List<SingleSeriesManager<SPCDetermineDataType, ChartControl>>();
        public List<IDrawBoard<ChartControl>> DrawBoards;
        private SPCDetermineDataType SourceData;
        public string Name;
        public System.Drawing.Color SeriesColor;
        private void InitSerieses()
        {
            foreach (var seriesManager in SeriesManagers)
            {
                seriesManager.InitData(this.SourceData);
            }
        }
        public SPCDetermineData(IList<double> items, string param, double ucl, double lcl, double standard, List<SPCCommandbase> commands, System.Drawing.Color color, List<IDrawBoard<ChartControl>> drawBoards)
        {
            SourceData = new SPCDetermineDataType(items, param, ucl, lcl, standard, commands);
            this.Name = param + "_" + DateTime.Now.ToBinary();
            this.SeriesColor = color;
            this.DrawBoards = drawBoards;
            InitSeriesManagers();
            InitSerieses();
        }
        public void DrawSerieses()
        {
            foreach (var seriesManager in SeriesManagers)
            {
                seriesManager.DrawSeries(this.SeriesColor);
            }
        }
        public void ClearSerieses()
        {
            for (int i = SeriesManagers.Count - 1; i >= 0; i--)
            {
                var seriesManager = SeriesManagers[i];
                seriesManager.RemoveSeries();
                if (seriesManager.DrawBoard.CheckCanRemove())
                {
                    seriesManager.DrawBoard.Parent.Controls.Remove(seriesManager.DrawBoard as System.Windows.Forms.Control);
                }
            }
        }
        public override string ToString()
        {
            return this.Name.ToString();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        //在此添加新需求
        private void InitSeriesManagers()
        {
            this.SeriesManagers.Add(new SPCDetermineSeriesManager() { DrawBoard = this.DrawBoards[0] });
        }
    }
}
