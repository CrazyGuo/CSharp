using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using Study.DevExpressForm.SPC.Base.Interface;
using Study.DevExpressForm.SPC.Monitor.Command;

namespace Study.DevExpressForm.SPC.Monitor.DrawBoards
{
    public partial class SPCDetermineDrawBoard : XtraUserControl, IDrawBoard<ChartControl>
    {
        public SPCDetermineDrawBoard()
        {
            InitializeComponent();
            var secondY = (this.chartControl1.Diagram as DevExpress.XtraCharts.XYDiagram2D).GetAllAxesY()[1];
            secondY.CustomLabels.Clear();
            var commands = SPCCommand.GetCommandArray();
            foreach (var command in commands)
            {
                secondY.CustomLabels.Add(new DevExpress.XtraCharts.CustomAxisLabel(command.Title, command.ID.ToString()));
            }
        }

        public ChartControl GetChart()
        {
            return this.chartControl1;
        }
        public bool CheckCanRemove()
        {
            if (this.GetChart().Series.Count == 2)
                return true;
            return false;
        }
    }
}
