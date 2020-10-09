namespace WinFormsServer
{
    partial class frmReadFile
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
            this.rtbFileLines = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbFileLines
            // 
            this.rtbFileLines.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.rtbFileLines.Location = new System.Drawing.Point(12, 12);
            this.rtbFileLines.Name = "rtbFileLines";
            this.rtbFileLines.Size = new System.Drawing.Size(767, 426);
            this.rtbFileLines.TabIndex = 0;
            this.rtbFileLines.Text = "";
            // 
            // frmReadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtbFileLines);
            this.Name = "frmReadFile";
            this.Text = "frmReadFile";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox rtbFileLines;
    }
}