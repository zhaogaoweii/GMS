using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.OMSFile.Helper
{
    /// <summary>
    /// 上传票据
    /// </summary>
    public class TacketBase
    {
        /// <summary>
        /// 主机名
        /// </summary>
        public virtual string Host { get; set; }

        /// <summary>
        /// 身份验证用户名
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 身份验证密码
        /// </summary>
        public virtual string PassWord { get; set; }

        internal UploadMode mode = UploadMode.NON;//默认无上传模式
        /// <summary>
        /// 上传模式
        /// </summary>
        public virtual UploadMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public TacketBase()
        {

        }

        /// <summary>
        /// 实例化上传票据实体
        /// </summary>
        /// <param name="mode"></param>
        public TacketBase(UploadMode mode)
        {
            this.mode = mode;
        }

        /// <summary>
        /// 实例化上传票据实体
        /// </summary>
        /// <param name="host">主机名</param>
        /// <param name="name">登录用户名</param>
        /// <param name="pwd">登录密码</param>
        /// <param name="mode">上传模式</param>
        public TacketBase(string host, string name, string pwd, UploadMode mode)
        {
            this.Host = host;
            this.UserName = name;
            this.PassWord = pwd;
            this.mode = mode;
        }
    }

    /// <summary>
    /// FTP远程上传票据信息
    /// </summary>
    public class FtpTacket : TacketBase
    {
        private int port = 21;

        /// <summary>
        /// 远程连接FTP端口 
        /// </summary>
        /// <remarks>
        /// 允许用户自定义
        /// </remarks>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// 远程连接FTP端口 
        /// </summary>
        /// <param name="host">FTP主机或域名</param>
        /// <param name="name">登录用户名</param>
        /// <param name="pwd">登录密码</param>
        public FtpTacket(string host, string name, string pwd)
            : base(host, name, pwd, UploadMode.FTP)
        {
        }

        /// <summary>
        /// 自定义端口FTP上传
        /// </summary>
        /// <param name="host">FTP主机或域名</param>
        /// <param name="name">登录用户名</param>
        /// <param name="pwd">登录密码</param>
        /// <param name="port">自定义端口</param>
        public FtpTacket(string host, string name, string pwd, int port)
            : base(host, name, pwd, UploadMode.FTP)
        {
            this.port = port;
        }
    }

    /// <summary>
    /// UNC共享目录上传票据信息
    /// </summary>
    public class UncTacket : TacketBase
    {
        /// <summary>
        /// 远程连接FTP端口 
        /// </summary>
        /// <param name="host">FTP主机或域名</param>
        /// <param name="name">登录用户名</param>
        /// <param name="pwd">登录密码</param>
        public UncTacket(string host, string name, string pwd)
            : base(host, name, pwd, UploadMode.UNC)
        {
        }
    }

    /// <summary>
    /// 本地上传的票据信息
    /// </summary>
    public class LocTacket : TacketBase
    {
        public LocTacket()
            : base(UploadMode.LOC)
        {

        }
    }
}
