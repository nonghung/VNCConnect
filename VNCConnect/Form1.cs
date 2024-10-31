using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
//using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Xml.Linq;

namespace VNCConnect
{
    public partial class Form1 : Form

    {
        #region init
        private static string iniFile = Path.GetDirectoryName(Application.ExecutablePath) + "\\Setup.ini";
        public static string DBNameAddr = string.Empty;
        public static string _S_DB_NAME = string.Empty;
        private static bool _BACKUP_ENABLE = false;
        private static string SFCSWebService = "http://v2haproxy_vip.wneweb.com.tw:100/sfcs_webservice/ats.asmx";
        private static string SendMailWebService = "http://v2sfcsjob1/MessageService/messageservice.asmx";
        private static string DBServer = "";
        private static string DBBackupServer = "";
        private static string DBName = "QVRT";
        private static string DBLoginID = "YXRz";
        private static string DBLoginPW = "am0wMDIw";
        public static SqlConnectionStringBuilder cnDBSetup;
        Thread threadSorted;
        SFCS_ATS.ATS aTS;
        CommandPrompt commandPromp;
        private const uint WM_CLOSE = 0x0010;
        FtpServer ftpServer = new FtpServer();
        string filePath = "";
        bool bMaxsize = true;
        string txtSqlString = "";
        List<string> PcActive = new List<string>();
        bool bSmbConnect = false;
        bool bActive = false;

        [StructLayout(LayoutKind.Sequential)]
        public struct NetResource
        {
            public int dwScope;
            public int dwType;
            public int dwDisplayType;
            public int dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpProvider;
        }

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(ref NetResource netResource, string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags, bool force);

        #endregion
        public Form1()
        {
            DateTime lastWriteTime = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            InitializeComponent();
            Text = "VNC connect by SE team * BuiltDateTime: " + lastWriteTime;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormResize();
            DataTable dataTable = new DataTable();
            try
            {
                // Read all lines from the text file
                string[] lines = File.ReadAllLines(filePath);

                // Assuming the first line contains the headers
                if (lines.Length > 0)
                {
                    // Split the first line to get column names
                    string[] headers = lines[0].Split(',');
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header.Trim());
                    }

                    // Read the remaining lines for data
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] rowData = lines[i].Split(',');
                        dataTable.Rows.Add(rowData);

                    }
                }

                // Bind the DataTable to the DataGridView
                dvTableView.DataSource = dataTable;

                List<Task<PingResult>> pingTasks = new List<Task<PingResult>>();
                Thread thread = new Thread((ThreadStart)delegate
                {
                    CheckIP(dvTableView);

                });
                thread.IsBackground = true;
                thread.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private async void CheckIP(DataGridView dt)
        {
            List<Task<PingResult>> pingTasks = new List<Task<PingResult>>();
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                string strIP = dt.Rows[i].Cells["Station_IP"].Value.ToString();
                pingTasks.Add(PingAsync(strIP, dt.Rows[i]));
            }
            // Wait for all ping tasks to complete
            PingResult[] results = await Task.WhenAll(pingTasks);
        }

        private void MoveRowToFail(DataGridViewRow row, Color color = default)
        {
            if (color == default)
            {
                color = Color.Red; // Set to Red if no color is specified
            }
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    MoveRowToFail(row, color);
                });
            }
            else
            {
                if (dtGridViewFail.Columns.Count != dvTableView.Columns.Count)
                {
                    for (int i = 1; i < dvTableView.Columns.Count; i++)
                    {
                        // Create a new column based on the source column
                        DataGridViewColumn newColumn = (DataGridViewColumn)dvTableView.Columns[i].Clone();

                        // Add the new column to the destination DataGridView
                        dtGridViewFail.Columns.Add(newColumn);
                    }

                }
                // Create a new DataGridViewRow
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dtGridViewFail);
                // Populate the cells with values from the DataRow
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    newRow.Cells[i].Value = row.Cells[i].Value;
                }
                newRow.DefaultCellStyle.BackColor = color;
                // Add the new DataGridViewRow to dtGridViewFail
                dtGridViewFail.Rows.Add(newRow);

                // Remove the DataRow from dvTableView (if applicable)
                dvTableView.Rows.Remove(row);
            }
        }
        private async Task<PingResult> PingAsync(string ip, DataGridViewRow row)
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply reply = await ping.SendPingAsync(ip, 1000); // 1000 ms timeout
                    if (reply.Status == IPStatus.Success)
                    {

                    }
                    else
                    {
                        // row.DefaultCellStyle.BackColor = Color.Red; // Failed ping
                        MoveRowToFail(row);                                          // redRows.Add(row);
                    }
                    return new PingResult
                    {
                        IP = ip,
                        Status = reply.Status,
                        RoundtripTime = reply.RoundtripTime
                    };
                }
                catch (Exception)
                {

                    // row.DefaultCellStyle.BackColor = Color.Yellow; // Error in pinging
                    MoveRowToFail(row);
                    return new PingResult
                    {
                        IP = ip,
                        Status = IPStatus.Unknown,
                        RoundtripTime = -1
                    };
                }
            }
        }

        private bool connectVNC(string remoteHost)
        {
            SetTextControl(lbStatus, "");
            bool result = true;
            // Path to the TightVNC Viewer executable
            string tightVncViewerExe = @"tvnviewer.exe"; ; // Adjust the path as needed
            string tightVncViewerPath = @"C:\Program Files\TightVNC\"; // Adjust the path as needed
            if (!File.Exists(Path.Combine(tightVncViewerPath, tightVncViewerExe)))
            {
                tightVncViewerPath = "C:\\Program Files (x86)\\TightVNC";
                if (!File.Exists(Path.Combine(tightVncViewerPath, tightVncViewerExe)))
                {
                    SetTextControl(lbStatus, "haven't TightVNC App", Color.Red);
                    return false;
                }
            }
            tightVncViewerExe = Path.Combine(tightVncViewerPath, tightVncViewerExe);

            // Optional: Password for the remote server
            List<string> listPassword = new List<string>();
            listPassword.Add("123abc@");
            listPassword.Add("000000");
            listPassword.Add("test");
            // Start the TightVNC Viewer
            string strPort = "5900";
            string windowTitle = $"{remoteHost}::{strPort} - TightVNC Viewer";
            // if (bCheckWebRev(remoteHost) || bCheckPing(remoteHost))
            if (bCheckWebRev(remoteHost))
            {
                IntPtr hWnd;
                for (int i = 0; i < listPassword.Count; i++)
                {
                    string Arguments = $"{remoteHost}::{strPort} -password={listPassword[i]} -useclipboard=yes -scale=auto";
                    Thread thread = new Thread((ThreadStart)delegate
                    {
                        ExecuteCmdResult(tightVncViewerExe, Arguments, tightVncViewerPath);
                    });
                    thread.IsBackground = true;
                    thread.Start();
                    Thread.Sleep(500);
                    hWnd = FindWindow(null, windowTitle);
                    if (hWnd != IntPtr.Zero)
                    {
                        //Console.WriteLine("Cửa sổ đã tìm thấy!");
                        // Thực hiện các thao tác khác với cửa sổ nếu cần
                        // Send the close message
                        SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        Thread.Sleep(100);
                        if (i == listPassword.Count - 1)
                        {
                            if (!bActive)
                            {
                                SetTextControl(lbStatus, "reset password to 123abc@", Color.Yellow);
                                ActiveSMB(remoteHost, "cmd", "/c reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\TightVNC\\Server\" /f /v PASSWORD /t REG_BINARY /d fbef774d01bdaaf4", ftpServer.Username, ftpServer.Password);
                                SetTextControl(lbStatus, "Fail, reset to 123abc@ Done, pls reset PC", Color.Red);
                            }
                            else
                            {
                                SetTextControl(lbStatus, "Wrong password VNC", Color.Red);
                            }
                            result = false;
                        }
                    }
                    else
                    {
                        SetTextControl(lbStatus, "Connect OK", Color.Green);
                        break;
                    }
                }
                hWnd = FindWindow(null, windowTitle);
                if (hWnd != IntPtr.Zero)
                {
                    SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
            }
            else
            {
                SetTextControl(lbStatus, "Check ping or Connect Web tightvnc Fail", Color.Red);
                result = false;
            }
            return result;
        }
        private bool connectVNCbk(string strIP)
        {

            SetTextControl(lbStatus, "");
            bool result = true;
            // Path to the TightVNC Viewer executable
            string tightVncViewerExe = @"tvnviewer.exe"; ; // Adjust the path as needed
            string tightVncViewerPath = @"C:\Program Files\TightVNC\"; // Adjust the path as needed
            if (!File.Exists(Path.Combine(tightVncViewerPath, tightVncViewerExe)))
            {
                tightVncViewerPath = "C:\\Program Files (x86)\\TightVNC";
                if (!File.Exists(Path.Combine(tightVncViewerPath, tightVncViewerExe)))
                {
                    SetTextControl(lbStatus, "haven't TightVNC App", Color.Red);
                    return false;
                }
            }
            tightVncViewerExe = Path.Combine(tightVncViewerPath, tightVncViewerExe);

            // IP address or hostname of the remote server
            string remoteHost = strIP;//"10.163.113.51"; // Replace with the actual IP address

            // Optional: Password for the remote server
            string password = "123abc@"; // Replace with the actual password
            string password0 = "000000";
            string passwordtest = "test";
            // Start the TightVNC Viewer
            string strPort = "5900";
            string windowTitle = $"{remoteHost}::{strPort} - TightVNC Viewer";
            string Arguments = $"{remoteHost}::{strPort} -password={password} -useclipboard=yes -scale=auto";
            if (bCheckWebRev(remoteHost) || bCheckPing(remoteHost))
            {
                Thread thread = new Thread((ThreadStart)delegate
                {
                    ExecuteCmdResult(tightVncViewerExe, Arguments, tightVncViewerPath);
                });
                thread.IsBackground = true;
                thread.Start();
                Thread.Sleep(500);

                IntPtr hWnd = FindWindow(null, windowTitle);
                if (hWnd != IntPtr.Zero)
                {
                    //Console.WriteLine("Cửa sổ đã tìm thấy!");
                    // Thực hiện các thao tác khác với cửa sổ nếu cần
                    // Send the close message
                    SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    Thread.Sleep(100);
                    Arguments = $"{remoteHost}::{strPort} -password={password0}";
                    windowTitle = $"{remoteHost}::{strPort} - TightVNC Viewer";
                    Thread thread1 = new Thread((ThreadStart)delegate
                    {
                        ExecuteCmdResult(tightVncViewerExe, Arguments, tightVncViewerPath);
                    });
                    thread1.IsBackground = true;
                    thread1.Start();
                    Thread.Sleep(500);

                    if (hWnd != IntPtr.Zero)
                    {
                        //Console.WriteLine("Cửa sổ đã tìm thấy!");
                        // Thực hiện các thao tác khác với cửa sổ nếu cần
                        // Send the close message
                        SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        Thread.Sleep(100);
                        Arguments = $"{remoteHost}::{strPort} -password={passwordtest}";
                        windowTitle = $"{remoteHost}::{strPort} - TightVNC Viewer";
                        Thread thread2 = new Thread((ThreadStart)delegate
                        {
                            ExecuteCmdResult(tightVncViewerExe, Arguments, tightVncViewerPath);
                        });
                        thread2.IsBackground = true;
                        thread2.Start();
                        Thread.Sleep(700);

                        if (hWnd != IntPtr.Zero)
                        {
                            //Console.WriteLine("Cửa sổ đã tìm thấy!");
                            // Thực hiện các thao tác khác với cửa sổ nếu cần
                            // Send the close message
                            SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            SetTextControl(lbStatus, "Pasword fail", Color.Red);
                            result = false;
                        }
                    }
                }
                hWnd = FindWindow(null, windowTitle);
                if (hWnd != IntPtr.Zero)
                {
                    SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
            }
            else
            {
                // SetTextControl(lbStatus, "Check ping Fail", Color.Red);
                SetTextControl(lbStatus, "Check ping or Connect Web tightvnc Fail", Color.Red);
                result = false;
            }
            return result;
        }

        bool SetTextControl(Control control, string sText, Color colortext = default)
        {
            bool result = true;
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetTextControl(control, sText, colortext);
                });
            }
            else
            {
                control.Text = sText;
            }
            if (colortext == default)
            {
                colortext = Color.White;
            }

            control.BackColor = colortext;
            return result;
        }
        bool SetControl(Control control, bool Enable)
        {
            bool result = true;
            Invoke((MethodInvoker)delegate
            {
                control.Enabled = Enable;
            });
            return result;
        }
        private bool bCheckPing(string strIP)
        {
            bool bRet = false;
            Ping ping = new Ping();
            PingReply pingresult = ping.Send(strIP, 500);
            if (pingresult.Status.ToString() == "Success")
            {
                bRet = true;
            }
            return bRet;
        }

        private bool bCheckWebRev(string strIP, TimeSpan? timeout = null)
        {
            var url = $"http://{strIP}:5800";
            using (var client = new WebClient())
            {
                try
                {
                    // Set up a cancellation token for the timeout
                    var cancellationTokenSource = new CancellationTokenSource();
                    cancellationTokenSource.CancelAfter(timeout ?? TimeSpan.FromMilliseconds(500));

                    // Start the download task
                    var task = Task.Run(() => client.DownloadString(url), cancellationTokenSource.Token);

                    // Wait for the task to complete or for the timeout to occur
                    task.Wait(cancellationTokenSource.Token);
                    return true;

                }
                catch (Exception ex)
                {
                    // Log the exception (ex) if needed
                    return false;
                }
            }
            return false; // Service is not available
        }

        //private bool bCheckWebRev(string strIP)
        //{
        //    bool bRet = false;
        //    string data = "";
        //    if (ExecuteCmdResult("curl", ref data, $"--max-time 0.5 http://{strIP}:5800"))
        //    {
        //        bRet = true;
        //    }

        //    return bRet;
        //}


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exe">Command to run.</param>
        /// <param name="arguments">Arguments to pass to exetham số truyền vào exe.</param>
        /// <param name="workingDirectory">Working directory to run command inthư mục làm việc để chạy.</param>
        public bool ExecuteCmdResult(string exe, string arguments = "", string workingDirectory = "", bool bDontCreatewindow = true)
        {
            try
            {

                using (commandPromp = new CommandPrompt(exe, arguments, workingDirectory))
                {

                    commandPromp.BeginRun();

                    while (commandPromp.IsRunning)
                    {
                        //string b = commandPromp.data_receive;
                        //addLogText(b + "\n");
                        //Thread.Sleep(5000);
                    }
                    Thread.Sleep(200);
                    string a = commandPromp.data_receive;
                    string error = commandPromp.StandardError;
                    return true;

                }

            }
            catch (Exception exception)
            {
                return false;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exe"></param>
        /// <param name="arguments"></param>
        /// <param name="workingDirectory"></param>
        /// <param name="sRev"></param>
        /// <param name="bDontCreatewindow"></param>
        /// <returns></returns>
        public bool ExecuteCmdResult(string exe, ref string sRev, string arguments = "", string workingDirectory = "", bool bDontCreatewindow = true)
        {
            try
            {

                using (commandPromp = new CommandPrompt(exe, arguments, workingDirectory))
                {
                    commandPromp.BeginRun();
                    while (commandPromp.IsRunning)
                    {
                        //string b = commandPromp.data_receive;
                        //addLogText(b + "\n");
                        //Thread.Sleep(5000);
                    }
                    Thread.Sleep(200);
                    string error = commandPromp.StandardError;
                    sRev = commandPromp.data_receive;
                    if (sRev != null && !sRev.Contains("Connection timed out"))
                    {
                        return true;
                    }
                    return false;
                }

            }
            catch (Exception exception)
            {
                return false;

            }
        }
        private void cbPartNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSet = new DataSet();
                string text = $"SELECT DISTINCT t.STATION as STATION_NAME FROM spn_tabl s,SPN_STATION t WHERE s.bu='{cbSelBU.Text}' and s.pn='{cbPartNumber.Text.Trim()}' AND s.SPN=t.SPN";
                txtSqlString = text;
                dataSet = GetDataFromSFCS(txtSqlString, "ASSP_V001");
                cbStation.Items.Clear();
                cbStation.Text = "";
                cbStation.Items.Add("");
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    cbStation.Items.Add(dataSet.Tables[0].Rows[i].ItemArray[0].ToString());
                }
                if (cbStation.Items.Count != 0)
                {
                    cbStation.Text = cbStation.Items[0].ToString();
                }
            }
            catch { }

            #region remove
            //DataSet dataSet = new DataSet();
            //string text = "";
            //SqlConnection sqlConnection = openDB();
            //if (sqlConnection != null)
            //{
            //    text = "SELECT DISTINCT Station_Name FROM Testing_Station_Management WHERE BU='" + cbSelBU.Text + "' and Part_Number ='" + cbPartNumber.Text.Trim() + "' order by Station_Name";
            //    txtSqlString = text;

            //    SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
            //    SqlDataReader reader = sqlCommand.ExecuteReader();
            //    dataSet.Load(reader, LoadOption.OverwriteChanges, "Testing_Station_By_PN");
            //    cbStation.Items.Clear();
            //    cbStation.Text = "";
            //    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            //    {
            //        cbStation.Items.Add(dataSet.Tables[0].Rows[i].ItemArray[0].ToString());
            //    }
            //    //if (cbStation.Items.Count != 0)
            //    //{
            //    //    cbStation.Text = cbPartNumber.Items[0].ToString();
            //    //}
            //}
            //sqlConnection.Close();
            #endregion
        }

        private void cbSelBU_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet dataSet = new DataSet();
            string text = "";
            SqlConnection sqlConnection = openDB();
            if (sqlConnection != null)
            {
                text = "SELECT DISTINCT Part_Number FROM Testing_Station_Management WHERE BU='" + cbSelBU.Text + "'  order by Part_Number";
                txtSqlString = text;
                //EditorUI.myMsgH.saveLog(text, "cbSelBU_SelectedIndexChanged");
                SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                dataSet.Load(reader, LoadOption.OverwriteChanges, "Testing_Station_Facility_Mapping_Current");
                cbPartNumber.Items.Clear();

                cbPartNumber.Text = "";
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    cbPartNumber.Items.Add(dataSet.Tables[0].Rows[i].ItemArray[0].ToString());

                }
                if (cbPartNumber.Items.Count != 0)
                {
                    cbPartNumber.Text = cbPartNumber.Items[0].ToString();
                }

            }
            sqlConnection.Close();
        }
        public static SqlConnection openDB()
        {
            cnDBSetup = new SqlConnectionStringBuilder();
            DBNameAddr = DBName + ":" + DBServer + ":" + iniFile;
            _S_DB_NAME = DBName;
            if (_BACKUP_ENABLE)
            {
                cnDBSetup.DataSource = DBBackupServer;
            }
            else
            {
                cnDBSetup.DataSource = DBServer;
            }
            cnDBSetup.InitialCatalog = DBName;
            cnDBSetup.UserID = DBLoginID;
            cnDBSetup.Password = DBLoginPW;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(cnDBSetup.ConnectionString);
                sqlConnection.Open();
                return sqlConnection;
            }
            catch
            {
                MessageBox.Show("Connect to backup host");
                cnDBSetup.DataSource = DBBackupServer;
                try
                {
                    SqlConnection sqlConnection = new SqlConnection(cnDBSetup.ConnectionString);
                    sqlConnection.Open();
                    _BACKUP_ENABLE = true;
                    return sqlConnection;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetTextControl(lbStatus, "On going search", Color.Yellow);
            try
            {
                btnSearch.Enabled = false;
                DataSet dataSet = new DataSet();
                string text = "";
                text = "SELECT t.STATION as STATION_NAME,t.IP as STATION_IP, s.pn as PART_NUMBER,t.STATION_SEQ as SN,t.STAGE as STAGE FROM spn_tabl s,SPN_STATION t WHERE";
                if (0 != cbSelBU.Text.Trim().Length)
                {

                    text = text + " s.bu='" + cbSelBU.Text + "'";
                }
                if (0 != cbPartNumber.Text.Trim().Length)
                {
                    if (0 == cbSelBU.Text.Trim().Length)
                    {
                        text = text + " s.pn='" + cbPartNumber.Text.Trim() + "'";
                    }
                    else
                    {
                        text = text + " AND s.pn='" + cbPartNumber.Text.Trim() + "'";
                    }

                }
                if (0 != cbStation.Text.Trim().Length)
                {
                    text = text + " and t.STATION like '%" + cbStation.Text.Trim() + "%'";
                }
                text = text + " AND s.SPN=t.SPN ORDER BY t.STATION ASC";
                txtSqlString = text;
                dataSet = GetDataFromSFCS(txtSqlString, "ASSP_V001");
                dvTableView.DataSource = dataSet.Tables[0];
                btnSearch.Enabled = true;
                dtGridViewFail.Rows.Clear();
                Thread thread = new Thread((ThreadStart)delegate
                {
                    CheckIP(dvTableView);
                    SetTextControl(lbStatus, "search finish", Color.Green);
                });
                thread.IsBackground = true;
                thread.Start();
            }
            catch { btnSearch.Enabled = true; SetTextControl(lbStatus, "search Error", Color.Red); }
            if (bMaxsize)
            {
                FormResize();
                this.StartPosition = FormStartPosition.CenterScreen;
                bMaxsize = false;
            }
            #region remove
            // SqlConnection sqlConnection = openDB();
            //try
            //{
            //    if (sqlConnection != null)
            //    {
            //        DataSet dataSet = new DataSet();
            //        string text = "";
            //        text = "SELECT Station_IP,Station_Name,Part_Number,Test_Station_Id,Record_Status,Creator,Date_Time,Serial_Number FROM Testing_Station_Management WHERE";
            //        if (0 != cbSelBU.Text.Trim().Length)
            //        {
            //            text = text + " BU='" + cbSelBU.Text + "'";
            //        }
            //        if (0 != cbPartNumber.Text.Trim().Length)
            //        {
            //            if (0 == cbSelBU.Text.Trim().Length)
            //            {
            //                text = text + " Part_Number like '%" + cbPartNumber.Text.Trim() + "%'";
            //            }
            //            else
            //            {
            //                text = text + " AND Part_Number like '%" + cbPartNumber.Text.Trim() + "%'";
            //            }

            //        }
            //        else if (0 != txtStationIP_Search.Text.Trim().Length)
            //        {
            //            text = text + " AND Station_IP like '%" + txtStationIP_Search.Text.Trim() + "%'";
            //        }

            //        if (0 != cbStation.Text.Trim().Length)
            //        {
            //            text = text + " and Station_Name like '%" + cbStation.Text.Trim() + "%'";
            //        }

            //        txtSqlString = text;

            //        SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
            //        SqlDataReader reader = sqlCommand.ExecuteReader();
            //        dataSet.Load(reader, LoadOption.OverwriteChanges, "Testing_Station_Management");
            //        dvTableView.DataSource = dataSet.Tables[0];
            //        for (int i = 4; i < dvTableView.Columns.Count; i++)
            //        {
            //            dvTableView.Columns[i].Visible = false;

            //        }

            //    }
            //    sqlConnection.Close();
            //}
            //catch (Exception)
            //{
            //    sqlConnection.Close();
            //    return;

            //}
            //Thread thread = new Thread((ThreadStart)delegate
            //{
            //    CheckIP(dvTableView);
            //});
            //thread.IsBackground = true;
            //thread.Start();
            //if (cbSelBU.Text.Trim() == "VBU")
            //{
            //    _BUMode = BUMode.Virtual;
            //}
            //else
            //{
            //    _BUMode = BUMode.Normal;
            //}
            //common.Part_Number = string.Empty;
            #endregion
        }
        private void addBbutton()
        {
            // Create a button column
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Action",
                Text = "Connect",
                UseColumnTextForButtonValue = true // Use the same text for all buttons
            };
            // Add the button column to the DataGridView
            dvTableView.Columns.Add(buttonColumn);

            dvTableView.CellContentClick += DataGridView_CellContentClick;
            buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Action",
                Text = "Connect",
                UseColumnTextForButtonValue = true // Use the same text for all buttons
            };
            dtGridViewFail.Columns.Add(buttonColumn);
            dtGridViewFail.CellContentClick += DataGridViewFail_CellContentClick;
        }
        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is a button cell

            if (e.ColumnIndex == 0 && e.RowIndex >= 0) // Assuming button column is at index 0
            {
                string strIP = dvTableView.Rows[e.RowIndex].Cells["Station_IP"].Value.ToString();
                connectVNC(strIP);
            }
            //else if (e.ColumnIndex == 1 && e.RowIndex >= 0) // Assuming button column is at index 0
            //{
            //    string strIP = dvTableView.Rows[e.RowIndex].Cells["Station_IP"].Value.ToString();
            //    connectVNC(strIP);
            //}
        }
        private void DataGridViewFail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is a button cell
            if (e.ColumnIndex == 0 && e.RowIndex >= 0) // Assuming button column is at index 0
            {
                string strIP = dtGridViewFail.Rows[e.RowIndex].Cells["Station_IP"].Value.ToString();
                connectVNC(strIP);
            }
        }
        private void LoadActive()
        {
            string sFile = "\\\\10.169.100.18\\nvnm\\Public\\Paul\\Active.ini";
            if (File.Exists(sFile))
            {
                try
                {
                    PcActive.AddRange(File.ReadAllText(sFile).Split(';').ToList());
                }
                catch (Exception)
                {

                }
            }
        }
        private void FormResize(int iWidth = 1022, int iHeight = 718)
        {
            this.Width = iWidth;
            this.Height = iHeight;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FormResize(316, 481);
            LoadActive();
            try
            {
                string fileName = Assembly.GetExecutingAssembly().Location;
                // Tách tên file từ đường dẫn
                string name = System.IO.Path.GetFileName(fileName);
                string sFileServer = "\\\\10.169.100.18\\nvnm\\Public\\Paul\\" + name.Replace(".exe", ".exx");
                if (File.Exists(sFileServer))
                {

                    if (File.Exists(fileName + "x"))
                    {
                        File.Delete(fileName + "x");
                    }

                    FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(sFileServer);
                    if (!fileInfo.ProductVersion.Equals(Assembly.GetExecutingAssembly().GetName().Version.ToString()) && Environment.MachineName != ftpServer.deccode("VjItMi05LVEtMTExMTg="))
                    {
                        File.Move(fileName, fileName + "x");
                        File.Copy(sFileServer, fileName, true);
                        frmMessage frmMessage = new frmMessage();
                        frmMessage.ShowMessage("update new version finish, Please reopen App", MessageBoxButtons.OK);
                        //string sFtpPath = "/ToolBox/App/" + name.Replace(".exe", ".exx");
                        //ftpServer.FtpFileDownLoadApp(sFtpPath, fileName);
                        Environment.Exit(0);
                    }
                }
            }
            catch { }

            SetTextControl(lbStatus, "On Going load data", Color.Yellow);
            Thread thread = new Thread((ThreadStart)delegate
            {
                if (!CheckSeteam())
                {
                    Environment.Exit(0);

                }
                ftpServer.Username = "Llx3bmM=";
                ftpServer.Password = "d25jMDAwMDAw";
                aTS = new SFCS_ATS.ATS();
                SetControl(btnSearch, true);
                SetControl(bt_LoadFromGroupModel, true);
                DBServer = ftpServer.deccode("VjJBVFNEZXBsb3kwMQ==");
                DBBackupServer = ftpServer.deccode("MTAuMTY5Ljk4LjEx");
                DBName = ftpServer.deccode(DBName);
                DBLoginID = ftpServer.deccode(DBLoginID);
                DBLoginPW = ftpServer.deccode(DBLoginPW);
                SetTextControl(lbStatus, "Load data Finish", Color.Green);
            });
            thread.IsBackground = true;
            thread.Start();
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            string username = identity.Name.Split('\\')[1];
            if (Environment.MachineName == ftpServer.deccode("VjItMi05LVEtMTExMTg="))
            {
                bActive = true;
                bt_disable_Defender.Enabled = true;
                bt_Reset_VNC_PW.Enabled = true;
                bt_Restart.Enabled = true;
                bt_UsbOff.Enabled = true;
                bt_UsbOn.Enabled = true;
                bt_En_MSTSC.Enabled = true;
                bt_Dis_MSTSC.Enabled = true;
                bt_EN_Telnet.Enabled = true;

            }
            else if (PcActive.Any(pc => ftpServer.deccode(pc).Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                bActive = true;
                bt_Reset_VNC_PW.Enabled = true;
                bt_Restart.Enabled = true;
                bt_EN_Telnet.Enabled = true;

            }
            else
            {
                gb_PCControl.Visible = false;
            }
            addBbutton();
            filePath = Directory.GetCurrentDirectory() + "\\ipStation.txt";
            loadlistmodel();
            loadcbIP();

            this.KeyDown += new KeyEventHandler(cb_listIP_KeyDown);
            this.KeyPreview = true;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                aTS.Dispose();
            }
            catch { }
        }
        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            string resourceName = "VNCConnect.Help.pdf";
            string tempFilePath = Path.Combine(Path.GetTempPath(), resourceName);

            using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                {
                    Console.WriteLine("Resource not found.");
                    return;
                }
                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }
            }
            try
            {
                Process.Start(tempFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while starting the process: {ex.Message}");
            }
        }

        private int Check_Version()
        {
            try
            {
                string strVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                string strTitle = Assembly.GetExecutingAssembly().GetName().Name.ToString();
                string sVersion = "VNCConnect=1.0.0.1";
                sVersion = File.ReadAllText("\\\\10.169.100.18\\nvnm\\Public\\Paul\\Version.ini");

                List<string> lists = sVersion.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int i = 0; i < lists.Count; i++)
                {
                    List<string> ver = lists[i].Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (ver[0].Equals(strTitle))
                    {
                        if (ver[1].Equals(strVersion))
                        {
                            return 1;
                        }
                        return 0;
                    }
                }
                return -1;
            }
            catch (Exception ex) { return -1; }
        }
        private bool CheckSeteam()
        {
            bool result = false;
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            //List<string> list = new List<string> { "20702603", "21700454", "22700710", "23002840", "19700718", "24003192", "22001415", "22001871", "23002234", "23002321", "23002547", 
            //    "24003469", "23002213", "23002901", "24003032", "23002823" };
            // Get the username
            try
            {
                if (Environment.MachineName == ftpServer.deccode("VjItMi05LVEtMTExMTg="))
                {
                    return true;
                }
                string username = identity.Name.Split('\\')[1];
                using (WS_Workflow.Service1 getLeaders = new WS_Workflow.Service1())
                {
                    DataSet dataSet = getLeaders.GetLeadersMailByEmp(username);
                    DataTable dt = new DataTable();
                    string sBossEmail = dataSet.Tables[0].Rows[0]["DIRECTMAIL"].ToString();
                    string sEmail = dataSet.Tables[0].Rows[0]["EMAIL"].ToString();
                    if (sBossEmail.Equals("syn.ruan@wnc.com.tw") || sEmail.Equals("syn.ruan@wnc.com.tw"))
                    {
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception)
            {

                if (Environment.MachineName == ftpServer.deccode("VjItMi05LVEtMTExMTg="))
                {
                    result = true;
                }
                return result;

            }

        }

        private void loadcbIP()
        {
            try
            {
                cb_listIP.Items.Clear();
                string[] items = File.ReadAllLines(Directory.GetCurrentDirectory() + "//IpConnect.txt");
                for (int i = items.Length - 1; i >= 0; i--)
                {
                    if (items[i].Trim().Length > 0)
                    {
                        cb_listIP.Items.Add(items[i]);
                    }

                }
                cb_listIP.SelectedIndex = 0;
            }
            catch (Exception)
            {

            }

        }
        private void loadlistmodel()
        {
            try
            {
                cbbListModel.Items.Clear();
                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\GroupModel.ini");
                foreach (string line in lines)
                {
                    if (line.Contains("="))
                    {

                        cbbListModel.Items.Add(line.Substring(0, line.IndexOf('=')));
                    }
                }

                cbbListModel.Text = "";
                tb_ListModel.Text = "";

            }
            catch (Exception)
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\GroupModel.ini", "[GroupModel]");

            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (tbIP.Text != "" && tbModel.Text != "" && tbStation.Text != "")
            {
                string strConten = $"\n{tbIP.Text},{tbStation.Text},{tbModel.Text}";
                File.AppendAllText(filePath, strConten);
            }
        }

        private void bt_LoadFromGroupModel_Click(object sender, EventArgs e)
        {
            SetTextControl(lbStatus, "On going load model", Color.Yellow);
            try
            {
                DataSet dataSet = new DataSet();
                string text = "";
                string strGroupModel = "";
                strGroupModel = tb_ListModel.Text.Trim();//ReadINI(Directory.GetCurrentDirectory(), "GroupModel", "GroupModel",cbbListModel.Text.Trim(), "Vista");
                if (!string.IsNullOrEmpty(strGroupModel))
                {
                    strGroupModel = strGroupModel.Replace(",", "','");
                    strGroupModel = $"'{strGroupModel}'";
                    text = $"SELECT t.STATION as STATION_NAME,t.IP as STATION_IP, s.pn as PART_NUMBER,t.STATION_SEQ as SN,t.STAGE as STAGE FROM spn_tabl s,SPN_STATION t WHERE s.pn IN ({strGroupModel})  AND s.SPN=t.SPN ORDER BY t.STATION ASC";
                    txtSqlString = text;
                    dataSet = GetDataFromSFCS(txtSqlString, "ASSP_V001");
                    dvTableView.DataSource = dataSet.Tables[0];
                    dtGridViewFail.Rows.Clear();
                    Thread thread = new Thread((ThreadStart)delegate
                    {
                        CheckIP(dvTableView);
                        SetTextControl(lbStatus, "load model Finhsh", Color.Green);
                    });
                    thread.IsBackground = true;
                    thread.Start();
                }
                else
                {
                    SetTextControl(lbStatus, "model Null", Color.Red);
                }
            }
            catch { SetTextControl(lbStatus, "load model Error", Color.Red); }
            if (bMaxsize)
            {
                this.Width = 1022;
                this.Height = 718;
                this.StartPosition = FormStartPosition.CenterScreen;
                bMaxsize = false;

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetTextControl(lbStatus, "On going add model", Color.Yellow);
            ftpServer.ReadINI(Directory.GetCurrentDirectory(), "GroupModel", "GroupModel", cbbListModel.Text.Trim(), "Vista");
            ftpServer.WriteINI(Directory.GetCurrentDirectory(), "GroupModel", "GroupModel", cbbListModel.Text.Trim(), tb_ListModel.Text.Trim());
            loadlistmodel();
            SetTextControl(lbStatus, "add model OK", Color.Green);
        }

        private void cbbListModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbListModel.Text != "")
            {
                string strList = ftpServer.ReadINI(Directory.GetCurrentDirectory(), "GroupModel", "GroupModel", cbbListModel.Text.Trim(), "");
                if (strList != "")
                {
                    tb_ListModel.Text = strList;
                }
                else
                {
                    tb_ListModel.Text = "";
                }
            }
        }

        private void dvTableView_Sorted(object sender, EventArgs e)
        {
            try
            {
                if (threadSorted == null)
                {
                    threadSorted = new Thread((ThreadStart)delegate
                    {
                        // CheckIP(dvTableView);
                    });
                    threadSorted.IsBackground = true;
                    threadSorted.Start();
                }
                else if (!threadSorted.IsAlive)
                {
                    threadSorted = new Thread((ThreadStart)delegate
                    {
                        // CheckIP(dvTableView);
                    });
                    threadSorted.IsBackground = true;
                    threadSorted.Start();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void dvTableView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.RowIndex >= 0 && !bSmbConnect && (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)) // Assuming button column is at index 0
            {
                bSmbConnect = true;
                Thread thread = new Thread((ThreadStart)delegate
                {
                    try
                    {
                        string strIP = dvTableView.Rows[e.RowIndex].Cells["Station_IP"].Value.ToString();
                        SetTextControl(lbStatus, "Connect to SMB:" + strIP, Color.Yellow);
                        string sresult = "";
                        if (e.Button == MouseButtons.Right)
                        {
                            sresult = ConnectSMB(strIP, "C$");
                        }
                        else if (e.Button == MouseButtons.Left)
                        {
                            sresult = ConnectSMB(strIP);
                        }
                        if (sresult == "OK")
                            SetTextControl(lbStatus, sresult, Color.Green);
                        else
                            SetTextControl(lbStatus, sresult, Color.Red);
                    }
                    catch (Exception)
                    {

                        SetTextControl(lbStatus, "Connect Error", Color.Red);
                    }
                    bSmbConnect = false;

                });
                thread.IsBackground = true;
                thread.Start();

            }

        }
        public string ConnectSMB(string Ip, string sDisk = "D$")
        {
            bool bRetry = true;
        Retry:
            SetTextControl(lbStatus, "Connect to SMB:" + Ip, Color.Yellow);
            ftpServer.SmbPath = $"{Ip}\\{sDisk}";
            NetResource netResource = new NetResource
            {
                dwType = 1, // Disk resource
                lpRemoteName = ftpServer.SmbPath,
            };

            int result = WNetAddConnection2(ref netResource, ftpServer.Password, ftpServer.Username, 0);
            if (result == 53)
            {
                if (bRetry && bActive)
                {
                    SetTextControl(lbStatus, "Active SMB:" + Ip, Color.Yellow);
                    ActiveSMB(Ip, "cmd", "/c net share C$=C:\\ /grant:wnc,FULL & net share D$=D:\\ /grant:wnc,FULL & exit", ftpServer.Username, ftpServer.Password);
                    bRetry = false;
                    Thread.Sleep(3000);
                    SetTextControl(lbStatus, "Active SMB Done:" + Ip, Color.Yellow);
                    goto Retry;
                }
                return "Not set SMB";
            }
            else if (result == 86)
            {
                return "Wrong account";
            }

            int re = WNetCancelConnection2(ftpServer.SmbPath, 0, true);

            Process.Start("explorer.exe", ftpServer.SmbPath);
            return "OK";
        }
        public bool ActiveSMB(string remoteMachine, string command, string arguments, string username, string password)
        {
            bool result = false;
            try
            {
                if (!bActive)
                {
                    return true;
                }
                // Create a connection options object
                ConnectionOptions options = new ConnectionOptions
                {
                    Username = username,
                    Password = password,
                    Impersonation = ImpersonationLevel.Impersonate,
                    Authentication = AuthenticationLevel.PacketPrivacy
                };

                // Connect to the remote machine
                ManagementScope scope = new ManagementScope($"\\\\{remoteMachine}\\root\\cimv2", options);
                scope.Connect();

                // Create a process start info object
                ManagementClass processClass = new ManagementClass(scope, new ManagementPath("Win32_Process"), null);

                // Create an instance of the process
                ManagementBaseObject inParams = processClass.GetMethodParameters("Create");
                inParams["CommandLine"] = $"{command} {arguments}";

                // Execute the command
                ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null);

                // Check the return value
                uint returnValue = (uint)outParams["returnValue"];
                if (returnValue == 0)
                {
                    Console.WriteLine("Shutdown command executed successfully.");
                }
                else
                {
                    Console.WriteLine($"Error executing command. Return value: {returnValue}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return true;
        }
        public void getSMB(string ip)
        {
            string remoteMachine = "10.165.111.88";
            string username = "wnc";
            string password = "wnc000000"; // Use a secure method to handle passwords

            try
            {
                // Create a connection options object
                ConnectionOptions options = new ConnectionOptions
                {
                    Username = username,
                    Password = password,
                    Impersonation = ImpersonationLevel.Impersonate,
                    Authentication = AuthenticationLevel.PacketPrivacy
                };

                // Connect to the remote machine
                ManagementScope scope = new ManagementScope($"\\\\{remoteMachine}\\root\\cimv2", options);
                scope.Connect();

                // Query for shared resources
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Share");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                // Execute the query and display results
                foreach (ManagementObject share in searcher.Get())
                {
                    Console.WriteLine($"Name: {share["Name"]}, Path: {share["Path"]}, Type: {share["Type"]}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        private void bt_connect_Click(object sender, EventArgs e)
        {

            bt_connect.Enabled = false;
            string sIP = cb_listIP.Text;


            if (IsValidIPv4(sIP) && connectVNC(sIP))
            {
                string IpConnect = Path.Combine(Directory.GetCurrentDirectory(), "IpConnect.txt");
                string sFileIpconnect = Path.Combine(Directory.GetCurrentDirectory(), "IpConnect.txt");
                if (File.Exists(sFileIpconnect))
                {
                    string data = File.ReadAllText(sFileIpconnect);
                    data = data.Replace(sIP, "").Replace("\n\n", "\n");
                    data = data + "\n" + sIP;
                    File.SetAttributes(IpConnect, FileAttributes.Normal);
                    // File.Delete(IpConnect);
                    File.WriteAllText(IpConnect, data);
                    File.SetAttributes(IpConnect, FileAttributes.Hidden);
                }
                else
                {
                    File.WriteAllText(IpConnect, sIP);
                    File.SetAttributes(IpConnect, FileAttributes.Hidden);
                }
                loadcbIP();
            }
            bt_connect.Enabled = true;
        }
        static bool IsValidIPv4(string ipAddress)
        {
            if (IPAddress.TryParse(ipAddress, out IPAddress ip))
            {
                return ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
            }
            return false;
        }
        private void cb_listIP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Call the connect button click event
                bt_connect_Click(sender, e);

            }
        }

        private DataSet GetDataFromSFCS(string SQLstr, string FUNCstr)
        {
            aTS.Url = SFCSWebService;
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = new DataColumn("SQL");
            dataColumn.DataType = Type.GetType("System.String");
            dataColumn.AllowDBNull = false;
            dataColumn.Caption = "SQL";
            dataColumn.DefaultValue = "";
            DataColumn dataColumn2 = new DataColumn("FUNC");
            dataColumn2.DataType = Type.GetType("System.String");
            dataColumn2.AllowDBNull = false;
            dataColumn2.Caption = "FUNC";
            dataColumn2.DefaultValue = "";
            DataRow dataRow = dataTable.NewRow();
            dataTable.Columns.Add(dataColumn);
            dataTable.Columns.Add(dataColumn2);
            dataRow["SQL"] = SQLstr;
            dataRow["FUNC"] = FUNCstr;
            dataTable.Rows.Add(dataRow);
            dataSet.Tables.Add(dataTable);
            string errMsg = "";
            return aTS.ASSP_V001(dataSet, ref errMsg);
        }

        private void bt_smbConnect_Click(object sender, EventArgs e)
        {
            if (!bSmbConnect)
            {
                bSmbConnect = true;
                string strIP = cb_listIP.Text;
                Thread thread = new Thread((ThreadStart)delegate
                {
                    try
                    {

                        if (bCheckPing(strIP) && bActive)
                        {
                            SetTextControl(lbStatus, "Connect to SMB:" + strIP, Color.Yellow);
                            string sresult = ConnectSMB(strIP);
                            if (sresult == "OK")
                                SetTextControl(lbStatus, sresult, Color.Green);
                            else
                                SetTextControl(lbStatus, sresult, Color.Red);

                        }

                    }
                    catch (Exception)
                    {

                        SetTextControl(lbStatus, "Connect Error", Color.Red);
                    }
                    bSmbConnect = false;

                });
                thread.IsBackground = true;
                thread.Start();

            }
        }

        private void bt_Delete_Click(object sender, EventArgs e)
        {
            if (cbbListModel.Text != "")
            {
                string lineToRemove = cbbListModel.Text;
                try
                {
                    SetTextControl(lbStatus, "delete model", Color.Yellow);
                    // Read all lines from the file
                    var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\GroupModel.ini").ToList();

                    // Remove the specified line
                    lines.RemoveAll(line =>
                    {
                        int index = line.IndexOf('=');
                        if (index != -1)
                        {
                            string partBeforeEquals = line.Substring(0, index).Trim();
                            return partBeforeEquals.Equals(lineToRemove, StringComparison.OrdinalIgnoreCase);
                        }
                        return false; // If there's no '=', don't remove the line
                    });

                    // Write the remaining lines back to the file
                    File.SetAttributes(Directory.GetCurrentDirectory() + "\\GroupModel.ini", FileAttributes.Normal);
                    File.WriteAllLines(Directory.GetCurrentDirectory() + "\\GroupModel.ini", lines);
                    File.SetAttributes(Directory.GetCurrentDirectory() + "\\GroupModel.ini", FileAttributes.Hidden);
                    loadlistmodel();
                    SetTextControl(lbStatus, "delete model OK", Color.Green);
                }
                catch (Exception)
                {

                    SetTextControl(lbStatus, "delete model Fail", Color.Red);
                }
            }
        }

        private void bt_PCControl_Click(object sender, EventArgs e)
        {
            if (bSmbConnect) return;
            bSmbConnect = true;
            string sCommand = "", sAction = "";
            Button button = (Button)sender;
            switch (button.Text)
            {
                case "Restart PC":
                    {
                        sCommand = "/c shutdown -r -t 0 -f";
                        sAction = "Restart PC";
                        break;
                    }
                case "USB ON":
                    {
                        sCommand = "/c reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\USBSTOR\" /f /v Start /t REG_DWORD /d 3 & exit";
                        sAction = "USB ON";
                        break;
                    }
                case "USB OFF":
                    {
                        sCommand = "/c reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\USBSTOR\" /f /v Start /t REG_DWORD /d 4 & exit";
                        sAction = "USB OFF";
                        break;
                    }
                case "DIS Defender":
                    {
                        sCommand = "/c netsh advfirewall set allprofiles state off & exit";
                        sAction = "Disable Defender";
                        break;
                    }
                case "Reset VNC PW":
                    {

                        sCommand = "/c reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\TightVNC\\Server\" /f /v PASSWORD /t REG_BINARY /d fbef774d01bdaaf4 & exit";
                        sAction = "Reset VNC PW to 123abc@";
                        break;
                    }
                case "EN MSTSC":
                    {
                        sCommand = "/c reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Terminal Server\" /f /v \"fDenyTSConnections\" /t REG_DWORD /d 0 & exit";
                        sAction = "Enable MSTSC";
                        break;
                    }
                case "DIS MSTSC":
                    {
                        sCommand = "/c reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Terminal Server\" /f /v \"fDenyTSConnections\" /t REG_DWORD /d 1 & exit";
                        sAction = "Disable MSTSC";
                        break;
                    }

                case "EN Telnet":
                    {
                        sCommand = "/c dism /online /Enable-Feature /FeatureName:TelnetClient & exit";
                        sAction = "Enable telnet";
                        break;
                    }
                case "EN Share":
                    {
                        sCommand = "/c net share C$=C:\\ /grant:wnc,FULL & net share D$=D:\\ /grant:wnc,FULL & exit";
                        sAction = "Enable Share";
                        break;
                    }

            }
            string sIP = tb_Ip_Restart.Text;
            Thread thread = new Thread((ThreadStart)delegate
            {
                if (IsValidIPv4(sIP))
                {
                    ControlPC(button, sCommand, sAction);
                }
                else
                {
                    SetTextControl(lbStatus, $"Ip Error", Color.Red);
                }
                bSmbConnect = false;
            });
            thread.IsBackground = true;
            thread.Start();

        }
        private void ControlPC(Control control, string sCommand, string sAction)
        {
            SetControl(control, false);
            string sIP = tb_Ip_Restart.Text;
            SetTextControl(lbStatus, $"Start {sAction}", Color.Yellow);
            if (IsValidIPv4(sIP) && sCommand != "")
            {
                ActiveSMB(sIP, "cmd", sCommand, ftpServer.Username, ftpServer.Password);//"/c reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Terminal Server\" /f /v \"fDenyTSConnections\" /t REG_DWORD /d 0 & exit");
                SetTextControl(lbStatus, $"{sAction} OK", Color.Green);
            }
            else
            {
                SetTextControl(lbStatus, $"Ip Error", Color.Red);
            }
            SetControl(control, true);
        }
    }

    class PingResult
    {
        public string IP { get; set; }
        public IPStatus Status { get; set; }
        public long RoundtripTime { get; set; }
        public string ErrorMessage { get; set; }
    }
}
