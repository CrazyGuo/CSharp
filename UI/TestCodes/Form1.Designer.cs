namespace TestCodes
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExcel = new System.Windows.Forms.Button();
            this.AOPTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(34, 53);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(164, 34);
            this.btnExcel.TabIndex = 0;
            this.btnExcel.Text = "Excel合并单元格拆分";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // AOPTest
            // 
            this.AOPTest.Location = new System.Drawing.Point(34, 117);
            this.AOPTest.Name = "AOPTest";
            this.AOPTest.Size = new System.Drawing.Size(164, 40);
            this.AOPTest.TabIndex = 1;
            this.AOPTest.Text = "AOP功能测试";
            this.AOPTest.UseVisualStyleBackColor = true;
            this.AOPTest.Click += new System.EventHandler(this.AOPTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 398);
            this.Controls.Add(this.AOPTest);
            this.Controls.Add(this.btnExcel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button AOPTest;
    }
}

