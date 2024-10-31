using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace VNCConnect
{
    class FtpServer
    {
        string serverIp = "";
        private string username = "";
        private string password = "";
        private string smbPath = "";
       
        [DllImport("Kernel32.dll")]
        private static extern int GetPrivateProfileString([MarshalAs(UnmanagedType.LPStr)] string lpAppName, [MarshalAs(UnmanagedType.LPStr)] string lpKeyName, [MarshalAs(UnmanagedType.LPStr)] string lpDefault, StringBuilder lpReturnedString, int nSize, [MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("Kernel32.dll")]
        private static extern bool WritePrivateProfileString([MarshalAs(UnmanagedType.LPStr)] string lpAppName, [MarshalAs(UnmanagedType.LPStr)] string lpKeyName, [MarshalAs(UnmanagedType.LPStr)] string lpString, [MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        public  string ReadINI(string Path, string FileName, string AppName, string KeyName, string Default, int Length = 1024)
        {
            try
            {
                FileName = Path + "\\" + FileName + ".ini";
                StringBuilder stringBuilder = new StringBuilder(Length);
                GetPrivateProfileString(AppName, KeyName, Default, stringBuilder, Length, FileName);
                return stringBuilder.ToString(0, stringBuilder.Length);
            }
            catch (Exception)
            {

                return "";
            }
        }
        public  void WriteINI(string Path, string FileName, string AppName, string KeyName, string lpString)
        {
            FileName = Path + "\\" + FileName + ".ini";
            WritePrivateProfileString(AppName, KeyName, lpString, FileName);
        }
        public string Username
        {
            get { return username; }
            set { username = deccode( value); }
        }
       
        public string SmbPath
        {
            get { return smbPath; }
            set { smbPath = $@"\\{value}\"; }
        }
        public string ServerIP
        {
            get { return serverIp; }
            set { serverIp = deccode(value); }
        }

        // Property for Password
        public string Password
        {
            get { return password; }
            set { password = deccode(value); }
        }
        public string deccode(string strdata)
        {
            return Deccode(strdata);
        }
        private string Deccode(string strdata)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(strdata));
        }
        public bool FtpFileExist(string DirectoryPath)
        {
            bool IsExist = false;
            try
            {

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(DirectoryPath);
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                request.Credentials = new NetworkCredential(username, password);
                using (FtpWebResponse responeList = (FtpWebResponse)request.GetResponse())
                {
                    return IsExist = true;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse responeList = (FtpWebResponse)ex.Response;
                    if (responeList.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                }

            }
            return IsExist;
        }
        public bool FtpFileUpload(string DirectoryPathLocal, string DirectoryPathServer)
        {
            bool IsUpload = false;
            try
            {
                using (var client = new WebClient())
                {

                    client.Credentials = new NetworkCredential(username, password);
                    client.UploadFile(DirectoryPathServer, WebRequestMethods.Ftp.UploadFile, DirectoryPathLocal);
                    IsUpload = true;
                }
            }
            catch (WebException ex)
            {
                IsUpload = false;

            }
            return IsUpload;
        }
        public bool FtpFileUpload(string DirectoryPathServer, byte[] data)
        {
            bool IsCreate = false;
            try
            {

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(DirectoryPathServer);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(username, password);
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
                var respon = (FtpWebResponse)request.GetResponse();
                IsCreate = true;
            }
            catch (WebException ex)
            {
                IsCreate = false;

            }
            return IsCreate;
        }
        public bool FtpFolderUpload(string DirectoryPathLocal, string DirectoryPathServer)
        {
            bool IsUpload = false;
            try
            {
                IEnumerable<FileSystemInfo> infor = new DirectoryInfo(DirectoryPathLocal).EnumerateFileSystemInfos();
                foreach (FileSystemInfo info in infor)
                {
                    if (info.Attributes.HasFlag(FileAttributes.Directory))
                    {
                        string subPath = DirectoryPathServer + "/" + info.Name;
                        if (!FtpDirectoryExist(subPath))
                        {
                            FtpDirectoryCreate(subPath);
                        }
                        FtpFolderUpload(info.FullName, DirectoryPathServer + "/" + info.Name);
                    }
                    else
                    {
                        FtpFileUpload(Path.GetDirectoryName(info.FullName) + "\\" + info.Name, DirectoryPathServer + "/" + info.Name);
                    }
                }
                IsUpload = true;

            }
            catch (WebException ex)
            {
                IsUpload = false;

            }
            return IsUpload;
        }
        private void FtpFileDownload(string pathServer, string pathLocal, string line, DateTime? dateLastModify)
        {
            //download file
            try
            {
                // pathServer = "ftp://" + serverIp + pathServer.Replace("\\", "/");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathServer + "/" + line);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);

                FtpWebResponse respone = (FtpWebResponse)request.GetResponse();
                Stream resStream = respone.GetResponseStream();
                Directory.CreateDirectory(pathLocal + "/");
                string name = line.Replace(".exx", ".exe");

                FileStream writeStream = new FileStream(pathLocal + "/" + line, FileMode.Create);
                int length = 2048;
                Byte[] buffer = new Byte[length];
                int byteRead = resStream.Read(buffer, 0, length);
                while (byteRead > 0)
                {
                    writeStream.Write(buffer, 0, byteRead);
                    byteRead = resStream.Read(buffer, 0, length);
                }
                writeStream.Close();
                respone.Close();
                File.Move(pathLocal + "/" + line, pathLocal + "/" + name);
                if (dateLastModify.HasValue)
                {
                    File.SetLastWriteTime(pathLocal + "/" + name, dateLastModify.Value);
                }
            }
            catch (Exception ex)
            { }
        }
        private void FtpFileDownload(string pathServer, string pathLocal, DateTime? dateLastModify)
        {
            //download file
            try
            {
                // pathServer = "ftp://" + serverIp + pathServer.Replace("\\", "/");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathServer);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);

                FtpWebResponse respone = (FtpWebResponse)request.GetResponse();
                Stream resStream = respone.GetResponseStream();
                FileStream writeStream = new FileStream(pathLocal, FileMode.Create);
                int length = 2048;
                Byte[] buffer = new Byte[length];
                int byteRead = resStream.Read(buffer, 0, length);
                while (byteRead > 0)
                {
                    writeStream.Write(buffer, 0, byteRead);
                    byteRead = resStream.Read(buffer, 0, length);
                }
                writeStream.Close();
                respone.Close();
                if (dateLastModify.HasValue)
                {
                    File.SetLastWriteTime(pathLocal, dateLastModify.Value);
                }
            }
            catch (Exception ex)
            { }
        }
        public void FtpFileDownLoadApp(string pathServer, string pathLocal)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathServer);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);

                FtpWebResponse respone = (FtpWebResponse)request.GetResponse();
                Stream resStream = respone.GetResponseStream();
                if (File.Exists(pathLocal.Replace(".exe", ".exx")))
                {
                    File.Delete(pathLocal.Replace(".exe", ".exx"));

                }
                FileStream writeStream = new FileStream(pathLocal.Replace(".exe", ".exx"), FileMode.Create);
                int length = 2048;
                Byte[] buffer = new Byte[length];
                int byteRead = resStream.Read(buffer, 0, length);
                while (byteRead > 0)
                {
                    writeStream.Write(buffer, 0, byteRead);
                    byteRead = resStream.Read(buffer, 0, length);
                }
                writeStream.Close();
                respone.Close();
                File.Move(pathLocal.Replace(".exe", ".exx"), pathLocal);
            }
            catch (Exception ex)
            { }

        }
        private async Task FtpFileDownloadAsync(string pathServer, string pathLocal, string line, DateTime? dateLastModify)
        {
            // Download file
            try
            {
                // pathServer = "ftp://" + serverIp + pathServer.Replace("\\", "/");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathServer + "/" + line);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);

                using (FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync())
                using (Stream resStream = response.GetResponseStream())
                {
                    Directory.CreateDirectory(pathLocal);

                    string name = line.Replace(".exx", ".exe");
                    string localFilePath = Path.Combine(pathLocal, line);
                    string renamedFilePath = Path.Combine(pathLocal, name);

                    using (FileStream writeStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        byte[] buffer = new byte[2048];
                        int byteRead;

                        while ((byteRead = await resStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await writeStream.WriteAsync(buffer, 0, byteRead);
                        }
                    }

                    // Rename the file
                    File.Move(localFilePath, renamedFilePath);

                    // Set last write time if provided
                    if (dateLastModify.HasValue)
                    {
                        File.SetLastWriteTime(renamedFilePath, dateLastModify.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed (e.g., logging)
            }
        }
        public bool FtpFileDel(string DirectoryPathServer)
        {
            bool IsCreate = false;
            try
            {
                // DirectoryPathServer = "ftp://" + serverIp + DirectoryPathServer.Replace("\\", "/");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(DirectoryPathServer);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.UsePassive = true;
                request.Credentials = new NetworkCredential(username, password);

                using (FtpWebResponse responeList = (FtpWebResponse)request.GetResponse())
                {
                    IsCreate = responeList.StatusCode == FtpStatusCode.CommandOK || responeList.StatusCode == FtpStatusCode.FileActionOK;
                }
            }
            catch (WebException ex)
            {
                IsCreate = false;

            }
            return IsCreate;
        }
        public bool FtpFileMove(string DirectoryPathServer, string DirectoryPathBackup)
        {
            //DownloadData(string address);
            bool IsUpload = false;
            try
            {
                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(username, password);
                    byte[] data = client.DownloadData(DirectoryPathServer);
                    byte[] data1 = client.UploadData(DirectoryPathBackup, data);
                    IsUpload = true;
                }
            }
            catch (WebException ex)
            {
                IsUpload = false;

            }
            return IsUpload;


        }

        public bool FtpDirectoryCreate(string DirectoryPath)
        {
            bool IsCreate = false;
            try
            {
                //  DirectoryPath = "ftp://" + serverIp + DirectoryPath.Replace("\\", "/");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(DirectoryPath);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;

                request.Credentials = new NetworkCredential(username, password);
                using (FtpWebResponse responeList = (FtpWebResponse)request.GetResponse())
                {
                    IsCreate = true;
                }
            }
            catch (WebException ex)
            {
                IsCreate = false;

            }
            return IsCreate;
        }
        public bool FtpDirectoryExist(string DirectoryPath)
        {
            bool IsExist = false;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(DirectoryPath);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                request.Credentials = new NetworkCredential(username, password);
                using (FtpWebResponse responeList = (FtpWebResponse)request.GetResponse())
                {
                    return IsExist = true;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse responeList = (FtpWebResponse)ex.Response;
                    if (responeList.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                }

            }
            return IsExist;
        }
        public DateTime? getDatetime(string pathServer)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathServer);
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                request.Credentials = new NetworkCredential(username, password);
                FtpWebResponse responeList = (FtpWebResponse)request.GetResponse();
                return responeList.LastModified;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public bool DownloadFileToString(string pathServer, ref string sResult)
        {
            bool bResult = true;
            try
            {
                using (var client = new WebClient())
                {
                    string sPath = pathServer.Substring(0, pathServer.LastIndexOf("/")) + ".txt";
                    if (FtpFileExist(sPath))
                    {
                        client.Credentials = new NetworkCredential(username, password);
                        sResult = client.DownloadString(sPath);
                    }
                    else
                    {
                        bResult = false;
                    }
                }
            }
            catch (WebException ex)
            {
                bResult = false;

            }
            return bResult;

        }
        public bool UploadStringToFile(string pathServer, string data)
        {
            bool bResult = true;
            try
            {
                using (var client = new WebClient())
                {
                    string sPath = pathServer.Substring(0, pathServer.LastIndexOf("/")) + ".txt";
                    client.Credentials = new NetworkCredential(username, password);
                    client.UploadString(sPath, data);
                }
            }
            catch (WebException ex)
            {
                bResult = false;

            }
            return bResult;

        }
    }
}
