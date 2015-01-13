using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IBatisQueryGenerator
{
    public partial class GenerateSQLForm : Form
    {
        private GridClass gridClazz;
        private EntConnection conn;

        public GenerateSQLForm()
        {
            InitializeComponent();
            gridClazz = new GridClass();
            listBoxQueryKindCd.SelectedItem = "SELECT";
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //delete 키 입력시
                if (e.KeyCode == Keys.Delete)
                {
                    gridClazz.setSelectedCellsValue(dgvTable, null);
                }

                
                //ctrl+v 키 입력시
                if (e.Control && e.KeyCode == Keys.V)
                {
                    gridClazz.setGridMultyRow(dgvTable, Clipboard.GetText());
                }

            }
            catch (MyException my)
            {
                my.showMessage();
            }


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //Table Changed
        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvTable.DataSource = conn.getColumnList(cboTable.SelectedItem.ToString(), chkUpperYn.Checked);
        }

        //DataBase Changed
        private void cboSchema_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTable.DataSource = conn.getTableList(cboDbName.SelectedItem.ToString());
        }

        private void chkComment_CheckedChanged(object sender, EventArgs e)
        {
            getQuery();
        }

        private void chkUpperYn_CheckedChanged(object sender, EventArgs e)
        {
            dgvTable.DataSource = conn.getColumnList(cboTable.SelectedItem.ToString(), chkUpperYn.Checked);
        }

        private void getQuery()
        {
            string sql=listBoxQueryKindCd.SelectedItem.ToString();
            SqlOperationType type=SqlOperationType.Select;
            if(sql.Equals("select",StringComparison.OrdinalIgnoreCase))
            {
                type = SqlOperationType.Select;
            }
            else if (sql.Equals("insert", StringComparison.OrdinalIgnoreCase))
            {
                type = SqlOperationType.Insert;
            }
            else if (sql.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                type = SqlOperationType.Update;
            }
            else if (sql.Equals("delete", StringComparison.OrdinalIgnoreCase))
            {
                type = SqlOperationType.Delete;
            }
            try
            {
                String result = gridClazz.getQueryText(
                    (DataTable)dgvTable.DataSource,
                    cboDbName.SelectedItem.ToString() + ".dbo." + cboTable.SelectedItem.ToString(),
                    txtTableAlias.Text,
                    int.Parse(txtNColEng.Text),
                    int.Parse(txtNColKor.Text),
                    type,
                    chkComment.Checked);

                rtbResultSql.Text = result;
            }

            catch (MyException my)
            {
                my.showMessage();
            }


        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (conn != null)
            {
                conn.close();
            }

            this.conn = new EntConnection(new ConnectionMSSQL2008(txtIp.Text, txtId.Text, txtPw.Text));     // MS-SQL 2008
            cboDbName.DataSource = conn.getSchemaList();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conn != null)
            {
                conn.close();
            }
            
        }

        private void btnGenerateSql_Click(object sender, EventArgs e)
        {
            getQuery();
        }
    }
}