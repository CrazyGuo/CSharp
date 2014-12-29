using Study.DevExpressForm.SPC.Base.Interface;
using Study.DevExpressForm.SPC.Base.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor
{
    public class SPCDetermineSeriesDrawer : ISeriesDrawer<DevExpress.XtraCharts.ChartControl>
    {
        private DevExpress.XtraCharts.Series[] Series;
        private DevExpress.XtraCharts.ChartControl DrawBoard;
        public void Draw(BasicSeriesData data, System.Drawing.Color color, DevExpress.XtraCharts.ChartControl drawBoard)
        {
            DrawBoard = drawBoard;
            Series = new DevExpress.XtraCharts.Series[2];
            string pre = "";
            for (int i = 0; i < 2; i++)
            {
                Series[i] = new DevExpress.XtraCharts.Series();
                Series[i].View = DrawBoard.Series[i].View.Clone() as DevExpress.XtraCharts.SeriesViewBase;
                Series[i].CrosshairLabelPattern = DrawBoard.Series[i].CrosshairLabelPattern;
                Series[i].View.Color = color;
                Series[i].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                DrawBoard.Series.Add(Series[i]);
                Series[i].Points.BeginUpdate();
                Series[i].Points.Clear();
            }
            (drawBoard.Diagram as DevExpress.XtraCharts.XYDiagram2D).GetAllAxesY()[0].ConstantLines.Add(new DevExpress.XtraCharts.ConstantLine("UCL", data.Y[0]) { Color = color });
            (drawBoard.Diagram as DevExpress.XtraCharts.XYDiagram2D).GetAllAxesY()[0].ConstantLines.Add(new DevExpress.XtraCharts.ConstantLine("STD", data.Y[1]) { Color = color });
            (drawBoard.Diagram as DevExpress.XtraCharts.XYDiagram2D).GetAllAxesY()[0].ConstantLines.Add(new DevExpress.XtraCharts.ConstantLine("LCL", data.Y[2]) { Color = color });

            for (int i = 3; i < data.X.Count; i++)
            {
                var x = data.X[i];
                if (x == null)
                {
                    Series[1].Points.Add(new DevExpress.XtraCharts.SeriesPoint(pre, data.Y[i]));
                }
                else
                {
                    Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(x, data.Y[i]));
                    pre = x;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                Series[i].Points.EndUpdate();
            }
        }

        public void Clear()
        {
            if (Series != null && DrawBoard != null)
            {
                for (int i = Series.Length - 1; i >= 0; i--)
                    this.DrawBoard.Series.Remove(Series[i]);
            }
        }
        public void Dispose()
        {
            for (int i = Series.Length - 1; i >= 0; i--)
                Series[i].Dispose();
        }
    }
}
