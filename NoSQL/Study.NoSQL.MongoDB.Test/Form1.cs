using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Study.NoSQL.MongoDB;

namespace Study.NoSQL.MongoDB.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_mongodb_start_Click(object sender, EventArgs e)
        {
            MongoDBTest mongoDBTest = new MongoDBTest();
            mongoDBTest.TestCode();
        }
    }
}
