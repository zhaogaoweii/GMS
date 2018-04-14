using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace ZGW.GMS.Core.OMSFile.Helper
{
    #region FTP远程操作类
    /// <summary>
    /// FTP远程操作类
    /// </summary>
    /// <remarks>
    /// 2014-02-13
    /// </remarks>
    public sealed class Ftp : IDisposable
    {
        #region .ctor

        /// <summary>
        /// 无参私有构造函数
        /// </summary>
        private Ftp()
        {

        }

        /// <summary>
        /// 根据FTP服务器主机地址、用户名、登录密码、端口号等初始化FTP远程信息
        /// </summary>
        /// <param name="host">FTP服务器主机地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">登录密码</param>
        /// <param name="fileName">上传的文件名称</param>
        /// <param name="port">端口 默认21</param>
        public Ftp(string host, string username, string password, string port = "")
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException("parameter host must be not null,please fix error.");

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("parameter username must be not null,please fix error.");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("parameter password must be not null,please fix error.");

            this.host = host;
            this.username = username;
            this.password = password;

            if (!string.IsNullOrEmpty(port))
                this.port = port;
        }

        /// <summary>
        /// 根据资源定位符、用户名、密码初始化FTP
        /// </summary>
        /// <param name="ftpUri">资源定位符</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public Ftp(Uri ftpUri, string username, string password)
        {
            if (ftpUri == null)
                throw new ArgumentNullException("parameter ftpUri must be not null,please fix error.");

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("parameter username must be not null,please fix error.");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("parameter password must be not null,please fix error.");

            this.ftpUri = ftpUri;
            this.username = username;
            this.password = password;
        }
        #endregion

        #region const
        /// <summary>
        /// FTP路径格式化
        /// <para>
        /// ftp://{IP地址}{用于端口的:}{端口号 默认21}/{自定义上传目录路径 可以有多级}{上传的远程文件名称}
        /// </para>
        /// </summary>
        private static readonly string FTP_FORMAT = "ftp://{0}{1}{2}{3}/{4}";
        #endregion

        #region fields
        private bool disposed = false;

        private int bufferSize = 2048;

        private string host = string.Empty;

        private string port = string.Empty;

        private string username = string.Empty;

        private string password = string.Empty;

        private Uri ftpUri = null;

        private byte[] uploadBuffer = null;

        private string spliter = ":";

        private string fileName = string.Empty;

        private static readonly string pathPattern = @"(/{0,1}[\w]+[/]{0,1})";

        private string directory = string.Empty;

        /// <summary>
        /// 用于目录检查的存储队列
        /// </summary>
        private Queue<string> directoryQueue = new Queue<string>(4);

        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpRespose = null;
        #endregion

        #region properties

        /// <summary>
        /// 远程FTP服务器IP地址
        /// </summary>
        public string Host
        {
            get
            {
                return this.host;
            }
            set
            {
                this.host = value;
            }
        }

        /// <summary>
        /// 远程FTP服务器连接端口
        /// </summary>
        public string Port
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
            }
        }

        /// <summary>
        /// 远程FTP服务器登录口令
        /// </summary>
        public string UserName
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        /// <summary>
        /// 远程FTP服务器登录密码
        /// </summary>
        public string PassWord
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        /// <summary>
        /// 远程FTP连接统一资源定位符
        /// </summary>
        public Uri FtpUri
        {
            get
            {
                return this.ftpUri;
            }
            set
            {
                this.ftpUri = value;
            }
        }

        /// <summary>
        /// 二进制上传资源
        /// </summary>
        public byte[] UploadBuffer
        {
            get
            {
                return this.uploadBuffer;
            }
            set
            {
                this.uploadBuffer = value;
            }
        }

        /// <summary>
        /// 端口号分隔符
        /// </summary>
        private string Spliter
        {
            get
            {
                return this.spliter;
            }
            set
            {
                this.spliter = value;
            }
        }

        /// <summary>
        /// FTP上传文件名称
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        /// <summary>
        /// 自定义上传路径 可以多级 例如：/Upload/User/Photo/
        /// </summary>
        public string Directory
        {
            get
            {
                return this.directory;
            }
            set
            {
                this.directory = value;
            }
        }

        #endregion

        #region private methods
        private string FtpPathFormat()
        {
            if (!string.IsNullOrEmpty(this.directory))
            {
                Regex regex = new Regex(pathPattern);

                if (!regex.IsMatch(this.directory))
                {
                    throw new ArgumentException("customPath format is faild.please fix error.");
                }
                else
                {
                    this.directory = this.directory.TrimStart('/');
                    this.directory = this.directory.TrimEnd('/');

                    this.directory = "/" + this.directory;
                }
            }

            //this.Spliter = (!string.IsNullOrEmpty(this.port.Trim()) && !this.port.Trim().Equals("21")) ? ":" : string.Empty;


            return string.Format(FTP_FORMAT, this.host, this.spliter, this.port, this.directory, this.fileName);
        }

        /// <summary>
        /// 上传执行方法
        /// </summary>
        /// <returns></returns>
        private bool Uploading()
        {
            try
            {
                Uri cUri;
                PingReply replyStatus;
                Init(out cUri, out replyStatus);

                #region 只要PING通的机器 才能继续用FTP远程连接并上传
                if (replyStatus.Status == IPStatus.Success)
                {
                    #region 如果客户端直接调用URI资源 则需要提取出目录 因为FTP服务器上不一定有用户指定的目录存在
                    DirectoryLoop(cUri);
                    #endregion

                    this.ftpUri = cUri;
                    this.ftpRequest = FtpWebRequest.Create(this.ftpUri) as FtpWebRequest;

                    if (this.ftpRequest != null)
                    {
                        this.ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                        this.ftpRequest.UseBinary = true;
                        this.ftpRequest.Credentials = new NetworkCredential(this.username, this.password);

                        using (Stream ftpStream = ftpRequest.GetRequestStream())
                        {
                            ftpStream.Write(this.uploadBuffer, 0, this.uploadBuffer.Length);
                            ftpStream.Flush();
                        }

                        return true;//上传成功
                    }
                }
                #endregion
            }

            #region 捕获并抛出各类异常 由应用层自定义异常处理机制
            catch (ProtocolViolationException pvException)
            {
                throw pvException;
            }
            catch (UriFormatException ufException)
            {
                throw ufException;
            }
            catch (WebException webException)
            {
                throw webException;
            }
            catch (InvalidOperationException ioException)
            {
                throw ioException;
            }
            catch (IOException ioException)
            {
                throw ioException;
            }
            #endregion

            return false;
        }

        private void Init(out Uri cUri, out PingReply replyStatus)
        {
            #region 提取并检查各种初始化参数 组合成FTP统一资源定位符
            cUri = null;

            if (this.ftpUri != null)
            {
                cUri = this.ftpUri;
            }
            else
            {
                string ftpFullPath = this.FtpPathFormat();
                if (!string.IsNullOrEmpty(ftpFullPath))
                {
                    cUri = new Uri(ftpFullPath);
                }
                else
                {
                    throw new ArgumentNullException("ftp upload uri must be not null,please fix error.");
                }
            }
            #endregion

            #region 在FTP连接之前 先进行IP检查 如果远程主机无法PING通 则不再继续处理

            Ping reply = new Ping();

            string domain = GetDomain(cUri);

            this.host = domain;
            replyStatus = reply.Send(domain);

            #endregion
        }

        private byte[] Downloading(Uri uri)
        {
            if (uri.Scheme != Uri.UriSchemeFtp)
            {
                return null;
            }

            byte[] bufferContent = null;

            try
            {
                Uri cUri;
                PingReply replyStatus;
                Init(out cUri, out replyStatus);

                #region 只要PING通的机器 才能继续用FTP远程连接并上传
                if (replyStatus.Status == IPStatus.Success)
                {
                    #region
                    //this.ftpRequest = FtpWebRequest.Create(this.ftpUri) as FtpWebRequest;

                    //if (this.ftpRequest != null)
                    //{
                    //    this.ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                    //    this.ftpRequest.UseBinary = true;
                    //    this.ftpRequest.KeepAlive = true;
                    //    this.ftpRequest.UsePassive = true;
                    //    this.ftpRequest.Credentials = new NetworkCredential(this.username, this.password);

                    //    this.ftpRespose = (FtpWebResponse)this.ftpRequest.GetResponse();
                    //    if (this.ftpRespose != null)
                    //    {
                    //        using (Stream responseStream = this.ftpRespose.GetResponseStream())
                    //        {
                    //            using (MemoryStream memStream = new MemoryStream())
                    //            {
                    //                byte[] byteBuffer = new byte[bufferSize];
                    //                int bytesRead = responseStream.Read(byteBuffer, 0, bufferSize);
                    //                while (bytesRead > 0)
                    //                {
                    //                    memStream.Write(byteBuffer, 0, bytesRead);
                    //                    bytesRead = responseStream.Read(byteBuffer, 0, bufferSize);
                    //                }

                    //                bufferContent = memStream.ToArray();
                    //            }
                    //        }

                    //        this.ftpRespose.Close();
                    //    }
                    //}

                    #endregion

                    WebClient request = new WebClient();

                    request.Credentials = new NetworkCredential(this.username, this.password);
                    try
                    {
                        bufferContent = request.DownloadData(cUri);

                        this.ftpUri = cUri;
                    }
                    catch (WebException e)
                    {
                        throw e;
                    }
                }
                #endregion
            }
            #region 捕获并抛出各类异常 由应用层自定义异常处理机制
            catch (ProtocolViolationException pvException)
            {
                throw pvException;
            }
            catch (UriFormatException ufException)
            {
                throw ufException;
            }
            catch (WebException webException)
            {
                throw webException;
            }
            catch (InvalidOperationException ioException)
            {
                throw ioException;
            }
            catch (IOException ioException)
            {
                throw ioException;
            }
            #endregion

            return bufferContent;
        }

        #endregion

        #region public methods

        /// <summary>
        /// 上传二进制文件
        /// </summary>
        /// <returns>上传结果</returns>
        public bool Upload()
        {
            return this.Upload(this.uploadBuffer);
        }

        /// <summary>
        /// 上传二进制文件
        /// </summary>
        /// <param name="content">二进制文件</param>
        /// <returns>上传结果</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="ProtocolViolationException">ProtocolViolationException</exception>
        /// <exception cref="UriFormatException">UriFormatException</exception>
        /// <exception cref="WebException">WebException</exception>
        /// <exception cref="InvalidOperationException">InvalidOperationException</exception>
        /// <exception cref="IOException">IOException</exception>
        public bool Upload(byte[] content)
        {
            if (content == null)
                throw new ArgumentNullException("upload buffer data must be not null,please fix error.");

            this.uploadBuffer = content;

            return Uploading();
        }

        public byte[] Download()
        {
            string url = this.FtpPathFormat();

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("uri");

            Uri cUri = new Uri(url);

            return Download(cUri);
        }

        public byte[] Download(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("ftp download uri must be not null,please fix error.");

            return this.Downloading(uri);
        }

        public Stream GetStream(Uri uri)
        {
            byte[] buffer = this.Downloading(uri);

            MemoryStream stream = new MemoryStream();

            stream.Write(buffer, 0, buffer.Length);

            return stream;
        }

        private static string GetDomain(Uri cUri)
        {
            string domain = cUri.Authority;

            int flag = cUri.Authority.IndexOf(':');

            if (flag >= 0)
            {
                domain = cUri.Authority.Remove(flag);
            }
            return domain;
        }

        /// <summary>
        /// 创建指定的目录
        /// </summary>
        /// <param name="uri"></param>
        public bool CreateDirectory(Uri uri)
        {
            this.ftpRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
            this.ftpRequest.KeepAlive = true;
            this.ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            this.ftpRequest.Credentials = new NetworkCredential(this.username, this.password);
            FtpWebResponse response = (FtpWebResponse)this.ftpRequest.GetResponse();

            bool bl = response.StatusCode == FtpStatusCode.PathnameCreated;

            response.Close();
            response = null;

            return bl;
        }

        private void DirectoryLoop(Uri uri)
        {
            //从根目录的下一级目录开始检查 直接目录检查结束

            string filename = Path.GetFileName(uri.AbsolutePath);
            string realPath = uri.AbsolutePath.Remove(uri.AbsolutePath.IndexOf(filename));

            if ((string.IsNullOrEmpty(realPath)) || (!string.IsNullOrEmpty(realPath) && realPath.Trim().Equals("/")))
                return;

            //移除前导 后导路径分割符 以减少分隔后的数组容量
            string[] allDirectory = realPath.TrimStart('/').TrimEnd('/').Split('/');

            //将各级目录名称 压入队列
            foreach (var item in allDirectory)
            {
                if (!string.IsNullOrEmpty(item))
                    this.directoryQueue.Enqueue(item);
            }

            string flagPath = "ftp://" + this.host;

            while (this.directoryQueue.Count > 0)
            {
                string prevPath = flagPath + "/";

                string[] existsDirectoryList = DirectoryList(new Uri(prevPath));

                string currentPath = this.directoryQueue.Dequeue();

                flagPath += "/" + currentPath;

                if (!existsDirectoryList.Contains(currentPath))
                {
                    CreateDirectory(new Uri(flagPath));
                }
            }
        }

        public string[] DirectoryList(Uri uri)
        {
            string[] directoryDetailList = DirectoryListDetail(uri);

            string[] dirList = directoryDetailList.Where(o => o.IndexOf("<DIR>") > -1).ToArray();

            List<string> newDirectoryList = new List<string>(dirList.Length);

            if (dirList == null || dirList.Count() == 0)
            {
                dirList = directoryDetailList.Where(o => o.IndexOf("drwxr-xr-x") > -1).ToArray();
            }

            if (dirList != null && dirList.Count() > 0)
            {
                string[] tempArray = new string[dirList.Count()];
                for (int i = 0; i < dirList.Count(); i++)
                {
                    string[] itemArray = dirList[i].Split(' ');
                    if (itemArray.Count() > 0)
                    {
                        newDirectoryList.Add(itemArray[itemArray.Count() - 1]);
                    }
                }
            }

            return newDirectoryList.ToArray();
        }

        /// <summary>
        /// 获取完整的相对路径
        /// </summary>
        /// <returns></returns>
        public string GetFullDirectoryPath()
        {
            return this.ftpUri.AbsolutePath;
        }

        /// <summary>
        /// 列出指定资源定位符的目录详细列表
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public string[] DirectoryList2(Uri uri)
        {
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(uri);

            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            ftpRequest.Credentials = new NetworkCredential(this.username, this.password);

            ftpRespose = (FtpWebResponse)ftpRequest.GetResponse();
            Stream responseStream = ftpRespose.GetResponseStream();

            StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.UTF8);

            List<string> directoryList = new List<string>();
            while (streamReader.Peek() != -1)
            {
                directoryList.Add(streamReader.ReadLine());
            }

            return directoryList.ToArray();
        }

        /// <summary>
        /// 列出指定资源定位符的目录详细列表
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public string[] DirectoryListDetail(Uri uri)
        {
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(uri);

            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            //ftpRequest.Credentials = new NetworkCredential("ftp01", "yzgjtsjt_881202");
            ftpRequest.Credentials = new NetworkCredential(this.username, this.password);
            ftpRequest.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Anonymous;
            ftpRequest.KeepAlive = true;
            ftpRequest.UseBinary = true;
            ftpRequest.Timeout = 10000;

            ftpRespose = (FtpWebResponse)ftpRequest.GetResponse();
            Stream responseStream = ftpRespose.GetResponseStream();

            StreamReader streamReader = new StreamReader(responseStream, Encoding.Default);

            List<string> directoryList = new List<string>();
            while (streamReader.Peek() != -1)
            {
                directoryList.Add(streamReader.ReadLine());
            }
            ftpRespose.Close();
            ftpRespose.Dispose();
            return directoryList.ToArray();
        }
        #endregion

        #region Dispose
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!this.disposed)
            {
                this.directoryQueue.Clear();

                this.ftpUri = null;

                this.ftpRequest = null;

                if (this.ftpRespose != null)
                    this.ftpRespose.Close();

                this.ftpRespose = null;

                disposed = true;
            }
        }
        #endregion
    }
    #endregion
}
