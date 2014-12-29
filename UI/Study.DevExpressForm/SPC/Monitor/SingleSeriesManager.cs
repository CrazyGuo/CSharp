using Study.DevExpressForm.SPC.Base.Interface;
using Study.DevExpressForm.SPC.Base.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor
{
    public abstract class SingleSeriesManager<SourceDataType, DrawBoardType>
    {
        protected ISeriesMaker<SourceDataType> SeriesMaker;
        protected ISeriesDrawer<DrawBoardType> SeriesDrawer;
        private BasicSeriesData SeriesData;
        public IDrawBoard<DrawBoardType> DrawBoard;
        protected abstract void Init();
        public SingleSeriesManager()
        {
            Init();
        }
        public void InitData(SourceDataType sourceData)
        {
            this.SeriesData = this.SeriesMaker.Make(sourceData);
        }
        public void DrawSeries(System.Drawing.Color color)
        {
            this.SeriesDrawer.Draw(SeriesData, color, this.DrawBoard.GetChart());
        }
        public void RemoveSeries()
        {
            this.SeriesDrawer.Clear();
        }
    }
}
