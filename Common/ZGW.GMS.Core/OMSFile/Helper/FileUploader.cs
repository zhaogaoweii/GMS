using ZGW.GMS.Core.OMSFile.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.OMSFile.Helper
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class FileUploader
    {
        private static readonly OMSFileLogic fileLogic = new OMSFileLogic();

        private ZGW.GMS.Core.OMSFile.Model.AttachmentInfo attachmentInfo;
        /// <summary>
        /// 上传或下载的附件信息
        /// </summary>
        public ZGW.GMS.Core.OMSFile.Model.AttachmentInfo AttachmentInfo
        {
            get { return attachmentInfo; }
            set { attachmentInfo = value; }
        }

        private TacketBase tacketBase = null;
        /// <summary>
        /// 上传身份验证信息
        /// </summary>
        public TacketBase TacketBase
        {
            get { return tacketBase; }
            set { tacketBase = value; }
        }

        private bool fileUnExists = false;

        public bool FileUnExists
        {
            get { return fileUnExists; }
            set { fileUnExists = value; }
        }

        public FileUploader()
        {

        }

        public FileUploader(TacketBase tacketBase)
        {
            if (tacketBase == null)
                throw new ArgumentNullException("tacketBase", "tacketBase must be not null,please fix error");

            this.tacketBase = tacketBase;
        }

        public ZGW.GMS.Core.OMSFile.Model.AttachmentInfo Upload()
        {
            return this.Upload(this.attachmentInfo);
        }

        public ZGW.GMS.Core.OMSFile.Model.AttachmentInfo Upload(ZGW.GMS.Core.OMSFile.Model.AttachmentInfo info, bool isMutiple = false)
        {
            if (info == null)
                throw new ArgumentNullException("attachmentInfo", "attachmentInfo must be not null,please fix error");

            if (this.tacketBase == null)
                throw new ArgumentNullException("tacketBase", "tacketBase must be not null,please fix error");

            if (info.Items != null)
            {
                if (info.Items == null)
                    throw new ArgumentNullException("attachmentInfo.Items must be not null,please fix error");

                foreach (var item in info.Items)
                {
                    if (item.Id.Equals(0) && (item.Content == null || item.Content.Content == null))
                        throw new ArgumentNullException("item.Content must be not null,please fix error");

                    item.UploadMode = this.tacketBase.Mode;
                }
            }

            attachmentInfo = fileLogic.SaveOrUpdate(info, true, isMutiple);

            #region switch case

            if (attachmentInfo.Items == null || !attachmentInfo.Items.Any()) return attachmentInfo;

            switch (this.tacketBase.Mode)
            {
                case UploadMode.FTP:

                    FtpTacket ftpTacket = this.tacketBase as FtpTacket;

                    if (ftpTacket == null)
                        throw new ArgumentNullException("tacketBase");


                    using (
                        Ftp ftp = new Ftp(ftpTacket.Host, ftpTacket.UserName, ftpTacket.PassWord,
                            ftpTacket.Port.ToString()))
                    {
                        foreach (var item in this.attachmentInfo.Items)
                        {
                            if (item.IsOld) continue;

                            ftp.Directory = item.FilePath.Replace(Path.GetFileName(item.FilePath), string.Empty);
                            ftp.FileName = Path.GetFileName(item.FilePath);

                            ftp.Upload(item.Content.Content);
                        }
                    }

                    break;
                case UploadMode.UNC:

                    using (
                        Unc unc = new Unc(this.tacketBase.UserName, this.tacketBase.PassWord, this.tacketBase.Host))
                    {
                        foreach (var item in this.attachmentInfo.Items)
                        {
                            //string uncDirectory = this.tacketBase.Host + "\\" + item.FilePath.Replace(Path.GetFileName(item.FilePath), string.Empty).TrimStart('/').Replace('/', '\\');
                            if (item.IsOld) continue;

                            string uncFullPath = this.tacketBase.Host + "\\" +
                                                 item.FilePath.TrimStart('/').Replace('/', '\\');

                            EnsureDirectory(uncFullPath);

                            File.WriteAllBytes(uncFullPath, item.Content.Content);
                        }
                    }

                    break;
                    //case UploadMode.LOC:
                default:

                    foreach (var item in this.attachmentInfo.Items)
                    {
                        //string localDirectory = this.tacketBase.Host + "\\" + item.FilePath.Replace(Path.GetFileName(item.FilePath), string.Empty).TrimStart('/').Replace('/', '\\');
                        if (item.IsOld) continue;

                        string localFullPath = this.tacketBase.Host + "\\" +
                                               item.FilePath.TrimStart('/').Replace('/', '\\');

                        EnsureDirectory(localFullPath);

                        File.WriteAllBytes(localFullPath, item.Content.Content);
                    }
                    break;
            }

            #endregion

            return attachmentInfo;
        }

        private void EnsureDirectory(string fullFilePath)
        {
            var parent = Directory.GetParent(fullFilePath);
            if (!parent.Exists)
            {
                parent.Create();
            }
        }

        public ZGW.GMS.Core.OMSFile.Model.AttachmentItem Download(int attachmentItemId, bool useBrinary)
        {
            if (attachmentItemId <= 0)
                throw new ArgumentOutOfRangeException("attachmentId");

            if (this.tacketBase == null)
                throw new ArgumentNullException("tacketBase", "tacketBase must be not null,please fix error");

            var attachmentItem = fileLogic.GetAttachmentItem(attachmentItemId, true);

            switch (attachmentItem.UploadMode)
            {
                case UploadMode.FTP:

                    FtpTacket ftpTacket = this.tacketBase as FtpTacket;

                    if (ftpTacket == null)
                        throw new ArgumentNullException("tacketBase");

                    using (Ftp ftp = new Ftp(ftpTacket.Host, ftpTacket.UserName, ftpTacket.PassWord, ftpTacket.Port.ToString()))
                    {
                        ftp.Directory = attachmentItem.FilePath.Replace(Path.GetFileName(attachmentItem.FilePath), string.Empty);
                        ftp.FileName = Path.GetFileName(attachmentItem.FilePath);

                        if (attachmentItem.Content == null)
                            attachmentItem.Content = new Model.AttachmentContent();
                        attachmentItem.Content.Content = ftp.Download();
                    }

                    break;
                case UploadMode.UNC:

                    using (Unc unc = new Unc(this.tacketBase.UserName, this.tacketBase.PassWord, this.tacketBase.Host))
                    {
                        string uncFullPath = this.tacketBase.Host + "\\" + attachmentItem.FilePath.TrimStart('/').Replace('/', '\\');

                        EnsureDirectory(uncFullPath);

                        if (!File.Exists(uncFullPath))
                        {
                            fileUnExists = true;
                            return null;
                        }

                        if (attachmentItem.Content == null)
                            attachmentItem.Content = new Model.AttachmentContent();
                        attachmentItem.Content.Content = File.ReadAllBytes(uncFullPath);
                    }

                    break;
                case UploadMode.LOC:
                default:
                    string localFullPath = this.tacketBase.Host + "\\" + attachmentItem.FilePath.TrimStart('/').Replace('/', '\\');

                    EnsureDirectory(localFullPath);

                    if (!File.Exists(localFullPath))
                    {
                        fileUnExists = true;
                        return null;
                    }

                    if (attachmentItem.Content == null)
                        attachmentItem.Content = new Model.AttachmentContent();
                    attachmentItem.Content.Content = File.ReadAllBytes(localFullPath);
                    break;
            }

            return attachmentItem;
        }
    }
}
