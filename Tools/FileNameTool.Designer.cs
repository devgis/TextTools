namespace DEVGIS.Tools
{
    partial class FileNameTool
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
            this.btChangeExtName = new System.Windows.Forms.Button();
            this.cbExt = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btChangeExtName
            // 
            this.btChangeExtName.Location = new System.Drawing.Point(187, 29);
            this.btChangeExtName.Name = "btChangeExtName";
            this.btChangeExtName.Size = new System.Drawing.Size(75, 23);
            this.btChangeExtName.TabIndex = 0;
            this.btChangeExtName.Text = "修改文件扩展名";
            this.btChangeExtName.UseVisualStyleBackColor = true;
            this.btChangeExtName.Click += new System.EventHandler(this.btChangeExtName_Click);
            // 
            // cbExt
            // 
            this.cbExt.FormattingEnabled = true;
            this.cbExt.Items.AddRange(new object[] {
            ".txt",
            ".png",
            ".jpg",
            ".gif",
            ".xls",
            ".doc",
            ".xlsx",
            ".docx"});
            this.cbExt.Location = new System.Drawing.Point(60, 32);
            this.cbExt.Name = "cbExt";
            this.cbExt.Size = new System.Drawing.Size(121, 20);
            this.cbExt.TabIndex = 1;
            // 
            // FileNameTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 314);
            this.Controls.Add(this.cbExt);
            this.Controls.Add(this.btChangeExtName);
            this.Name = "FileNameTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文明名工具";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btChangeExtName;
        private System.Windows.Forms.ComboBox cbExt;
    }
}