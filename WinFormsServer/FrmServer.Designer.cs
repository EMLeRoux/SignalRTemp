namespace WinFormsServer
{
    partial class FrmServer
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpBroadcast = new System.Windows.Forms.GroupBox();
            this.cboMessage = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbClients = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblFilePathDesc = new System.Windows.Forms.Label();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.dgvFilesDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeViewFiles = new System.Windows.Forms.TreeView();
            this.Services = new System.Windows.Forms.TabPage();
            this.dgvServicesDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnServiceStart = new System.Windows.Forms.Button();
            this.btnServiceStop = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSqlConnectionStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpBroadcast.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilesDataGridView)).BeginInit();
            this.Services.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicesDataGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Url";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(44, 19);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(388, 20);
            this.txtUrl.TabIndex = 2;
            this.txtUrl.Text = "http://localhost:8080";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(8, 29);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(450, 138);
            this.txtLog.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLog);
            this.groupBox1.Location = new System.Drawing.Point(466, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(476, 190);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtUrl);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(10, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 52);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server";
            // 
            // grpBroadcast
            // 
            this.grpBroadcast.Controls.Add(this.cboMessage);
            this.grpBroadcast.Controls.Add(this.btnSend);
            this.grpBroadcast.Controls.Add(this.label2);
            this.grpBroadcast.Controls.Add(this.cmbClients);
            this.grpBroadcast.Enabled = false;
            this.grpBroadcast.Location = new System.Drawing.Point(10, 72);
            this.grpBroadcast.Name = "grpBroadcast";
            this.grpBroadcast.Size = new System.Drawing.Size(444, 130);
            this.grpBroadcast.TabIndex = 7;
            this.grpBroadcast.TabStop = false;
            this.grpBroadcast.Text = "Broadcast Message";
            // 
            // cboMessage
            // 
            this.cboMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMessage.FormattingEnabled = true;
            this.cboMessage.Items.AddRange(new object[] {
            "GetDirectoriesAndFiles",
            "GetWindowsServices",
            "CheckSqlConnection"});
            this.cboMessage.Location = new System.Drawing.Point(78, 100);
            this.cboMessage.Name = "cboMessage";
            this.cboMessage.Size = new System.Drawing.Size(157, 21);
            this.cboMessage.TabIndex = 10;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(301, 77);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(131, 41);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Message";
            // 
            // cmbClients
            // 
            this.cmbClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbClients.FormattingEnabled = true;
            this.cmbClients.Location = new System.Drawing.Point(14, 19);
            this.cmbClients.Name = "cmbClients";
            this.cmbClients.Size = new System.Drawing.Size(418, 60);
            this.cmbClients.TabIndex = 4;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.Services);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(10, 208);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(932, 482);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.lblFilePath);
            this.tabPage1.Controls.Add(this.lblFilePathDesc);
            this.tabPage1.Controls.Add(this.btnReadFile);
            this.tabPage1.Controls.Add(this.dgvFilesDataGridView);
            this.tabPage1.Controls.Add(this.treeViewFiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(924, 456);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Files";
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(71, 132);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(0, 13);
            this.lblFilePath.TabIndex = 12;
            // 
            // lblFilePathDesc
            // 
            this.lblFilePathDesc.AutoSize = true;
            this.lblFilePathDesc.Location = new System.Drawing.Point(11, 132);
            this.lblFilePathDesc.Name = "lblFilePathDesc";
            this.lblFilePathDesc.Size = new System.Drawing.Size(54, 13);
            this.lblFilePathDesc.TabIndex = 11;
            this.lblFilePathDesc.Text = "File Path :";
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(823, 126);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(87, 25);
            this.btnReadFile.TabIndex = 10;
            this.btnReadFile.Text = "Read File";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // dgvFilesDataGridView
            // 
            this.dgvFilesDataGridView.AllowUserToAddRows = false;
            this.dgvFilesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvFilesDataGridView.Location = new System.Drawing.Point(14, 192);
            this.dgvFilesDataGridView.Name = "dgvFilesDataGridView";
            this.dgvFilesDataGridView.ReadOnly = true;
            this.dgvFilesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFilesDataGridView.Size = new System.Drawing.Size(896, 150);
            this.dgvFilesDataGridView.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "File";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Date/Time";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 130;
            // 
            // treeViewFiles
            // 
            this.treeViewFiles.Location = new System.Drawing.Point(14, 16);
            this.treeViewFiles.Name = "treeViewFiles";
            this.treeViewFiles.Size = new System.Drawing.Size(896, 104);
            this.treeViewFiles.TabIndex = 0;
            this.treeViewFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFiles_AfterSelect);
            // 
            // Services
            // 
            this.Services.BackColor = System.Drawing.SystemColors.Control;
            this.Services.Controls.Add(this.btnServiceStop);
            this.Services.Controls.Add(this.btnServiceStart);
            this.Services.Controls.Add(this.dgvServicesDataGridView);
            this.Services.Location = new System.Drawing.Point(4, 22);
            this.Services.Name = "Services";
            this.Services.Padding = new System.Windows.Forms.Padding(3);
            this.Services.Size = new System.Drawing.Size(924, 456);
            this.Services.TabIndex = 1;
            this.Services.Text = "Services";
            // 
            // dgvServicesDataGridView
            // 
            this.dgvServicesDataGridView.AllowUserToAddRows = false;
            this.dgvServicesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServicesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvServicesDataGridView.Location = new System.Drawing.Point(14, 21);
            this.dgvServicesDataGridView.Name = "dgvServicesDataGridView";
            this.dgvServicesDataGridView.ReadOnly = true;
            this.dgvServicesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServicesDataGridView.Size = new System.Drawing.Size(896, 150);
            this.dgvServicesDataGridView.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Service";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 600;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Status";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // btnServiceStart
            // 
            this.btnServiceStart.Location = new System.Drawing.Point(651, 177);
            this.btnServiceStart.Name = "btnServiceStart";
            this.btnServiceStart.Size = new System.Drawing.Size(72, 26);
            this.btnServiceStart.TabIndex = 10;
            this.btnServiceStart.Text = "Start";
            this.btnServiceStart.UseVisualStyleBackColor = true;
            this.btnServiceStart.Click += new System.EventHandler(this.btnServiceStart_Click);
            // 
            // btnServiceStop
            // 
            this.btnServiceStop.Location = new System.Drawing.Point(740, 177);
            this.btnServiceStop.Name = "btnServiceStop";
            this.btnServiceStop.Size = new System.Drawing.Size(72, 26);
            this.btnServiceStop.TabIndex = 11;
            this.btnServiceStop.Text = "Stop";
            this.btnServiceStop.UseVisualStyleBackColor = true;
            this.btnServiceStop.Click += new System.EventHandler(this.btnServiceStop_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.lblSqlConnectionStatus);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(924, 456);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "SQL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "SQL Connection:";
            // 
            // lblSqlConnectionStatus
            // 
            this.lblSqlConnectionStatus.AutoSize = true;
            this.lblSqlConnectionStatus.Location = new System.Drawing.Point(105, 14);
            this.lblSqlConnectionStatus.Name = "lblSqlConnectionStatus";
            this.lblSqlConnectionStatus.Size = new System.Drawing.Size(88, 13);
            this.lblSqlConnectionStatus.TabIndex = 13;
            this.lblSqlConnectionStatus.Text = "SQL Connection:";
            // 
            // FrmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 702);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.grpBroadcast);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmServer";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmServer_FormClosing);
            this.Load += new System.EventHandler(this.FrmServer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpBroadcast.ResumeLayout(false);
            this.grpBroadcast.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilesDataGridView)).EndInit();
            this.Services.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicesDataGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grpBroadcast;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbClients;
        private System.Windows.Forms.ComboBox cboMessage;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView treeViewFiles;
        private System.Windows.Forms.TabPage Services;
        private System.Windows.Forms.DataGridView dgvFilesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblFilePathDesc;
        private System.Windows.Forms.DataGridView dgvServicesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button btnServiceStop;
        private System.Windows.Forms.Button btnServiceStart;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblSqlConnectionStatus;
        private System.Windows.Forms.Label label3;
    }
}

