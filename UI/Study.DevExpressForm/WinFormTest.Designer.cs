namespace Study.DevExpressForm
{
    partial class WinFormTest
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
            this.btn_bitmapDigital = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_bitmapDigital
            // 
            this.btn_bitmapDigital.Location = new System.Drawing.Point(25, 30);
            this.btn_bitmapDigital.Name = "btn_bitmapDigital";
            this.btn_bitmapDigital.Size = new System.Drawing.Size(116, 62);
            this.btn_bitmapDigital.TabIndex = 0;
            this.btn_bitmapDigital.Text = "生成数字图片";
            this.btn_bitmapDigital.UseVisualStyleBackColor = true;
            this.btn_bitmapDigital.Click += new System.EventHandler(this.btn_bitmapDigital_Click);
            // 
            // WinFormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 336);
            this.Controls.Add(this.btn_bitmapDigital);
            this.Name = "WinFormTest";
            this.Text = "WinFormTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_bitmapDigital;
    }
}