namespace Study.DevExpressForm.SPC.Monitor
{
    partial class DetermineControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.multiControlsVerticalLayout1 = new Study.DevExpressForm.SPC.Base.Control.MultiControlsVerticalLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(684, 576);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.multiControlsVerticalLayout1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(678, 548);
            this.xtraTabPage1.Text = "SPC分析";
            // 
            // multiControlsVerticalLayout1
            // 
            this.multiControlsVerticalLayout1.AutoScroll = false;
            this.multiControlsVerticalLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiControlsVerticalLayout1.Location = new System.Drawing.Point(0, 0);
            this.multiControlsVerticalLayout1.Name = "multiControlsVerticalLayout1";
            this.multiControlsVerticalLayout1.Size = new System.Drawing.Size(678, 548);
            this.multiControlsVerticalLayout1.SizeChangeStep = 50;
            this.multiControlsVerticalLayout1.TabIndex = 0;
            // 
            // DetermineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "DetermineControl";
            this.Size = new System.Drawing.Size(684, 576);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Base.Control.MultiControlsVerticalLayout multiControlsVerticalLayout1;
    }
}
