using IBatisQueryGenerator.EasyUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
                //delete 虐 涝仿矫
                if (e.KeyCode == Keys.Delete)
                {
                    gridClazz.setSelectedCellsValue(dgvTable, null);
                }

                
                //ctrl+v 虐 涝仿矫
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
            string entity = name;
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
                string insert = "<insert id=\"" + sqlId + "\" parameterClass=\"" + entity + "\" "  +"> ";
                bulider.Append(insert);
                bulider.Append("\r\n");
                nodeAppend = @"</insert>";
                type = SqlOperationType.Insert;
            }
            else if (sql.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                sqlId = "u" + name;
                string update = "<update id=\""+sqlId+"\" parameterClass=\""+"string"+"\"> ";
                bulider.Append(update);
                bulider.Append("\r\n");
                nodeAppend = @"</update>";
                type = SqlOperationType.Update;
            }
            else if (sql.Equals("delete", StringComparison.OrdinalIgnoreCase))
            {
                sqlId = "d" + name;
                string delete = "<delete id=\""+sqlId+"\" parameterClass=\""+"string"+"\"> ";
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
            string space = this.txtClassNameSpace.Text.Trim();
            if(string.IsNullOrEmpty(space))
            {
                MessageBox.Show("参数/实体类命名空间未设定");
                return;
            }
            GenerateEntity ge = new GenerateEntity(conn);
            ge.ClassNameSpace = space;
            string entity = ge.BeginGenerateEntityClass(this.cboDbName.Text, this.cboTable.Text, this.cboTable.Text);
            this.rtbEntity.Text = entity;
            this.rtbQuery.Text = ge.BeginGenerateEntityQuery(this.cboDbName.Text, this.cboTable.Text);
            this.rtbEntityDto.Text = ge.BeginGenerateEntityDto(this.cboDbName.Text, this.cboTable.Text);
        }

        private void btnGenerateEasyUI_Click(object sender, EventArgs e)
        {
            string EntitySpace = this.txtClassNameSpace.Text.Trim();
            string ServiceSpace = this.txtServiceNameSpace.Text.Trim();
            string ProjectName = this.txtProjectName.Text.Trim();
            string AreaName = this.txtUrl.Text.Trim();
            if (AreaName.Contains("/"))
            {
               var content = AreaName.Split('/');
                if(content.Length == 2)
                {
                    AreaName = content[0];
                }
                else
                {
                    MessageBox.Show("URL 中未设置Area对应的子项目");
                    return;
                }
            }
            else
            {
                MessageBox.Show("URL 中未设置Area对应的子项目");
                return;
            }
            if (string.IsNullOrEmpty(EntitySpace))
            {
                MessageBox.Show("参数/实体类命名空间未设定");
                return;
            }
            if(string.IsNullOrEmpty(ServiceSpace))
            {
                MessageBox.Show("Service类命名空间未设定");
                return;
            }
            if (string.IsNullOrEmpty(ProjectName))
            {
                MessageBox.Show("project名未设定");
                return;
            }
            GenerateServiceCode gs = new GenerateServiceCode();
            gs.ProjectName = ProjectName;
            gs.EntityNameSpace = EntitySpace;
            gs.ServiceNameSpace = ServiceSpace;
            gs.AreaChildName = AreaName;
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
            //此处的url截取最后一个/后面的字符串,因为mvc路由设置了Area
            int lastIndex = txtUrl.Text.LastIndexOf('/');
            if(lastIndex>0)
            {
                html.Url = txtUrl.Text.Trim();
                html.QueryAndDownloadUrl = txtUrl.Text.Substring(lastIndex + 1);
            }
            else
            {
                html.Url = txtUrl.Text.Trim();
            }
            
            rtbIndex.Text = html.Index();
            rtbQueryForm.Text = html.QueryForm();
            txtAddForm.Text = html.AddForm();
            txtUpdateForm.Text = html.UpdateForm();
        }

        private void BtnSaveFile_Click(object sender, EventArgs e)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string[] dirs = { "html", "entity", "service" };
            try
            {
                foreach (var dirName in dirs)
                {
                    DirectoryInfo d = new DirectoryInfo(dir + "/" + dirName);
                    var files = d.GetFiles();
                    foreach (var file in files)
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
            string name = cboTable.SelectedItem.ToString();
            string entity = rtbEntity.Text;
            string entityDto = rtbEntityDto.Text;
            string entityQuery = rtbQuery.Text;

            string controller = rtbController.Text;
            string iservice = rtbIservice.Text;
            string service = rtbService.Text;

            string index = rtbIndex.Text;
            string queryForm = rtbQueryForm.Text;
            string addForm = txtAddForm.Text;
            string updateForm = txtUpdateForm.Text;

            string[] contents = { entity , entityDto ,entityQuery,
                                  controller , iservice , service,
                                  index , queryForm , addForm , updateForm
                                };
            string[] titles = { 
                                  dir+"/entity/" + name+".cs" , 
                                  dir+"/entity/" + name+"Dto.cs" , 
                                  dir+"/entity/" + name+"Query.cs",
                                  dir+"/service/" + name+"Controller.cs", 
                                  dir+"/service/" + "I"+name+"Service.cs" , 
                                  dir+"/service/" + name+"Service.cs",
                                  dir+"/html/" + "Index.cshtml" , 
                                  dir+"/html/" + "QueryForm.cshtml" , 
                                  dir+"/html/" + "AddForm.cshtml" , 
                                  dir+"/html/" + "UpdateForm.cshtml"
                              };
            for (int i = 0; i < contents.Length; i++)
            {
                LoggingHelper.Writelog(titles[i], contents[i]);
            }
        }
    }
}