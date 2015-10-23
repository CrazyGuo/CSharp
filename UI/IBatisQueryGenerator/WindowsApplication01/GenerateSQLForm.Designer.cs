namespace IBatisQueryGenerator
{
    partial class GenerateSQLForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.col_eng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_kor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.key = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.operationedFiled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rtbResultSql = new System.Windows.Forms.RichTextBox();
            this.btnGenerateSql = new System.Windows.Forms.Button();
            this.listBoxQueryKindCd = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNColEng = new System.Windows.Forms.TextBox();
            this.txtNColKor = new System.Windows.Forms.TextBox();
            this.txtTableAlias = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.cboDbName = new System.Windows.Forms.ComboBox();
            this.chkComment = new System.Windows.Forms.CheckBox();
            this.chkUpperYn = new System.Windows.Forms.CheckBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.BtnGenerateEntity = new System.Windows.Forms.Button();
            this.btnGenerateEasyUI = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SQL = new System.Windows.Forms.TabPage();
            this.Entity = new System.Windows.Forms.TabPage();
            this.rtbEntity = new System.Windows.Forms.RichTextBox();
            this.Query = new System.Windows.Forms.TabPage();
            this.rtbQuery = new System.Windows.Forms.RichTextBox();
            this.Dto = new System.Windows.Forms.TabPage();
            this.rtbEntityDto = new System.Windows.Forms.RichTextBox();
            this.IService接口 = new System.Windows.Forms.TabPage();
            this.rtbIservice = new System.Windows.Forms.RichTextBox();
            this.Service实现 = new System.Windows.Forms.TabPage();
            this.rtbService = new System.Windows.Forms.RichTextBox();
            this.Controller = new System.Windows.Forms.TabPage();
            this.rtbController = new System.Windows.Forms.RichTextBox();
            this.Indexcshtml = new System.Windows.Forms.TabPage();
            this.rtbIndex = new System.Windows.Forms.RichTextBox();
            this.QueryForm = new System.Windows.Forms.TabPage();
            this.rtbQueryForm = new System.Windows.Forms.RichTextBox();
            this.AddForm = new System.Windows.Forms.TabPage();
            this.txtAddForm = new System.Windows.Forms.RichTextBox();
            this.UpdateForm = new System.Windows.Forms.TabPage();
            this.txtUpdateForm = new System.Windows.Forms.RichTextBox();
            this.btnHtml = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtServiceNameSpace = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtClassNameSpace = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.BtnSaveFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SQL.SuspendLayout();
            this.Entity.SuspendLayout();
            this.Query.SuspendLayout();
            this.Dto.SuspendLayout();
            this.IService接口.SuspendLayout();
            this.Service实现.SuspendLayout();
            this.Controller.SuspendLayout();
            this.Indexcshtml.SuspendLayout();
            this.QueryForm.SuspendLayout();
            this.AddForm.SuspendLayout();
            this.UpdateForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.AllowUserToDeleteRows = false;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_eng,
            this.col_kor,
            this.key,
            this.operationedFiled});
            this.dgvTable.Location = new System.Drawing.Point(12, 48);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.RowTemplate.Height = 23;
            this.dgvTable.Size = new System.Drawing.Size(535, 288);
            this.dgvTable.TabIndex = 1;
            this.dgvTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // col_eng
            // 
            this.col_eng.DataPropertyName = "col_eng";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_eng.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_eng.HeaderText = "字段";
            this.col_eng.Name = "col_eng";
            this.col_eng.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // col_kor
            // 
            this.col_kor.DataPropertyName = "col_kor";
            this.col_kor.HeaderText = "注释内容";
            this.col_kor.Name = "col_kor";
            // 
            // key
            // 
            this.key.DataPropertyName = "pk";
            this.key.HeaderText = "WHERE筛选字段";
            this.key.Name = "key";
            this.key.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.key.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.key.Width = 150;
            // 
            // operationedFiled
            // 
            this.operationedFiled.DataPropertyName = "operationedFiled";
            this.operationedFiled.HeaderText = "操作字段";
            this.operationedFiled.Name = "operationedFiled";
            this.operationedFiled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.operationedFiled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // rtbResultSql
            // 
            this.rtbResultSql.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbResultSql.Location = new System.Drawing.Point(6, 1);
            this.rtbResultSql.Name = "rtbResultSql";
            this.rtbResultSql.Size = new System.Drawing.Size(626, 288);
            this.rtbResultSql.TabIndex = 3;
            this.rtbResultSql.Text = "";
            // 
            // btnGenerateSql
            // 
            this.btnGenerateSql.Location = new System.Drawing.Point(15, 422);
            this.btnGenerateSql.Name = "btnGenerateSql";
            this.btnGenerateSql.Size = new System.Drawing.Size(131, 32);
            this.btnGenerateSql.TabIndex = 4;
            this.btnGenerateSql.Text = "生成SQL";
            this.btnGenerateSql.UseVisualStyleBackColor = true;
            this.btnGenerateSql.Click += new System.EventHandler(this.btnGenerateSql_Click);
            // 
            // listBoxQueryKindCd
            // 
            this.listBoxQueryKindCd.FormattingEnabled = true;
            this.listBoxQueryKindCd.Items.AddRange(new object[] {
            "SELECT",
            "INSERT",
            "UPDATE",
            "DELETE"});
            this.listBoxQueryKindCd.Location = new System.Drawing.Point(73, 342);
            this.listBoxQueryKindCd.Name = "listBoxQueryKindCd";
            this.listBoxQueryKindCd.Size = new System.Drawing.Size(73, 69);
            this.listBoxQueryKindCd.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(965, 383);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "字段最大长度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(965, 413);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "注释长度";
            // 
            // txtNColEng
            // 
            this.txtNColEng.Location = new System.Drawing.Point(1056, 379);
            this.txtNColEng.MaxLength = 2;
            this.txtNColEng.Name = "txtNColEng";
            this.txtNColEng.Size = new System.Drawing.Size(40, 20);
            this.txtNColEng.TabIndex = 8;
            this.txtNColEng.Text = "30";
            // 
            // txtNColKor
            // 
            this.txtNColKor.Location = new System.Drawing.Point(1056, 406);
            this.txtNColKor.MaxLength = 2;
            this.txtNColKor.Name = "txtNColKor";
            this.txtNColKor.Size = new System.Drawing.Size(40, 20);
            this.txtNColKor.TabIndex = 9;
            this.txtNColKor.Text = "30";
            // 
            // txtTableAlias
            // 
            this.txtTableAlias.Location = new System.Drawing.Point(460, 12);
            this.txtTableAlias.MaxLength = 5;
            this.txtTableAlias.Name = "txtTableAlias";
            this.txtTableAlias.Size = new System.Drawing.Size(62, 20);
            this.txtTableAlias.TabIndex = 10;
            this.txtTableAlias.Text = "T";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 358);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "SQL Type";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(581, 12);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(73, 20);
            this.txtIp.TabIndex = 14;
            this.txtIp.Text = "127.0.0.1";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(707, 13);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(71, 20);
            this.txtId.TabIndex = 15;
            this.txtId.Text = "sa";
            // 
            // txtPw
            // 
            this.txtPw.Location = new System.Drawing.Point(835, 12);
            this.txtPw.Name = "txtPw";
            this.txtPw.Size = new System.Drawing.Size(89, 20);
            this.txtPw.TabIndex = 16;
            this.txtPw.Text = "Abcd1234";
            // 
            // cboTable
            // 
            this.cboTable.FormattingEnabled = true;
            this.cboTable.ItemHeight = 13;
            this.cboTable.Location = new System.Drawing.Point(239, 11);
            this.cboTable.MaxDropDownItems = 30;
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(143, 21);
            this.cboTable.TabIndex = 17;
            this.cboTable.SelectedIndexChanged += new System.EventHandler(this.cboTable_SelectedIndexChanged);
            // 
            // cboDbName
            // 
            this.cboDbName.FormattingEnabled = true;
            this.cboDbName.Location = new System.Drawing.Point(73, 11);
            this.cboDbName.Name = "cboDbName";
            this.cboDbName.Size = new System.Drawing.Size(104, 21);
            this.cboDbName.TabIndex = 18;
            this.cboDbName.SelectedIndexChanged += new System.EventHandler(this.cboSchema_SelectedIndexChanged);
            // 
            // chkComment
            // 
            this.chkComment.AutoSize = true;
            this.chkComment.Checked = true;
            this.chkComment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkComment.Location = new System.Drawing.Point(1102, 379);
            this.chkComment.Name = "chkComment";
            this.chkComment.Size = new System.Drawing.Size(110, 17);
            this.chkComment.TabIndex = 19;
            this.chkComment.Text = "不显示注释内容";
            this.chkComment.UseVisualStyleBackColor = true;
            this.chkComment.CheckedChanged += new System.EventHandler(this.chkComment_CheckedChanged);
            // 
            // chkUpperYn
            // 
            this.chkUpperYn.AutoSize = true;
            this.chkUpperYn.Location = new System.Drawing.Point(1102, 408);
            this.chkUpperYn.Name = "chkUpperYn";
            this.chkUpperYn.Size = new System.Drawing.Size(50, 17);
            this.chkUpperYn.TabIndex = 20;
            this.chkUpperYn.Text = "大写";
            this.chkUpperYn.UseVisualStyleBackColor = true;
            this.chkUpperYn.CheckedChanged += new System.EventHandler(this.chkUpperYn_CheckedChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(968, 9);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(64, 25);
            this.btnConnect.TabIndex = 21;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "数据库";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "表名：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(399, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "表别名：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(546, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "IP：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(660, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "帐号：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(784, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "密码：";
            // 
            // BtnGenerateEntity
            // 
            this.BtnGenerateEntity.Location = new System.Drawing.Point(165, 347);
            this.BtnGenerateEntity.Name = "BtnGenerateEntity";
            this.BtnGenerateEntity.Size = new System.Drawing.Size(113, 34);
            this.BtnGenerateEntity.TabIndex = 28;
            this.BtnGenerateEntity.Text = "生成参数/实体类";
            this.BtnGenerateEntity.UseVisualStyleBackColor = true;
            this.BtnGenerateEntity.Click += new System.EventHandler(this.BtnGenerateEntity_Click);
            // 
            // btnGenerateEasyUI
            // 
            this.btnGenerateEasyUI.Location = new System.Drawing.Point(165, 387);
            this.btnGenerateEasyUI.Name = "btnGenerateEasyUI";
            this.btnGenerateEasyUI.Size = new System.Drawing.Size(113, 33);
            this.btnGenerateEasyUI.TabIndex = 29;
            this.btnGenerateEasyUI.Text = "生成Service";
            this.btnGenerateEasyUI.UseVisualStyleBackColor = true;
            this.btnGenerateEasyUI.Click += new System.EventHandler(this.btnGenerateEasyUI_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SQL);
            this.tabControl1.Controls.Add(this.Entity);
            this.tabControl1.Controls.Add(this.Query);
            this.tabControl1.Controls.Add(this.Dto);
            this.tabControl1.Controls.Add(this.IService接口);
            this.tabControl1.Controls.Add(this.Service实现);
            this.tabControl1.Controls.Add(this.Controller);
            this.tabControl1.Controls.Add(this.Indexcshtml);
            this.tabControl1.Controls.Add(this.QueryForm);
            this.tabControl1.Controls.Add(this.AddForm);
            this.tabControl1.Controls.Add(this.UpdateForm);
            this.tabControl1.Location = new System.Drawing.Point(581, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(643, 321);
            this.tabControl1.TabIndex = 30;
            // 
            // SQL
            // 
            this.SQL.Controls.Add(this.rtbResultSql);
            this.SQL.Location = new System.Drawing.Point(4, 22);
            this.SQL.Name = "SQL";
            this.SQL.Padding = new System.Windows.Forms.Padding(3);
            this.SQL.Size = new System.Drawing.Size(635, 295);
            this.SQL.TabIndex = 0;
            this.SQL.Text = "SQL";
            this.SQL.UseVisualStyleBackColor = true;
            // 
            // Entity
            // 
            this.Entity.BackColor = System.Drawing.Color.Red;
            this.Entity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Entity.Controls.Add(this.rtbEntity);
            this.Entity.ForeColor = System.Drawing.Color.Red;
            this.Entity.Location = new System.Drawing.Point(4, 22);
            this.Entity.Name = "Entity";
            this.Entity.Size = new System.Drawing.Size(635, 295);
            this.Entity.TabIndex = 4;
            this.Entity.Text = "Entity";
            this.Entity.UseVisualStyleBackColor = true;
            // 
            // rtbEntity
            // 
            this.rtbEntity.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbEntity.Location = new System.Drawing.Point(4, 3);
            this.rtbEntity.Name = "rtbEntity";
            this.rtbEntity.Size = new System.Drawing.Size(626, 288);
            this.rtbEntity.TabIndex = 4;
            this.rtbEntity.Text = "";
            // 
            // Query
            // 
            this.Query.BackColor = System.Drawing.Color.Red;
            this.Query.Controls.Add(this.rtbQuery);
            this.Query.Location = new System.Drawing.Point(4, 22);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(635, 295);
            this.Query.TabIndex = 5;
            this.Query.Text = "Query";
            // 
            // rtbQuery
            // 
            this.rtbQuery.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbQuery.Location = new System.Drawing.Point(4, 3);
            this.rtbQuery.Name = "rtbQuery";
            this.rtbQuery.Size = new System.Drawing.Size(626, 288);
            this.rtbQuery.TabIndex = 4;
            this.rtbQuery.Text = "";
            // 
            // Dto
            // 
            this.Dto.BackColor = System.Drawing.Color.Red;
            this.Dto.Controls.Add(this.rtbEntityDto);
            this.Dto.Location = new System.Drawing.Point(4, 22);
            this.Dto.Name = "Dto";
            this.Dto.Size = new System.Drawing.Size(635, 295);
            this.Dto.TabIndex = 6;
            this.Dto.Text = "Dto";
            // 
            // rtbEntityDto
            // 
            this.rtbEntityDto.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbEntityDto.Location = new System.Drawing.Point(4, 3);
            this.rtbEntityDto.Name = "rtbEntityDto";
            this.rtbEntityDto.Size = new System.Drawing.Size(626, 288);
            this.rtbEntityDto.TabIndex = 4;
            this.rtbEntityDto.Text = "";
            // 
            // IService接口
            // 
            this.IService接口.Controls.Add(this.rtbIservice);
            this.IService接口.Location = new System.Drawing.Point(4, 22);
            this.IService接口.Name = "IService接口";
            this.IService接口.Padding = new System.Windows.Forms.Padding(3);
            this.IService接口.Size = new System.Drawing.Size(635, 295);
            this.IService接口.TabIndex = 1;
            this.IService接口.Text = "IService接口";
            this.IService接口.UseVisualStyleBackColor = true;
            // 
            // rtbIservice
            // 
            this.rtbIservice.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbIservice.Location = new System.Drawing.Point(4, 3);
            this.rtbIservice.Name = "rtbIservice";
            this.rtbIservice.Size = new System.Drawing.Size(626, 288);
            this.rtbIservice.TabIndex = 4;
            this.rtbIservice.Text = "";
            // 
            // Service实现
            // 
            this.Service实现.Controls.Add(this.rtbService);
            this.Service实现.Location = new System.Drawing.Point(4, 22);
            this.Service实现.Name = "Service实现";
            this.Service实现.Size = new System.Drawing.Size(635, 295);
            this.Service实现.TabIndex = 2;
            this.Service实现.Text = "Service实现";
            this.Service实现.UseVisualStyleBackColor = true;
            // 
            // rtbService
            // 
            this.rtbService.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbService.Location = new System.Drawing.Point(4, 3);
            this.rtbService.Name = "rtbService";
            this.rtbService.Size = new System.Drawing.Size(626, 288);
            this.rtbService.TabIndex = 5;
            this.rtbService.Text = "";
            // 
            // Controller
            // 
            this.Controller.Controls.Add(this.rtbController);
            this.Controller.Location = new System.Drawing.Point(4, 22);
            this.Controller.Name = "Controller";
            this.Controller.Size = new System.Drawing.Size(635, 295);
            this.Controller.TabIndex = 3;
            this.Controller.Text = "Controller";
            this.Controller.UseVisualStyleBackColor = true;
            // 
            // rtbController
            // 
            this.rtbController.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbController.Location = new System.Drawing.Point(4, 3);
            this.rtbController.Name = "rtbController";
            this.rtbController.Size = new System.Drawing.Size(626, 288);
            this.rtbController.TabIndex = 6;
            this.rtbController.Text = "";
            // 
            // Indexcshtml
            // 
            this.Indexcshtml.Controls.Add(this.rtbIndex);
            this.Indexcshtml.Location = new System.Drawing.Point(4, 22);
            this.Indexcshtml.Name = "Indexcshtml";
            this.Indexcshtml.Size = new System.Drawing.Size(635, 295);
            this.Indexcshtml.TabIndex = 7;
            this.Indexcshtml.Text = "Index.cshtml";
            this.Indexcshtml.UseVisualStyleBackColor = true;
            // 
            // rtbIndex
            // 
            this.rtbIndex.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbIndex.Location = new System.Drawing.Point(4, 3);
            this.rtbIndex.Name = "rtbIndex";
            this.rtbIndex.Size = new System.Drawing.Size(626, 288);
            this.rtbIndex.TabIndex = 7;
            this.rtbIndex.Text = "";
            // 
            // QueryForm
            // 
            this.QueryForm.Controls.Add(this.rtbQueryForm);
            this.QueryForm.Location = new System.Drawing.Point(4, 22);
            this.QueryForm.Name = "QueryForm";
            this.QueryForm.Size = new System.Drawing.Size(635, 295);
            this.QueryForm.TabIndex = 8;
            this.QueryForm.Text = "QueryForm";
            this.QueryForm.UseVisualStyleBackColor = true;
            // 
            // rtbQueryForm
            // 
            this.rtbQueryForm.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbQueryForm.Location = new System.Drawing.Point(4, 3);
            this.rtbQueryForm.Name = "rtbQueryForm";
            this.rtbQueryForm.Size = new System.Drawing.Size(626, 288);
            this.rtbQueryForm.TabIndex = 4;
            this.rtbQueryForm.Text = "";
            // 
            // AddForm
            // 
            this.AddForm.Controls.Add(this.txtAddForm);
            this.AddForm.Location = new System.Drawing.Point(4, 22);
            this.AddForm.Name = "AddForm";
            this.AddForm.Padding = new System.Windows.Forms.Padding(3);
            this.AddForm.Size = new System.Drawing.Size(635, 295);
            this.AddForm.TabIndex = 9;
            this.AddForm.Text = "AddForm";
            this.AddForm.UseVisualStyleBackColor = true;
            // 
            // txtAddForm
            // 
            this.txtAddForm.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddForm.Location = new System.Drawing.Point(4, 3);
            this.txtAddForm.Name = "txtAddForm";
            this.txtAddForm.Size = new System.Drawing.Size(626, 288);
            this.txtAddForm.TabIndex = 5;
            this.txtAddForm.Text = "";
            // 
            // UpdateForm
            // 
            this.UpdateForm.Controls.Add(this.txtUpdateForm);
            this.UpdateForm.Location = new System.Drawing.Point(4, 22);
            this.UpdateForm.Name = "UpdateForm";
            this.UpdateForm.Padding = new System.Windows.Forms.Padding(3);
            this.UpdateForm.Size = new System.Drawing.Size(635, 295);
            this.UpdateForm.TabIndex = 10;
            this.UpdateForm.Text = "UpdateForm";
            this.UpdateForm.UseVisualStyleBackColor = true;
            // 
            // txtUpdateForm
            // 
            this.txtUpdateForm.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpdateForm.Location = new System.Drawing.Point(4, 3);
            this.txtUpdateForm.Name = "txtUpdateForm";
            this.txtUpdateForm.Size = new System.Drawing.Size(626, 288);
            this.txtUpdateForm.TabIndex = 6;
            this.txtUpdateForm.Text = "";
            // 
            // btnHtml
            // 
            this.btnHtml.Location = new System.Drawing.Point(165, 426);
            this.btnHtml.Name = "btnHtml";
            this.btnHtml.Size = new System.Drawing.Size(113, 29);
            this.btnHtml.TabIndex = 31;
            this.btnHtml.Text = "生成View页面";
            this.btnHtml.UseVisualStyleBackColor = true;
            this.btnHtml.Click += new System.EventHandler(this.btnHtml_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(388, 434);
            this.txtUrl.MaxLength = 100;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(207, 20);
            this.txtUrl.TabIndex = 32;
            this.txtUrl.Text = "AreaName/ControllerName";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(290, 438);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "MVC CRUD Url：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(290, 403);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "Service命名空间：";
            // 
            // txtServiceNameSpace
            // 
            this.txtServiceNameSpace.Location = new System.Drawing.Point(388, 400);
            this.txtServiceNameSpace.MaxLength = 100;
            this.txtServiceNameSpace.Name = "txtServiceNameSpace";
            this.txtServiceNameSpace.Size = new System.Drawing.Size(207, 20);
            this.txtServiceNameSpace.TabIndex = 35;
            this.txtServiceNameSpace.Text = "Study.Service";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(284, 358);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "类命名空间：";
            // 
            // txtClassNameSpace
            // 
            this.txtClassNameSpace.Location = new System.Drawing.Point(368, 355);
            this.txtClassNameSpace.MaxLength = 100;
            this.txtClassNameSpace.Name = "txtClassNameSpace";
            this.txtClassNameSpace.Size = new System.Drawing.Size(207, 20);
            this.txtClassNameSpace.TabIndex = 37;
            this.txtClassNameSpace.Text = "Study.Entity";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(637, 383);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "ProjectName：";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(722, 379);
            this.txtProjectName.MaxLength = 100;
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(207, 20);
            this.txtProjectName.TabIndex = 39;
            this.txtProjectName.Text = "Study.Webs.EasyUI";
            // 
            // BtnSaveFile
            // 
            this.BtnSaveFile.Location = new System.Drawing.Point(642, 422);
            this.BtnSaveFile.Name = "BtnSaveFile";
            this.BtnSaveFile.Size = new System.Drawing.Size(109, 29);
            this.BtnSaveFile.TabIndex = 40;
            this.BtnSaveFile.Text = "SaveToFiles";
            this.BtnSaveFile.UseVisualStyleBackColor = true;
            this.BtnSaveFile.Click += new System.EventHandler(this.BtnSaveFile_Click);
            // 
            // GenerateSQLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 470);
            this.Controls.Add(this.BtnSaveFile);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtClassNameSpace);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtServiceNameSpace);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnHtml);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnGenerateEasyUI);
            this.Controls.Add(this.BtnGenerateEntity);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.chkUpperYn);
            this.Controls.Add(this.chkComment);
            this.Controls.Add(this.cboDbName);
            this.Controls.Add(this.cboTable);
            this.Controls.Add(this.txtPw);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTableAlias);
            this.Controls.Add(this.txtNColKor);
            this.Controls.Add(this.txtNColEng);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxQueryKindCd);
            this.Controls.Add(this.btnGenerateSql);
            this.Controls.Add(this.dgvTable);
            this.Name = "GenerateSQLForm";
            this.Text = "IBatisQueryGenerator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.SQL.ResumeLayout(false);
            this.Entity.ResumeLayout(false);
            this.Query.ResumeLayout(false);
            this.Dto.ResumeLayout(false);
            this.IService接口.ResumeLayout(false);
            this.Service实现.ResumeLayout(false);
            this.Controller.ResumeLayout(false);
            this.Indexcshtml.ResumeLayout(false);
            this.QueryForm.ResumeLayout(false);
            this.AddForm.ResumeLayout(false);
            this.UpdateForm.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.RichTextBox rtbResultSql;
        private System.Windows.Forms.Button btnGenerateSql;
        private System.Windows.Forms.ListBox listBoxQueryKindCd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNColEng;
        private System.Windows.Forms.TextBox txtNColKor;
        private System.Windows.Forms.TextBox txtTableAlias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtPw;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.ComboBox cboDbName;
        private System.Windows.Forms.CheckBox chkComment;
        private System.Windows.Forms.CheckBox chkUpperYn;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_eng;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_kor;
        private System.Windows.Forms.DataGridViewCheckBoxColumn key;
        private System.Windows.Forms.DataGridViewCheckBoxColumn operationedFiled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BtnGenerateEntity;
        private System.Windows.Forms.Button btnGenerateEasyUI;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SQL;
        private System.Windows.Forms.TabPage IService接口;
        private System.Windows.Forms.TabPage Service实现;
        private System.Windows.Forms.TabPage Controller;
        private System.Windows.Forms.RichTextBox rtbIservice;
        private System.Windows.Forms.RichTextBox rtbService;
        private System.Windows.Forms.RichTextBox rtbController;
        private System.Windows.Forms.TabPage Entity;
        private System.Windows.Forms.RichTextBox rtbEntity;
        private System.Windows.Forms.TabPage Query;
        private System.Windows.Forms.TabPage Dto;
        private System.Windows.Forms.RichTextBox rtbQuery;
        private System.Windows.Forms.RichTextBox rtbEntityDto;
        private System.Windows.Forms.TabPage Indexcshtml;
        private System.Windows.Forms.RichTextBox rtbIndex;
        private System.Windows.Forms.Button btnHtml;
        private System.Windows.Forms.TabPage QueryForm;
        private System.Windows.Forms.RichTextBox rtbQueryForm;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage AddForm;
        private System.Windows.Forms.RichTextBox txtAddForm;
        private System.Windows.Forms.TabPage UpdateForm;
        private System.Windows.Forms.RichTextBox txtUpdateForm;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtServiceNameSpace;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtClassNameSpace;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Button BtnSaveFile;
    }
}

