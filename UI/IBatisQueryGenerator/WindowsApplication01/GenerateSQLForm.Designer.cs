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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_eng.DefaultCellStyle = dataGridViewCellStyle1;
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
            this.rtbResultSql.Location = new System.Drawing.Point(553, 48);
            this.rtbResultSql.Name = "rtbResultSql";
            this.rtbResultSql.Size = new System.Drawing.Size(479, 288);
            this.rtbResultSql.TabIndex = 3;
            this.rtbResultSql.Text = "";
            // 
            // btnGenerateSql
            // 
            this.btnGenerateSql.Location = new System.Drawing.Point(179, 375);
            this.btnGenerateSql.Name = "btnGenerateSql";
            this.btnGenerateSql.Size = new System.Drawing.Size(106, 42);
            this.btnGenerateSql.TabIndex = 4;
            this.btnGenerateSql.Text = "GenerateSQL";
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
            this.listBoxQueryKindCd.Location = new System.Drawing.Point(73, 367);
            this.listBoxQueryKindCd.Name = "listBoxQueryKindCd";
            this.listBoxQueryKindCd.Size = new System.Drawing.Size(73, 56);
            this.listBoxQueryKindCd.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(552, 380);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "字段最大长度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(552, 411);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "注释长度";
            // 
            // txtNColEng
            // 
            this.txtNColEng.Location = new System.Drawing.Point(699, 375);
            this.txtNColEng.MaxLength = 2;
            this.txtNColEng.Name = "txtNColEng";
            this.txtNColEng.Size = new System.Drawing.Size(40, 20);
            this.txtNColEng.TabIndex = 8;
            this.txtNColEng.Text = "30";
            // 
            // txtNColKor
            // 
            this.txtNColKor.Location = new System.Drawing.Point(699, 406);
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
            this.label4.Location = new System.Drawing.Point(12, 367);
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
            this.chkComment.Location = new System.Drawing.Point(827, 377);
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
            this.chkUpperYn.Location = new System.Drawing.Point(827, 406);
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
            this.BtnGenerateEntity.Location = new System.Drawing.Point(328, 375);
            this.BtnGenerateEntity.Name = "BtnGenerateEntity";
            this.BtnGenerateEntity.Size = new System.Drawing.Size(108, 42);
            this.BtnGenerateEntity.TabIndex = 28;
            this.BtnGenerateEntity.Text = "GenerateEntity";
            this.BtnGenerateEntity.UseVisualStyleBackColor = true;
            this.BtnGenerateEntity.Click += new System.EventHandler(this.BtnGenerateEntity_Click);
            // 
            // GenerateSQLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 472);
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
            this.Controls.Add(this.rtbResultSql);
            this.Controls.Add(this.dgvTable);
            this.Name = "GenerateSQLForm";
            this.Text = "IBatisQueryGenerator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
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
    }
}

