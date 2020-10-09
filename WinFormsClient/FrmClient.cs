using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR;
using System.Net;
using System.Data.SqlClient;

namespace WinFormsClient
{
    public partial class FrmClient : Form
    {
        //Connection to a SignalR Server
        HubConnection _hubServerSignalRConnection;

        //Proxy object for a Hub hosted on the SignalR Server
        IHubProxy _hubServerProxy;
        private object GlobalHost;

        public FrmClient(string _name)
        {
            InitializeComponent();

            this.txtUserName.Text = _name;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            await connectAsync();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            //Close the server connection if exists
            if(_hubServerSignalRConnection != null)
            {
                _hubServerSignalRConnection.Stop();
                _hubServerSignalRConnection.Dispose();
                _hubServerSignalRConnection = null;

                btnConnect.Enabled = true;
                txtUrl.Enabled = true;
                txtUserName.Enabled = true;
                btnDisconnect.Enabled = false;                
            }
        }

        private async Task connectAsync()
        {
            //Create a connection for the SignalR Server
            _hubServerSignalRConnection = new HubConnection(txtUrl.Text);
                       
            _hubServerSignalRConnection.StateChanged += HubConnection_StateChanged;

            //Proxy object that interacts with specific Hub on the Server
            _hubServerProxy = _hubServerSignalRConnection.CreateHubProxy("SignalRHub");

            //Reigster to the "AddMessage" callback method of the hub (This method is invoked by the hub)
            _hubServerProxy.On<string>("AddMessage", (message) => ProcessMessage($"{message}"));            

            btnConnect.Enabled = false;

            try
            {
                //Connect to the server
                await _hubServerSignalRConnection.Start();

                //Send user name for this client, so we won't need to send it with every message
                await _hubServerProxy.Invoke("SetUserName", txtUserName.Text);

                txtUrl.Enabled = false;
                txtUserName.Enabled = false;
                btnDisconnect.Enabled = true;
            }
            catch (Exception ex)
            {
                writeToLog($"Error:{ex.Message}");
                btnConnect.Enabled = true;
            }
        }

        private void HubConnection_StateChanged(StateChange obj)
        {
            if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
            {
                writeToLog("Connected");
            }
            else
            {
                if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
                {
                    writeToLog("Disconnected");
                }
            }
        }      

        public void writeToLog(string log)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => txtLog.AppendText(log + Environment.NewLine)));
            }
            else
            {
                txtLog.AppendText(log + Environment.NewLine);
            }
        }
        
        private async void ProcessMessage(string task)
        {
            //NB message Contains Task Required from Server
            if (task == "GetDirectoriesAndFiles")
            {
                FileInfo[] files = new FileInfo[0];

                int intOffset = 0;
                string strDirectory = "";
                FileInfo fileInfo = new FileInfo(@"C:\Projects\Validite\TimeClient\Time.txt");

                DataTable FileInfoDataTable = new DataTable("FileInfo");

                FileInfoDataTable.Columns.Add("FileName", typeof(String));
                FileInfoDataTable.Columns.Add("FullFileName", typeof(String));
                FileInfoDataTable.Columns.Add("LastWriteTime", typeof(DateTime));

                if (fileInfo != null)
                {
                    //Should always be true
                    intOffset = fileInfo.FullName.IndexOf("Validite");

                    if (intOffset > -1)
                    {
                        strDirectory = fileInfo.FullName.Substring(0, intOffset + 9);
                    }
                    else
                    {
                        intOffset = fileInfo.FullName.IndexOf("Interact Software");

                        if (intOffset > -1)
                        {
                            strDirectory = fileInfo.FullName.Substring(0, intOffset + 18);
                        }
                    }

                    if (strDirectory != "")
                    {
                        var dir = new DirectoryInfo(strDirectory);
                        files = dir.GetFiles("*.*", SearchOption.AllDirectories);

                        for (int intRow = 0; intRow < files.Length; intRow++)
                        {
                            DataRow myDataRow = FileInfoDataTable.NewRow();

                            myDataRow["FileName"] = files[intRow].Name;
                            myDataRow["FullFileName"] = files[intRow].FullName;
                            myDataRow["LastWriteTime"] = files[intRow].LastWriteTime;

                            FileInfoDataTable.Rows.Add(myDataRow);

                        }
                    }
                }

                object[] parms = new object[1];
                parms[0] = FileInfoDataTable;

                await _hubServerProxy.Invoke(task, parms);
            }
            else
            {
                if (task == "GetWindowsServices")
                {
                    DataTable WindowsServiceDataTable = new DataTable("ServiceInfo");

                    WindowsServiceDataTable.Columns.Add("ServiceName", typeof(String));
                    WindowsServiceDataTable.Columns.Add("Status", typeof(String));

                    ServiceController[] services = ServiceController.GetServices();

                    // try to find service name
                    foreach (ServiceController service in services)
                    {
                        if (service.ServiceName == "CFS Batch File Deal Import Service"
                        || service.ServiceName == "AJRouter")
                        {
                            DataRow myDataRow = WindowsServiceDataTable.NewRow();

                            myDataRow["ServiceName"] = service.ServiceName;
                            myDataRow["Status"] = service.Status.ToString();

                            WindowsServiceDataTable.Rows.Add(myDataRow);
                        }
                    }

                    object[] parms = new object[1];
                    parms[0] = WindowsServiceDataTable;

                    await _hubServerProxy.Invoke(task, parms);
                }
                else
                {
                    if (task.Substring(0,8) == "ReadFile")
                    {
                        DataTable FileDataTable = new DataTable("File");

                        FileDataTable.Columns.Add("Line", typeof(String));
                       
                        string[] lines = System.IO.File.ReadAllLines(@task.Substring(9));
                                              
                        foreach (string line in lines)
                        {
                            // Use a tab to indent each line of the file.

                            DataRow myDataRow = FileDataTable.NewRow();

                            myDataRow["Line"] = line;
                           
                            FileDataTable.Rows.Add(myDataRow);
                        }

                        object[] parms = new object[2];
                        parms[0] = FileDataTable;
                        parms[1] = @task.Substring(9);

                        //GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;

                        _hubServerProxy.Invoke(task.Substring(0, 8), parms);
                    }
                    else
                    {
                        if (task.Substring(0, 7) == "Service")
                        {
                            if (task.Substring(0, 12) == "ServiceStart")
                            {
                                string strServiceName = @task.Substring(13);


                                object[] parms = new object[2];
                                parms[0] = strServiceName;
                                parms[1] = "";
                                //parms[1] = "Error";

                                //GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;

                                _hubServerProxy.Invoke("ServiceStarted", parms);
                            }
                            else
                            {
                                if (task.Substring(0, 11) == "ServiceStop")
                                {
                                    string strServiceName = @task.Substring(12);

                                    object[] parms = new object[2];
                                    parms[0] = strServiceName;
                                    //Error Parm
                                    parms[1] = "";

                                    //GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;

                                    _hubServerProxy.Invoke("ServiceStopped", parms);
                                }
                            }
                        }
                        else
                        {
                            if (task == "CheckSqlConnection")
                            {
                                bool blnConnected = false;
                                string strError = "";

                                try
                                {
                                    DataSet dataSet = new DataSet();

                                    StringBuilder strQry = new StringBuilder();

                                    strQry.AppendLine(" SELECT");
                                    strQry.AppendLine(" KEY1");
                                    strQry.AppendLine(" FROM CFS.dbo.TT_TEST ");

                                    string strSqlConnectionString = @"Data Source=SW090272\COFIS_DEV_2014;Initial Catalog=CFS;Trusted_Connection=True;";
                                    using (SqlConnection con = new SqlConnection(strSqlConnectionString))
                                    {
                                        SqlCommand cmd = new SqlCommand(strQry.ToString(), con);

                                        SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(cmd);

                                        objSqlDataAdapter.Fill(dataSet, "Test");
                                    }

                                    blnConnected = true;
                                }
                                catch(Exception ex)
                                {
                                    strError = ex.Message;

                                }

                                object[] parms = new object[1];
                                parms[0] = strError;
                                
                                _hubServerProxy.Invoke(task, parms);
                            }
                        }
                    }
                }
            }
        }
    }
}
