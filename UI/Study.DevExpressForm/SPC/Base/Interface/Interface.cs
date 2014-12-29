using Study.DevExpressForm.SPC.Base.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Base.Interface
{
    public interface IDrawBoard<ChartType>
    {
        ChartType GetChart();
        System.Windows.Forms.Control Parent { get; }
        bool CheckCanRemove();
    }

    public interface ISeriesMaker<SourceDataType>
    {
        BasicSeriesData Make(SourceDataType sourceData);
    }

    public interface ISeriesDrawer<DrawBoardType> : IDisposable
    {
        void Draw(BasicSeriesData data, System.Drawing.Color color, DrawBoardType drawBoard);
        void Clear();
    }
}
