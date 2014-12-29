using Study.DevExpressForm.SPC.Monitor.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Study.Business;

namespace Study.DevExpressForm
{
    public partial class SPCForm : Form
    {
        public SPCForm()
        {
            InitializeComponent();
            Init();           
        }

        public void Init()
        {
            CfbSpindleTestRecordService service = new CfbSpindleTestRecordService();
            IList<double> items=service.GetRecords();

            List<SPCCommandbase> result = new List<SPCCommandbase>();
            //spcRule1 rule = new spcRule1();
            spcRule6 rule7 = new spcRule6();
            //result.Add(rule);
            result.Add(rule7);
            determineControl1.DrawSPC(items, 3000, -1000, 1000, result);
        }
    }
}
