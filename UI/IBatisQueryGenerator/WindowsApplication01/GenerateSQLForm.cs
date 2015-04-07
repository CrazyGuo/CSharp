using IBatisQueryGenerator.EasyUI;
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
            string name = cboTable.SelectedItem.ToString();
            string sqlId = string.Empty;
            string parameterClass = name + "Query";
            string resultClass = name + "Dto";
            StringBuilder bulider = new StringBuilder();
            string nodeAppend = "";
            string sql=listBoxQueryKindCd.SelectedItem.ToString();
            SqlOperationType type=SqlOperationType.Select;
            if(sql.Equals("select",StringComparison.OrdinalIgnoreCase))
            {
                sqlId = "q" + name;
                string select="<select id=\""+sqlId+"\" parameterClass=\""+parameterClass+"\" resultClass=\""+resultClass+"\"> ";                
                bulider.Append(select);
                bulider.Append("\r\n");
                nodeAppend = @"</select>";
                type = SqlOperationType.Select;
            }
            else if (sql.Equals("insert", StringComparison.OrdinalIgnoreCase))
            {
                sqlId = "i" + name;
                string insert = "<insert id=\""+sqlId+"\" parameterClass=\""+parameterClass+"\" resultClass=\""+resultClass+"\"> ";
                bulider.Append(insert);
                bulider.Append("\r\n");
                nodeAppend = @"</insert>";
                type = SqlOperationType.Insert;
            }
            else if (sql.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                sqlId = "u" + name;
                string update = "<update id=\""+sqlId+"\" parameterClass=\""+parameterClass+"\" resultClass=\""+resultClass+"\"> ";
                bulider.Append(update);
                bulider.Append("\r\n");
                nodeAppend = @"</update>";
                type = SqlOperationType.Update;
            }
            else if (sql.Equals("delete", StringComparison.OrdinalIgnoreCase))
            {
                sqlId = "d" + name;
                string delete = "<delete id=\""+sqlId+"\" parameterClass=\""+parameterClass+"\" resultClass=\""+resultClass+"\"> ";
                bulider.Append(delete);
                bulider.Append("\r\n");
                nodeAppend = @"</delete>";
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
                bulider.Append(result);
                bulider.Append("\r\n");
                bulider.Append(nodeAppend);
                rtbResultSql.Text = bulider.ToString();
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

        private void BtnGenerateEntity_Click(object sender, EventArgs e)
        {
            GenerateEntity ge = new GenerateEntity(conn);
            string entity = ge.BeginGenerateEntityClass(this.cboDbName.Text, this.cboTable.Text, this.cboTable.Text);
            this.rtbEntity.Text = entity;
            this.rtbQuery.Text = ge.BeginGenerateEntityQuery(this.cboDbName.Text, this.cboTable.Text);
            this.rtbEntityDto.Text = ge.BeginGenerateEntityDto(this.cboDbName.Text, this.cboTable.Text);
        }

        private void btnGenerateEasyUI_Click(object sender, EventArgs e)
        {
            GenerateServiceCode gs = new GenerateServiceCode();
            gs.Name = cboTable.SelectedItem.ToString();
            this.rtbIservice.Text = gs.IServiceCode();
            this.rtbService.Text = gs.ServiceCode();
            this.rtbController.Text = gs.ControllerCode();
        }

        private void btnHtml_Click(object sender, EventArgs e)
        {
            GenerateHtml html = new GenerateHtml();
            string name = cboTable.SelectedItem.ToString();
            html.Name = name;
            html.Url = txtUrl.Text.Trim();
            rtbIndex.Text = html.Index();
            rtbQueryForm.Text = html.QueryForm();
            txtAddForm.Text = html.AddForm();
            txtUpdateForm.Text = html.UpdateForm();
        }
    }
}