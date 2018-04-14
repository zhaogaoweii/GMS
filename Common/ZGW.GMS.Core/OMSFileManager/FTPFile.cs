using System;
using System.Net;
using System.IO;
using System.Text;

namespace ZGW.GMS.Core.OMSFileManager
{
    /// <summary>
    /// FTP文件管理器
    /// </summary>
    public class FTPFile
    {
        private string ftpServerIP = "127.0.0.1";//服务器ip
        private string ftpUserID = "";//用户名
        private string ftpPassword = "";//密码
        private string result;

        /// <summary>
        /// 结果描述：
        ///     正确：空
        ///     错误：错误描述
        /// </summary>
        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        public FTPFile(string _ftpServerIP, string _ftpUserID, string _ftpPassword)
        {
            ftpServerIP = _ftpServerIP;
            ftpUserID = _ftpUserID;
            ftpPassword = _ftpPassword;
        }

        /// <summary>
        /// 实现文件上传
        /// </summary>
        /// <param name="filename">本地文件的全路径名称</param>
        public bool Upload(string fullFileName)
        {
            FileInfo fileInfo = new FileInfo(fullFileName);
            string uri = "ftp://" + ftpServerIP + "/" + fileInfo.Name;
            FtpWebRequest ftpWebRequest;
            // 根据uri创建FtpWebRequest对象 
            ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileInfo.Name));
            // ftp用户名和密码
            ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            // 默认为true，连接不会被关闭
            // 在一个命令之后被执行
            ftpWebRequest.KeepAlive = false;
            // 指定执行什么命令
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
            // 指定数据传输类型
            ftpWebRequest.UseBinary = true;
            // 上传文件时通知服务器文件的大小
            ftpWebRequest.ContentLength = fileInfo.Length;
            // 缓冲大小设置为2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件
            FileStream fs = fileInfo.OpenRead();
            try
            {
                // 把上传的文件写入流
                Stream stream = ftpWebRequest.GetRequestStream();
                // 每次读文件流的2kb
                contentLen = fs.Read(buff, 0, buffLength);
                // 流内容没有结束
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream
                    stream.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                // 关闭两个流
                stream.Close();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 从FTP服务器下载文件
        /// </summary>
        /// <param name="filePath">要保存的文件路径（注意中间路径的分隔符为\\，且结尾必须要有\\）</param>
        /// <param name="fileName">要保存的文件名</param>
        public bool Download(string filePath, string fileName)
        {
            FtpWebRequest ftpWebRequest;
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));
                ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 从ftp服务器上获得文件列表
        /// </summary>
        /// <returns>文件列表</returns>
        public string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder sbResult = new StringBuilder();
            FtpWebRequest ftpWebRequest;
            try
            {
                ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = ftpWebRequest.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    sbResult.Append(line);
                    sbResult.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'        
                result.Remove(sbResult.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return sbResult.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                result = ex.Message;
                return downloadFiles;
            }
        }
    }
}
