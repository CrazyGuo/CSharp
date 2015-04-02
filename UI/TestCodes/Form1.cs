using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel;

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
    }
}
