using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VNCConnect
{
    public partial class Form2 : Form
    {
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
      

        public Form2()
        {
            InitializeComponent();

        }
        private delegate void addText(string _text, int _iWriteType = 0);
        public void addLogText(string _text, int _iWriteType = 0)
        {
            if (this.InvokeRequired)
            {
                addText at = new addText(addLogText);
                this.Invoke(at, new object[] { _text, _iWriteType });
            }
            else
            {
                if (_iWriteType == 0 || _iWriteType == 1)
                {
                    richTextBox1.AppendText(_text);
                }
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //SELECT prog.Test_Program_Id,prog.Program_Location FROM Testing_Program_Management prog,Testing_Station_Program_Mapping mapping  WHERE mapping.Test_Program_Id=prog.Test_Program_Id and mapping.Test_Station_Id=(SELECT Test_Station_Id FROM Testing_Station_Management WHERE BU='ND' AND Part_Number like '%57SAQ417.G11%' and Station_Name like '%CHECK FCC%') order by Program_Status,Test_Program_Id desc
            using (SqlConnection sqlConnection = openDB())
            {
                try
                {
                    if (sqlConnection != null)
                    {
                        using (DataSet dataSet = new DataSet())
                        {
                            string text = "";
                            if (0 != cbSelBU.Text.Trim().Length && 0 != cbPartNumber.Text.Trim().Length && 0 != cbStation.Text.Trim().Length)
                            {
                                text = $"SELECT prog.Test_Program_Id,prog.Program_Location FROM Testing_Program_Management prog,Testing_Station_Program_Mapping mapping  WHERE mapping.Test_Program_Id=prog.Test_Program_Id and mapping.Test_Station_Id=(SELECT Test_Station_Id FROM Testing_Station_Management WHERE BU='{cbSelBU.Text.Trim()}' AND Part_Number like '%{cbPartNumber.Text.Trim()}%' and Station_Name like '%{cbStation.Text.Trim()}%') order by Program_Status,Test_Program_Id desc";
                                txtSqlString = text;
                                SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
                                SqlDataReader reader = sqlCommand.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    dataSet.Load(reader, LoadOption.OverwriteChanges, "Testing_Program_Management");
                                    DataTable tb = dataSet.Tables[0];
                                    tb_path_server.Text = tb.Rows[0]["Program_Location"].ToString();
                                    sqlConnection.Close();
                                }

                            }
                        }
                    }
                }
                catch (Exception)
                {
                    sqlConnection.Close();
                    return;

                }
            }

        }
        private void cbPartNumber_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet dataSet = new DataSet();
            string text = "";
            SqlConnection sqlConnection = openDB();
            if (sqlConnection != null)
            {
                text = "SELECT DISTINCT Station_Name FROM Testing_Station_Management WHERE BU='" + cbSelBU.Text + "' and Part_Number ='" + cbPartNumber.Text.Trim() + "' order by Station_Name";
                txtSqlString = text;

                SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                dataSet.Load(reader, LoadOption.OverwriteChanges, "Testing_Station_By_PN");
                cbStation.Items.Clear();
                cbStation.Text = "";
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    cbStation.Items.Add(dataSet.Tables[0].Rows[i].ItemArray[0].ToString());
                }
                //if (cbStation.Items.Count != 0)
                //{
                //    cbStation.Text = cbPartNumber.Items[0].ToString();
                //}
            }
            sqlConnection.Close();
        }
        string txtSqlString = "";
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
                MessageBox.Show("連接至備援主機");
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
        String getPath(int select)
        {
            String Path = "";
            switch (select)
            {
                case 1:
                    {

                        OpenFileDialog _openFileDialog = new OpenFileDialog();
                        _openFileDialog.Title = "Chọn file";
                        _openFileDialog.InitialDirectory = @"D:\";
                        if (_openFileDialog.ShowDialog() == DialogResult.OK)
                            Path = _openFileDialog.FileName; //lay duong dan cua file
                        return Path;
                        break;
                    }
                case 2:
                    {
                        FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();
                        _folderBrowserDialog.SelectedPath = @"D:\";
                        _folderBrowserDialog.ShowNewFolderButton = true;
                        if (_folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            Path = _folderBrowserDialog.SelectedPath;//lay duong dan thu muc
                        }
                        return Path;
                        break;
                    }
            }
            return null;

        }

        private void bt_SelectFolder_Click(object sender, EventArgs e)
        {
            string sPath = getPath(2);
            if (sPath.Length > 3)
                tb_path_local.Text = sPath;
        }
        private bool CalculateMD5ForFolder(string folderPath, List<Filemd5> files)
        {
            string fileName = Path.GetFileName(folderPath);
            string text = CalculateMD5FromString(fileName);
            addLogText("\\" + fileName + "=" + text.ToUpper() + " (Folder Name MD5)");
            string[] fileSystemEntries = Directory.GetFileSystemEntries(folderPath, "*.*", SearchOption.AllDirectories);
            string[] array = fileSystemEntries;
            bool result = true;
            foreach (string text2 in array)
            {
                Filemd5 filemd5 = new Filemd5();
                try
                {
                    if (File.Exists(text2))
                    {
                        filemd5.sFilename = text2.Replace(folderPath, "");
                        filemd5.sMD5 = CalculateMD5(text2);
                        files.Add(filemd5);
                    }
                }
                catch (Exception)
                {
                    filemd5.sFilename = text2.Replace(folderPath, "");
                    filemd5.sMD5 = "Error calculating MD5";
                    files.Add(filemd5);
                    result = false;
                }
            }
            return result;
        }
        private string CalculateMD5(string filePath)
        {
            using (MD5 mD = MD5.Create())
            {
                using (FileStream inputStream = File.OpenRead(filePath))
                {
                    byte[] array = mD.ComputeHash(inputStream);
                    return BitConverter.ToString(array).Replace("-", "").ToLower();
                }
            }

        }
        private string CalculateMD5FromString(string input)
        {
            using (MD5 mD = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] array = mD.ComputeHash(bytes);
                StringBuilder stringBuilder = new StringBuilder();
                byte[] array2 = array;
                foreach (byte b in array2)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                return stringBuilder.ToString();
            }

        }
        struct Filemd5
        {
            public string sFilename;
            public string sMD5;
        }
        bool SetControl(Control control, bool bStatus = true)
        {
            bool result = true;
            Invoke((MethodInvoker)delegate
            {
                control.Enabled = bStatus;
            });
            return result;
        }
        private void bt_upload_Click(object sender, EventArgs e)
        {
            string sFtpserverPath = "ftp://" + ftpServer.ServerIP + tb_path_server.Text.Replace("\\", "/");
            string sLocalPath = tb_path_local.Text;
            DialogResult result = MessageBox.Show("Do you wan't upload to server?. ", "Upload", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }
            Thread thread = new Thread((ThreadStart)delegate
            {
                try
                {
                    SetControl(bt_upload, false);
                    Upload(sLocalPath, sFtpserverPath);
                    SetControl(bt_upload, true);
                }
                catch (Exception ex)
                {
                    addLogText(ex.ToString() + "\n");
                    SetControl(bt_upload, true);
                }
            });
            thread.IsBackground = true;
            thread.Start();

        }
        bool bUploadNewFile = true;
        private bool Upload(string sLocalPath, string sFtpserverPath)
        {
            bool result = true;
            string data = "";
            if (Directory.Exists(sLocalPath) && ftpServer.FtpDirectoryExist(sFtpserverPath) && ftpServer.DownloadFileToString(sFtpserverPath, ref data))
            {
                List<Filemd5> files = new List<Filemd5>();
                if (!CalculateMD5ForFolder(sLocalPath, files))
                {
                    addLogText("CalculateMD5ForFolder fail\n");
                    return false;
                }
                addLogText("CalculateMD5ForFolder OK\n");
                string datanew = "";
                if (!GetLinesContainingKeys(data, files, ref datanew))
                {
                    addLogText("have file the same on server, please check\n");
                    return false;
                }
                addLogText("Old data check sum:\n" + data + "\n");
                addLogText("New data check sum:\n" + datanew + "\n");
                if (!ftpServer.FtpFolderUpload(sLocalPath, sFtpserverPath))
                {
                    addLogText("upload Folder to FTP Fail\n");
                    return false;
                }
                else
                {
                    addLogText("upload Folder to FTP OK\n");
                    string sLocalPathBackup = "D:\\BackUpLocal";
                    if (!Directory.Exists(sLocalPathBackup))
                    {
                        Directory.CreateDirectory(sLocalPathBackup);
                    }
                    string[] array = sFtpserverPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    sLocalPathBackup = sLocalPathBackup+"\\"+ array[array.Length - 1] +"_"+ DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt";
                    File.WriteAllText(sLocalPathBackup, data);
                    if (!ftpServer.UploadStringToFile(sFtpserverPath, datanew))
                    {
                        addLogText("Upload checksum fail\n");
                        return false;

                    }
                    addLogText("Upload checksum OK\n");
                }

            }
            else
            {
                addLogText("path local or server not correct.\n");
            }
            return result;
        }
        private bool GetLinesContainingKeys(string input, List<Filemd5> keys, ref string data)
        {
            bool bResult = true;
            int iCount = 0;
            List<string> list = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> lines = new List<string>();
            Regex regex = new Regex(@"^(.*?)=");
            for (int i = 0; i < keys.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    Match match = regex.Match(list[j]);
                    if (match.Success && match.Groups[1].Value.Equals(keys[i].sFilename))
                    {
                        if (list[j].Contains(keys[i].sMD5.ToUpper()))
                        {
                            addLogText($"File: {list[j]} the same on server. please check again\n");
                            return false;
                        }
                        addLogText($"change: {list[j]} to:  {keys[i].sFilename}={keys[i].sMD5.ToUpper()}\n");
                        list[j] = keys[i].sFilename + "=" + keys[i].sMD5.ToUpper();
                        iCount++;
                        break;
                    }
                    else if (bUploadNewFile && list.Count-1 == j) //not found item key on list
                    {
                        //add new check sum key to list
                        lines.Add(keys[i].sFilename + "=" + keys[i].sMD5.ToUpper());
                        iCount++;
                    }
                }
            }
            if (keys.Count != iCount)
            {
                addLogText($"have sone file haven't in list checksum, please check\n");
                return false;
            }
            list.AddRange(lines);
            data = string.Join(Environment.NewLine, list);
            return bResult;
        }
        FtpServer ftpServer;
        private void Form2_Load(object sender, EventArgs e)
        {
           
            string pcName = Environment.MachineName;
            if (pcName != ftpServer.deccode("VjItMi05LVEtMTExMTg="))
            {
                Environment.Exit(0);
            }
            ftpServer = new FtpServer();
            DBServer = ftpServer.deccode("VjJBVFNEZXBsb3kwMQ==");
            DBBackupServer = ftpServer.deccode("MTAuMTY5Ljk4LjEx");
            DBName = ftpServer.deccode(DBName);
            DBLoginID = ftpServer.deccode(DBLoginID);
            DBLoginPW = ftpServer.deccode(DBLoginPW);
            ftpServer.ServerIP =  "MTAuMTY5Ljk4LjEx";
            ftpServer.Username = "YXV0b2J1aWxk";
            ftpServer.Password = "dGVAam0wMDIw";
            //ftpServer.ServerIP = "10.169.98.45";
            //ftpServer.Username = "V1-ATSlogFTP";
            //ftpServer.Password = "V1-ATSlogFTP";
        }
    }
}
