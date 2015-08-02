using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel;
using System.Threading;

namespace TestCodes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Utility utility = new Utility();
            try
            {
                utility.MergedCells("C:\\1.xls", "C:\\2.xls");
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            MessageBox.Show("Done");
        }

        [LoggingAspect(BusinessName = "我的AOP测试")]
        private void AOPTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("业务执行时间:"+DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
            Thread.Sleep(3000);
        }
    }
}
