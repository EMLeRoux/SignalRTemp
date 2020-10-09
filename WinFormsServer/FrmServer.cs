using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.AspNet.SignalR;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Data;

namespace WinFormsServer
{
    public partial class FrmServer : Form
    {
        private IDisposable _signalR;
        private BindingList<ClientItem> _clients = new BindingList<ClientItem>();

        private DataView pvtDataView;
        private DataTable pvtDataTable;
        private DataTable pvtFileDataTable;
        
        private string[] strFiles;

        private bool pvtblnLoadingTreeView = false;

        public FrmServer()
        {
            InitializeComponent();
            
            //return;
                                                                           
            bindListsToControls();

            //Register to static hub events
            SignalRHub.ClientConnected += SignalRHub_ClientConnected;
            SignalRHub.ClientDisconnected += SignalRHub_ClientDisconnected;
            SignalRHub.ClientNameChanged += SignalRHub_ClientNameChanged;

            SignalRHub.ReturnDirectoriesAndFiles += SignalRHub_ReturnDirectoriesAndFiles;
            SignalRHub.ReturnWindowsServices += SignalRHub_ReturnWindowsServices;
            SignalRHub.ReturnReadFile += SignalRHub_ReturnReadFile;
            SignalRHub.ReturnServiceStarted += SignalRHub_ReturnServiceStarted;
            SignalRHub.ReturnServiceStopped += SignalRHub_ReturnServiceStopped;
            SignalRHub.ReturnCheckSqlConnection += SignalRHub_ReturnCheckSqlConnection;
        }

        private void bindListsToControls()
        {
            //Clients list
            cmbClients.DisplayMember = "Name";
            cmbClients.ValueMember = "Id";
            cmbClients.DataSource = _clients;
        }

        private delegate void SignalRHub_ClientConnected_ThreadSafe(string clientId);
        private void SignalRHub_ClientConnected(string clientId)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new SignalRHub_ClientConnected_ThreadSafe(SignalRHub_ClientConnected), new object[] { clientId });
            }
            else
            {
                //Add client to our clients list
                _clients.Add(new ClientItem() { Id = clientId, Name = clientId });

                WriteToLog($"Client connected:{clientId}");
            }
        }

        private void SignalRHub_ClientDisconnected(string clientId)
        {
            //Remove client from the list
            var client = _clients.FirstOrDefault(x => x.Id == clientId);

            if (client != null)
            {
                _clients.Remove(client);
            }
            
            WriteToLog($"Client disconnected:{clientId} Name:{client.Name}");
        }

        private void SignalRHub_ClientNameChanged(string clientId, string newName)
        {
            //Update the client's name if it exists
            var client = _clients.FirstOrDefault(x => x.Id == clientId);
            if (client != null)
            {
                client.Name = newName;
            }
            
            WriteToLog($"Client name changed. Id:{clientId}, Name:{newName}");
        }

        private delegate void SignalRHub_ReturnCheckSqlConnection_ThreadSafe(string strError);
        private void SignalRHub_ReturnCheckSqlConnection(string strError)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new SignalRHub_ReturnCheckSqlConnection_ThreadSafe(SignalRHub_ReturnCheckSqlConnection), new object[] { strError });
            }
            else
            {
                if (strError == "")
                {
                    this.lblSqlConnectionStatus.Text = "Connected";
                    MessageBox.Show("SQL Connection Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Error From Client
                    MessageBox.Show("SQL Connection Error\n\n" + strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.lblSqlConnectionStatus.Text = "Error";
                }

                this.tabControl.SelectedIndex = 2;
            }
        }

        private delegate void SignalRHub_ReturnServiceStopped_ThreadSafe(string serviceName, string strError);
        private void SignalRHub_ReturnServiceStopped(string serviceName, string strError)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new SignalRHub_ReturnServiceStopped_ThreadSafe(SignalRHub_ReturnServiceStopped), new object[] { serviceName, strError });
            }
            else
            {
                if (strError == "")
                {
                    for (int intRow = 0; intRow < dgvServicesDataGridView.Rows.Count; intRow++)
                    {
                        if (dgvServicesDataGridView[0, intRow].Value.ToString() == serviceName)
                        {
                            dgvServicesDataGridView[1, intRow].Value = "Stopped";

                            MessageBox.Show(serviceName + " Stopped Successfully");

                            break;
                        }
                    }
                }
                else
                {
                    //Error From Client
                    MessageBox.Show("Error Stopping " + serviceName + "\n\n" + strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private delegate void SignalRHub_ReturnServiceStarted_ThreadSafe(string serviceName, string strError);
        private void SignalRHub_ReturnServiceStarted(string serviceName, string strError)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new SignalRHub_ReturnServiceStarted_ThreadSafe(SignalRHub_ReturnServiceStarted), new object[] { serviceName, strError });
            }
            else
            {
                if (strError == "")
                {
                    for (int intRow = 0; intRow < dgvServicesDataGridView.Rows.Count; intRow++)
                    {
                        if (dgvServicesDataGridView[0, intRow].Value.ToString() == serviceName)
                        {
                            dgvServicesDataGridView[1, intRow].Value = "Running";

                            MessageBox.Show(serviceName + " Started Successfully");

                            break;
                        }
                    }
                }
                else
                {
                    //Error From Client
                    MessageBox.Show("Error Starting " + serviceName + "\n\n" + strError, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private delegate void SignalRHub_ReturnReadFile_ThreadSafe(DataTable dtReadFileDataTable, string strfileName);
        private void SignalRHub_ReturnReadFile(DataTable dtReadFileDataTable,string strfileName)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new SignalRHub_ReturnReadFile_ThreadSafe(SignalRHub_ReturnReadFile), new object[] { dtReadFileDataTable, strfileName });
            }
            else
            {
                pvtDataTable = dtReadFileDataTable;

                pvtDataView = null;
                pvtDataView = new DataView(pvtDataTable, "", "", DataViewRowState.CurrentRows);

                frmReadFile frmReadFile = new frmReadFile();
                frmReadFile.Text = strfileName;

                for (int intRow = 0; intRow < pvtDataView.Count; intRow++)
                {
                    frmReadFile.rtbFileLines.AppendText(pvtDataView[intRow]["Line"].ToString() + Environment.NewLine);
                }

                frmReadFile.ShowDialog();
            }
        }

        private delegate void SignalRHub_ReturnWindowsServices_ThreadSafe(DataTable dtServiceDataTable);
        private void SignalRHub_ReturnWindowsServices(DataTable dtServiceDataTable)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new SignalRHub_ReturnWindowsServices_ThreadSafe(SignalRHub_ReturnWindowsServices), new object[] { dtServiceDataTable });
            }
            else
            {
                this.dgvServicesDataGridView.Rows.Clear();

                string strClientName = "";
                pvtDataTable = dtServiceDataTable;

                pvtDataView = null;
                pvtDataView = new DataView(pvtDataTable, "", "ServiceName", DataViewRowState.CurrentRows);

                for (int intRow = 0; intRow < pvtDataView.Count; intRow++)
                {
                    this.dgvServicesDataGridView.Rows.Add(pvtDataView[intRow]["ServiceName"].ToString(),
                                                          pvtDataView[intRow]["Status"].ToString());
                }
                
                this.tabControl.SelectedIndex = 1;

                WriteToLog("ReturnWindowsServices");
            }
        }

        private delegate void SignalRHub_ReturnDirectoriesAndFiles_ThreadSafe(DataTable dtFileDataTable);
        private void SignalRHub_ReturnDirectoriesAndFiles(DataTable dtFileDataTable)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new SignalRHub_ReturnDirectoriesAndFiles_ThreadSafe(SignalRHub_ReturnDirectoriesAndFiles), new object[] { dtFileDataTable });
            }
            else
            {
                pvtblnLoadingTreeView = true;
                string strClientName = "";

                pvtFileDataTable = dtFileDataTable;

                pvtDataView = null;
                pvtDataView = new DataView(pvtFileDataTable, "", "FullFileName", DataViewRowState.CurrentRows);

                for (int intRow = 0; intRow < pvtDataView.Count; intRow++)
                {
                    string[] strDirectories = pvtDataView[intRow]["FullFileName"].ToString().Split('\\');

                    for (int intDir = 0; intDir < strDirectories.Length - 1; intDir++)
                    {
                        if (intRow == 0)
                        {
                            //Add Nodes
                            TreeNode treeNode = new TreeNode(strDirectories[intDir]);
                            treeNode.Name = strDirectories[intDir];

                            if (intDir == 0)
                            {
                                this.treeViewFiles.Nodes.Add(treeNode);
                            }
                            else
                            {
                                if (intDir == strDirectories.Length - 2)
                                {
                                    //Files
                                    treeNode.Tag = "Y";
                                }

                                this.treeViewFiles.SelectedNode.Nodes.Add(treeNode);
                            }

                            this.treeViewFiles.SelectedNode = treeNode;
                        }
                        else
                        {
                            if (intDir == 0)
                            {
                                this.treeViewFiles.SelectedNode = treeViewFiles.Nodes[0];
                            }
                            else
                            {
                                TreeNode[] tn = treeViewFiles.Nodes[0].Nodes.Find(strDirectories[intDir], true);

                                if (tn.Length == 0)
                                {
                                    TreeNode treeNode = new TreeNode(strDirectories[intDir]);
                                    treeNode.Name = strDirectories[intDir];

                                    if (intDir == strDirectories.Length - 2)
                                    {
                                        //Files
                                        treeNode.Tag = "Y";
                                    }

                                    this.treeViewFiles.SelectedNode.Nodes.Add(treeNode);

                                    this.treeViewFiles.SelectedNode = this.treeViewFiles.Nodes[strDirectories[intDir]];
                                }
                                else
                                {
                                    this.treeViewFiles.SelectedNode = tn[0];
                                }
                            }
                        }
                    }
                }

                pvtblnLoadingTreeView = false; 

                this.tabControl.SelectedIndex = 0;

                WriteToLog("ReturnDirectoriesAndFiles");
            }
        }
               
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbClients.SelectedValue != null
                && this.cboMessage.SelectedItem != null)
                {
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();

                    hubContext.Clients.Client((string)cmbClients.SelectedValue).addMessage(this.cboMessage.SelectedItem.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Send Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private delegate void WriteToLog_ThreadSafe(string log);
        public void WriteToLog(string log)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new WriteToLog_ThreadSafe(WriteToLog), new object[] { log });
            }
            else
            {
                txtLog.AppendText(log + Environment.NewLine);
            }
        }
        
        private void FrmServer_Load(object sender, EventArgs e)
        {
            txtLog.Clear();

            try
            {
                //Start SignalR server with the give URL address
                //Final server address will be "URL/signalr"
                _signalR = WebApp.Start<Startup>(txtUrl.Text);

                txtUrl.Enabled = false;
                grpBroadcast.Enabled = true;

                WriteToLog($"Server started at:{txtUrl.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _clients.Clear();

            SignalRHub.ClearState();

            if (_signalR != null)
            {
                _signalR.Dispose();
                _signalR = null;
            }
        }

        private void treeViewFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (pvtblnLoadingTreeView == false)
            {
                TreeNode myNode = this.treeViewFiles.SelectedNode;

                this.dgvFilesDataGridView.Rows.Clear();

                if (myNode.Tag == null)
                {
                    //No Files
                    this.lblFilePath.Text = "";
                }
                else
                {
                    //Files
                    string[] strPath = new string[15];
                    string strFilePath = "";

                    strPath[0] = myNode.Text;
                    int intLevel = 1;

                treeViewFiles_Continue:

                    if (myNode.Parent != null)
                    {
                        myNode = myNode.Parent;

                        strPath[intLevel] = myNode.Text;

                        intLevel += 1;

                        goto treeViewFiles_Continue;

                    }

                    for (int intRow = strPath.Length - 1; intRow > -1; intRow--)
                    {
                        if (strPath[intRow] == null)
                        {

                        }
                        else
                        {
                            strFilePath += strPath[intRow] + "\\";
                        }
                    }

                    this.lblFilePath.Text = strFilePath;

                    pvtDataView = null;
                    pvtDataView = new DataView(pvtFileDataTable, "FullFileName Like '%" + strFilePath + "%'", "FileName", DataViewRowState.CurrentRows);

                    for (int intRow = 0; intRow < pvtDataView.Count; intRow++)
                    {
                        int intOffset = pvtDataView[intRow]["FileName"].ToString().IndexOf(strPath[0]) + strPath[0].Length + 1;


                        string strFile = pvtDataView[intRow]["FileName"].ToString();

                        DateTime LastWriteTime = Convert.ToDateTime(pvtDataView[intRow]["LastWriteTime"]);


                        this.dgvFilesDataGridView.Rows.Add(pvtDataView[intRow]["FileName"].ToString(),
                                                           Convert.ToDateTime(pvtDataView[intRow]["LastWriteTime"]).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvFilesDataGridView.Rows.Count > 0)
                {
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();

                    string strFilePath = this.lblFilePath.Text + this.dgvFilesDataGridView[0, this.dgvFilesDataGridView.SelectedRows[0].Index].Value.ToString();

                    hubContext.Clients.Client((string)cmbClients.SelectedValue).addMessage(@"ReadFile=" + strFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "btnReadFile_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnServiceStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvServicesDataGridView.Rows.Count > 0)
                {
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();

                    string strServiceName = this.dgvServicesDataGridView[0, this.dgvServicesDataGridView.SelectedRows[0].Index].Value.ToString();

                    hubContext.Clients.Client((string)cmbClients.SelectedValue).addMessage(@"ServiceStart=" + strServiceName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "btnServiceStart_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnServiceStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvServicesDataGridView.Rows.Count > 0)
                {
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();

                    string strServiceName = this.dgvServicesDataGridView[0, this.dgvServicesDataGridView.SelectedRows[0].Index].Value.ToString();

                    hubContext.Clients.Client((string)cmbClients.SelectedValue).addMessage(@"ServiceStop=" + strServiceName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "btnServiceStop_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
