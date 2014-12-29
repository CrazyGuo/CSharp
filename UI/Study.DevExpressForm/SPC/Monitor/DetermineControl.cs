using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using Study.DevExpressForm.SPC.Monitor.DrawBoards;
using Study.DevExpressForm.SPC.Base.Interface;
using Study.DevExpressForm.SPC.Monitor.Command;

namespace Study.DevExpressForm.SPC.Monitor
{
    public partial class DetermineControl : DevExpress.XtraEditors.XtraUserControl
    {
        private List<Type> DrawBoardTypes = new List<Type>();
        private PaletteEntry[] Colors;
        private ChartControl basicColorChart = new ChartControl();
        public DetermineControl()
        {
            InitializeComponent();
            InitDrawBoads();      
        }

        private List<IDrawBoard<ChartControl>> AddDrawBoards()
        {
            List<IDrawBoard<ChartControl>> drawBoards = new List<IDrawBoard<ChartControl>>();
            for (int i = 0; i < this.xtraTabControl1.TabPages.Count; i++)
            {
                if (xtraTabControl1.TabPages[i].Controls.Count > 0 && this.DrawBoardTypes.Count > i)
                {
                    var temp = Activator.CreateInstance(this.DrawBoardTypes[i], null);
                    this.xtraTabControl1.TabPages[i].Controls[0].Controls.Add(temp as UserControl);
                    drawBoards.Add(temp as IDrawBoard<ChartControl>);
                }
            }
            return drawBoards;
        }

        private void InitDrawBoads()
        {
            this.DrawBoardTypes.Add(typeof(SPCDetermineDrawBoard));
            Colors = this.basicColorChart.GetPaletteEntries(50);
        }

        public void DrawSPC(IList<double> items, double ucl, double lcl, double standard, List<SPCCommandbase> commands)
        {
            SPCDetermineData sPCDetermineData = new SPCDetermineData(items, "", ucl, lcl, standard, commands, this.Colors[0].Color, this.AddDrawBoards());
            sPCDetermineData.DrawSerieses();
        }
    }
}
