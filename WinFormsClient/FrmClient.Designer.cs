namespace WinFormsClient
{
    partial class FrmClient
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(79, 45);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(242, 20);
            this.txtUserName.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "User name";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(213, 71);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(108, 23);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(79, 19);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(242, 20);
            this.txtUrl.TabIndex = 6;
            this.txtUrl.Text = "http://localhost:8080/signalr";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Url";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(79, 71);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(108, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtLog);
            this.groupBox3.Location = new System.Drawing.Point(12, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(403, 297);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(6, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(391, 272);
            this.txtLog.TabIndex = 5;
            // 
            // FrmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 431);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmClient";
            this.Text = "Client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtLog;
    }
}

