namespace Study.DevExpressForm
{
    partial class SPCForm
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
            this.determineControl1 = new Study.DevExpressForm.SPC.Monitor.DetermineControl();
            this.SuspendLayout();
            // 
            // determineControl1
            // 
            this.determineControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.determineControl1.Location = new System.Drawing.Point(0, 0);
            this.determineControl1.Name = "determineControl1";
            this.determineControl1.Size = new System.Drawing.Size(757, 578);
            this.determineControl1.TabIndex = 0;
            // 
            // SPCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 578);
            this.Controls.Add(this.determineControl1);
            this.Name = "SPCForm";
            this.Text = "SPCForm";
            this.ResumeLayout(false);

        }

        #endregion

        private SPC.Monitor.DetermineControl determineControl1;


    }
}