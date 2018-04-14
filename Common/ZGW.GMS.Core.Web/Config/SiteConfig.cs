using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 站点配置信息
    /// </summary>
    public class SiteConfig
    {
        /// <summary>
        /// 默认上传文件的后辍名
        /// </summary>
        public static string FileExt
        {
            get
            {
                return GetSettingValue("FileExt");
            }
        }

        /// <summary>
        /// 默认上传文件的后辍集合
        /// </summary>
        public static List<string> FileExts
        {
            get
            {
                return !FileExt.IsNullOrEmpty()
                    ? FileExt.Split('|').Select(m => m.ToLower()).ToList()
                    : new List<string>();
            }
        }

        /// <summary>
        /// 附件文件夹
        /// </summary>
        public static string UploadFolder
        {
            get { return GetSettingValue("UploadFolder"); }
        }

        /// <summary>
        /// 附件文件夹的物理路径
        /// </summary>
        public static string UploadFolderPhysicalPath
        {
            get { return HttpContext.Current.Server.MapPath(UploadFolder); }
        }

        /// <summary>
        /// 附件的最大大小
        /// </summary>
        public static int MaxFileSize
        {
            get { return GetSettingValue("MaxFileSize").ToInt() * 1024 * 1024; }
        }

        /// <summary>
        /// 最大文件大小字符信息
        /// </summary>
        public static string MaxFileSizeText
        {
            get { return (((float)MaxFileSize) / 1024).ToString("f0") + "kb"; }
        }

        private static string GetSettingValue(string key)
        {
            var kv = SiteConfigSection.GetConfig().Settings[key];
            return kv != null ? kv.Value : null;
        }

        #region 此段代码由唐鑫添加 主要用于FTP上传组件的基础配置

        /// <summary>
        /// 应王波要求 添加默认上传模式
        /// </summary>
        public static int DefaultMode
        {
            get
            {
                return GetSettingValue("Mode").ToInt();
            }
        }

        /// <summary>
        /// <para>日期：2014年2月14日</para>
        /// <para>作者：唐鑫</para>
        /// <para>FTP服务器域名或IP</para>
        /// </summary>
        public static string FtpHost
        {
            get
            {
                string host = GetSettingValue("FtpHost");

                if (string.IsNullOrEmpty(host))
                    throw new ArgumentNullException("FtpHost");

                return host;
            }
        }

        /// <summary>
        /// <para>日期：2014年2月14日</para>
        /// <para>作者：唐鑫</para>
        /// <para>FTP服务器端口</para>
        /// </summary>
        public static int FtpPort
        {
            get
            {
                return GetSettingValue("FtpPort").ToInt();
            }
        }

        /// <summary>
        /// <para>日期：2014年2月14日</para>
        /// <para>作者：唐鑫</para>
        /// <para>FTP服务器登录用户名</para>
        /// </summary>
        public static string FtpUsername
        {
            get
            {
                string username = GetSettingValue("FtpUsername");

                if (string.IsNullOrEmpty(username))
                    throw new ArgumentNullException("FtpUsername");

                return username;
            }
        }

        /// <summary>
        /// <para>日期：2014年2月14日</para>
        /// <para>作者：唐鑫</para>
        /// <para>FTP服务器登录密码</para>
        /// </summary>
        public static string FtpPassword
        {
            get
            {
                string password = GetSettingValue("FtpPassword");

                if (string.IsNullOrEmpty(password))
                    throw new ArgumentNullException("FtpPassword");

                return password;
            }
        }

        /// <summary>
        /// 获取完整的FTP主机格式
        /// </summary>
        /// <returns></returns>
        public static string GetFtpRoot()
        {
            string port = string.Empty;
            string split = string.Empty;

            if (!FtpPort.Equals(21))
            {
                split = ":";
                port = FtpPort.ToString();
            }

            return string.Format("ftp://{0}{1}{2}", FtpHost, split, port);
        }

        #endregion

        #region 此段代码由唐鑫添加 主要用于共享目录上传组件的基础配置

        /// <summary>
        /// <para>日期：2014年2月14日</para>
        /// <para>作者：唐鑫</para>
        /// <para>Unc服务器域名或IP</para>
        /// </summary>
        public static string UncHost
        {
            get
            {
                string host = GetSettingValue("UncHost");

                if (string.IsNullOrEmpty(host))
                    throw new ArgumentNullException("UncHost");

                return host;
            }
        }

        /// <summary>
        /// <para>日期：2014年2月14日</para>
        /// <para>作者：唐鑫</para>
        /// <para>Unc服务器域名或IP</para>
        /// </summary>
        public static string UncSite
        {
            get
            {
                string host = GetSettingValue("UncSite");

                if (string.IsNullOrEmpty(host))
                    throw new ArgumentNullException("UncSite");

                return host;
            }
        }

        /// <summary>
        /// <para>日期：2014年2月14日</para>
        /// <para>作者：唐鑫</para>
        /// <para>Unc服务器登录用户名</para>
        /// </summary>
        public static string UncUsername
        {
            get
            {
                string username = GetSettingValue("UncUsername");

                if (string.IsNullOrEmpty(username))
                    throw new ArgumentNullException("UncUsername");

                return username;
            }
        }

        /// <summary>
        /// <para>日期：2014年2月14日</para>
        /// <para>作者：唐鑫</para>
        /// <para>Unc服务器登录密码</para>
        /// </summary>
        public static string UncPassword
        {
            get
            {
                string password = GetSettingValue("UncPassword");

                if (string.IsNullOrEmpty(password))
                    throw new ArgumentNullException("UncPassword");

                return password;
            }
        }
         
        #endregion

        /// <summary>
        /// <para>日期：2014年2月14日</para>
        /// <para>作者：唐鑫</para>
        /// <para>Unc服务器域名或IP</para>
        /// </summary>
        public static string[] PreviewExts
        {
            get
            {
                string prevExt = GetSettingValue("PreviewExt");

                if (string.IsNullOrEmpty(prevExt))
                    throw new ArgumentNullException("PreviewExt");

                return prevExt.Split('|');
            }
        }
    }
}
