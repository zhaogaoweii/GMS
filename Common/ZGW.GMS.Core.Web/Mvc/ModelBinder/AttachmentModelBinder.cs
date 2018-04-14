using Common.Core.OMSFile.Helper;
using Common.Core.OMSFile.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAttachmentInfo = Common.Core.OMSFile.Model.AttachmentInfo;

namespace Common.Core.Mvc
{
    /// <summary>
    /// AttachmentInfo的模型绑定
    /// </summary>
    public class AttachmentModelBinder : IModelBinder
    {
        private IAttachmentService AttachmentService
        {
            get { return ObjectContainer.ResolveService<IAttachmentService>(); }
        }

        /// <summary>
        /// 用指定的控制器上下文和绑定上下文将模型绑定到AttachmentInfo
        /// </summary>
        /// <param name="controllerContext">ControllerContext</param>
        /// <param name="bindingContext">ModelBindingContext</param>
        /// <returns>绑定后的数据</returns>
        /// <remarks>
        /// 应登明要求 重写此绑定上传方法 由原来的站点目录上传改写为FTP远程上传
        /// <para>
        /// 2014-02-13
        /// </para>
        /// <para>
        /// 唐鑫
        /// </para>
        /// </remarks>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            string modelName = bindingContext.ModelName;
            bool isDb = IsDB(request, modelName);
            bool isMultiple = IsMultiple(request, modelName);
            bool isRequired = IsRequired(request, modelName);
            bool isValid = true;

            string uploadDir = UploadDir(request, modelName);

            UploadMode mode = (UploadMode)IsMode(request, modelName);
            AttachmentInfo attachment = BuildAttachmentInfo(request, modelName);

            HttpFileCollectionBase files = controllerContext.HttpContext.Request.Files;
            IList<HttpPostedFileBase> modelFiles = files.GetMultiple(modelName)
                                                        .Where(m => !m.FileName.IsNullOrEmpty())
                                                        .ToList();

            if (!isMultiple && isRequired && (modelFiles.Count == 0 || modelFiles.Count(m => m.ContentLength > 0) == 0))
            {
                bindingContext.ModelState.AddModelError(modelName, "必填项");
                isValid = false;
            }


            #region 上传员工照片 做特殊处理
            if (modelFiles.Count > 0 && !string.IsNullOrEmpty(request.QueryString["empPhoto"]) && request.QueryString["empPhoto"] == "1")
            {
                if (Path.GetExtension(modelFiles[0].FileName).Trim('.').ToLower() != "jpg")
                {
                    bindingContext.ModelState.AddModelError(modelName, "仅支持如下文件格式：jpg");
                    isValid = false;
                }
                else if (modelFiles.Any(m => m.ContentLength / 1024 >= 20))
                {
                    bindingContext.ModelState.AddModelError(modelName, "超过文件大小限定:9KB ～ 20KB");
                    isValid = false;
                }
                else if (modelFiles.Any(m => m.ContentLength / 1024 < 9))
                {
                    bindingContext.ModelState.AddModelError(modelName, "超过文件大小限定:9KB ～ 20KB");
                    isValid = false;
                }
                Image pic = null;
                if (isValid)
                {
                    pic = System.Drawing.Image.FromStream(modelFiles[0].InputStream);
                }
                if (isValid && pic.Width != 358)
                {
                    bindingContext.ModelState.AddModelError(modelName, "照版的宽度必须是:358像素");
                    isValid = false;
                }
                else if (isValid && pic.Height != 441)
                {
                    bindingContext.ModelState.AddModelError(modelName, "照版的高度必须是:441像素");
                    isValid = false;
                }
            }
            #endregion

            if (modelFiles.Any(m => !SiteConfig.FileExts.Contains(Path.GetExtension(m.FileName).Trim('.').ToLower())))
            {
                bindingContext.ModelState.AddModelError(modelName, "仅支持如下文件格式：" + SiteConfig.FileExt);
                isValid = false;
            }

            if (modelFiles.Any(m => m.ContentLength > SiteConfig.MaxFileSize))
            {
                bindingContext.ModelState.AddModelError(modelName, "超过文件大小限定:" + SiteConfig.MaxFileSizeText);
                isValid = false;
            }

            if (!isValid)
            {
                return new AttachmentInfo();
            }

            if (modelFiles.Count > 0)
            {
                if (!isMultiple)
                {
                    var items = attachment.Items.Remove();
                }

                foreach (var item in modelFiles.Select(file => BuildAttachmentItem(file, isDb, uploadDir)))
                {
                    attachment.Items.Add(item);
                }
            }

            TacketBase tacketBase = null;

            switch (mode)
            {
                case UploadMode.FTP: //FTP远程上传
                    tacketBase = new FtpTacket(SiteConfig.FtpHost, SiteConfig.FtpUsername, SiteConfig.FtpPassword);
                    break;
                case UploadMode.UNC: //共享目录上传
                    tacketBase = new UncTacket(@"" + SiteConfig.UncHost, SiteConfig.UncUsername, SiteConfig.UncPassword);
                    break;
                case UploadMode.LOC: //本地上传测试
                    tacketBase = new LocTacket() { Host = SiteConfig.UploadFolderPhysicalPath };
                    break;
                default:
                    UploadMode defaultMode = (UploadMode)SiteConfig.DefaultMode;
                    switch (defaultMode)
                    {
                        case UploadMode.FTP: //FTP远程上传
                            tacketBase = new FtpTacket(SiteConfig.FtpHost, SiteConfig.FtpUsername,
                                SiteConfig.FtpPassword);
                            break;
                        case UploadMode.UNC: //共享目录上传
                            tacketBase = new UncTacket(@"" + SiteConfig.UncHost, SiteConfig.UncUsername,
                                SiteConfig.UncPassword);
                            break;
                        case UploadMode.LOC: //本地上传测试
                        default:
                            tacketBase = new LocTacket() { Host = SiteConfig.UploadFolderPhysicalPath };
                            break;
                    }
                    break;
            }
            //FileUploader fileUploader = new FileUploader(tacketBase);
            NewAttachmentInfo newInfo = AttachmentConvert.Convert(attachment);

            //if ((CheckFile(files) && isValid) && newInfo.Items != null && newInfo.Items.Any())
            //{
            //fileUploader.Upload(newInfo, isMultiple);
            var info = Upload(newInfo, isMultiple, tacketBase);
            //}

            attachment = AttachmentConvert.Convert(info);

            return attachment;

        }

        /// <summary>
        /// 构建AttachmentItem
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="isDb">是否二进制存储</param>
        /// <param name="isFtp">是否采用FTP上传</param>
        /// <returns></returns>
        /// <remarks>
        /// 2014-01-14 由唐鑫增加FTP上传扩展
        /// </remarks>
        private AttachmentItem BuildAttachmentItem(HttpPostedFileBase file, bool isDb, string dir = "", bool isFtp = true)
        {
            string fileName = Path.GetFileName(file.FileName);
            string fullName = GetFullFileName(fileName, dir);
            var extension = Path.GetExtension(fileName);
            string fileExt = string.Empty;

            if (extension != null)
            {
                fileExt = extension.Trim('.');
            }

            string contentType = file.ContentType;
            int fileSize = file.ContentLength;

            #region 原有上传代码  保留不删除 下面进行重写
            //EnsureDirectory(fullFilePath);

            ////保存附件
            //file.SaveAs(fullFilePath);
            #endregion

            AttachmentItem item = new AttachmentItem
            {
                FileName = fileName,
                FileExt = fileExt,
                FilePath = fullName,
                FileSize = fileSize,
                ContentType = contentType,
                IsDB = isDb,
            };

            byte[] content = ReadStream(file.InputStream);

            item.Content.Content = content;

            return item;
        }

        /// <summary>
        /// 是否把数据存储在DB里面
        /// </summary>
        /// <param name="request"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        private int IsMode(HttpRequestBase request, string modelName)
        {
            return request[modelName + ".Mode"].ToInt();
        }

        /// <summary>
        /// 取得完整的附件名称
        /// </summary>
        /// <returns></returns>
        private string GetFullFileName(string fileName, string dir = "")
        {
            if (string.IsNullOrWhiteSpace(dir))
            {
                fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + DateTime.Now.Ticks + Path.GetExtension(fileName);

                string rootPath = DateTime.Now.Year + "/" + DateTime.Now.ToString("yyyyMMdd");

                fileName = String.Format("{0}/{1}/{2}", rootPath, Path.GetExtension(fileName).Trim('.').ToUpper(), fileName);
                return fileName;
            }
            else
            {
                fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + DateTime.Now.Ticks + Path.GetExtension(fileName);
                fileName = String.Format("{0}/{1}", dir, fileName);
                return fileName;

            }
        }

        /// <summary>
        /// 是否把数据存储在DB里面
        /// </summary>
        /// <param name="request"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        private bool IsDB(HttpRequestBase request, string modelName)
        {
            return request[modelName + ".IsDB"].ToBoolean();
        }

        /// <summary>
        /// 是否是多文件上传
        /// </summary>
        /// <param name="request"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        private bool IsMultiple(HttpRequestBase request, string modelName)
        {
            return request[modelName + ".IsMultiple"].ToBoolean();
        }

        /// <summary>
        /// 是否是必填项
        /// </summary>
        /// <param name="request"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        private bool IsRequired(HttpRequestBase request, string modelName)
        {
            return request[modelName + ".IsRequired"].ToBoolean();
        }

        /// <summary>
        /// 执行新的目录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        private string UploadDir(HttpRequestBase request, string modelName)
        {
            string dir = "";
            if (request[modelName + ".UploadDir"] != null)
            {
                dir = request[modelName + ".UploadDir"].ToString().Trim();
            }

            return dir;
        }

        /// <summary>
        /// 确保文件夹存在
        /// </summary>
        /// <param name="fullFilePath"></param>
        private static void EnsureDirectory(string fullFilePath)
        {
            var parent = Directory.GetParent(fullFilePath);
            if (!parent.Exists)
            {
                parent.Create();
            }
        }

        /// <summary>
        /// 构建附件信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        private AttachmentInfo BuildAttachmentInfo(HttpRequestBase request, string modelName)
        {
            int infoId = request[modelName + ".InfoID"].ToInt();
            string moduleName = request[modelName + ".Module"];
            string functionName = request[modelName + ".Function"];
            string itemIds = request[modelName + ".ItemId"];

            AttachmentInfo attachment = new AttachmentInfo
            {
                Id = infoId,
                ModuleName = moduleName,
                FunctionName = functionName,
            };
            LoadExistItems(attachment, itemIds);
            return attachment;
        }

        /// <summary>
        /// 加载已经存在的记录项
        /// </summary>
        /// <param name="info"></param>
        /// <param name="itemIds"></param>
        private void LoadExistItems(AttachmentInfo info, string itemIds)
        {
            if (!itemIds.IsNullOrEmpty())
            {
                var ids = itemIds.Split(',').Select(m => m.ToInt()).Where(m => m != 0);
                foreach (var id in ids)
                {
                    info.Items.Add(new AttachmentItem { Id = id });
                }
            }
        }

        /// <summary>
        /// 检测上传文件是否正确
        /// </summary>
        /// <param name="request"></param>
        /// <param name="modelName"></param>
        private bool CheckFile(HttpFileCollectionBase files)
        {
            bool result = true;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                if (!file.FileName.IsNullOrEmpty() && !files.Keys[i].StartsWith("__"))
                {
                    string fileExt = Path.GetExtension(file.FileName).Trim('.').ToLower();
                    if (!SiteConfig.FileExts.Contains(fileExt) || file.ContentLength > SiteConfig.MaxFileSize || file.ContentLength != file.InputStream.Length)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 从流中读取数据
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        private byte[] ReadStream(Stream inputStream)
        {
            byte[] buffer = new byte[inputStream.Length];
            inputStream.Position = 0;
            inputStream.Read(buffer, 0, buffer.Length);
            inputStream.Position = 0;
            return buffer;
        }

        public static Common.Core.OMSFile.Model.AttachmentInfo Upload(Common.Core.OMSFile.Model.AttachmentInfo info, bool isMutiple, TacketBase tacketBase, bool isUpdateModel = false)
        {
            IAttachmentService attachmentService = ObjectContainer.ResolveService<IAttachmentService>();
            if (info == null)
                throw new ArgumentNullException("attachmentInfo", "attachmentInfo must be not null,please fix error");

            if (tacketBase == null)
                throw new ArgumentNullException("tacketBase", "tacketBase must be not null,please fix error");

            if (info.Items != null)
            {
                if (info.Items == null)
                    throw new ArgumentNullException("attachmentInfo.Items must be not null,please fix error");

                foreach (var item in info.Items)
                {
                    if (item.Id.Equals(0) && (item.Content == null || item.Content.Content == null))
                        throw new ArgumentNullException("item.Content must be not null,please fix error");

                    item.UploadMode = tacketBase.Mode;
                }
            }

            var attachmentInfo = attachmentService.SaveOrUpdate(info, true, isMutiple, isUpdateModel);

            #region switch case

            if (attachmentInfo.Items == null || !attachmentInfo.Items.Any()) return attachmentInfo;

            switch (tacketBase.Mode)
            {
                case UploadMode.FTP:

                    FtpTacket ftpTacket = tacketBase as FtpTacket;

                    if (ftpTacket == null)
                        throw new ArgumentNullException("tacketBase");


                    using (
                        Ftp ftp = new Ftp(ftpTacket.Host, ftpTacket.UserName, ftpTacket.PassWord,
                            ftpTacket.Port.ToString()))
                    {
                        foreach (var item in attachmentInfo.Items)
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
                        Unc unc = new Unc(tacketBase.UserName, tacketBase.PassWord, tacketBase.Host))
                    {
                        foreach (var item in attachmentInfo.Items)
                        {
                            //string uncDirectory = this.tacketBase.Host + "\\" + item.FilePath.Replace(Path.GetFileName(item.FilePath), string.Empty).TrimStart('/').Replace('/', '\\');
                            if (item.IsOld) continue;

                            string uncFullPath = tacketBase.Host + "\\" +
                                                 item.FilePath.TrimStart('/').Replace('/', '\\');

                            EnsureDirectory(uncFullPath);

                            File.WriteAllBytes(uncFullPath, item.Content.Content);
                        }
                    }

                    break;
                //case UploadMode.LOC:
                default:

                    foreach (var item in attachmentInfo.Items)
                    {
                        //string localDirectory = this.tacketBase.Host + "\\" + item.FilePath.Replace(Path.GetFileName(item.FilePath), string.Empty).TrimStart('/').Replace('/', '\\');
                        if (item.IsOld) continue;

                        string localFullPath = tacketBase.Host + "\\" +
                                               item.FilePath.TrimStart('/').Replace('/', '\\');

                        EnsureDirectory(localFullPath);

                        File.WriteAllBytes(localFullPath, item.Content.Content);
                    }
                    break;
            }

            #endregion

            return attachmentInfo;
        }

        public static Common.Core.OMSFile.Model.AttachmentItem Download(int attachmentItemId, bool useBrinary, TacketBase tacketBase, out bool fileUnExists)
        {
            fileUnExists = false;

            if (attachmentItemId <= 0)
                throw new ArgumentOutOfRangeException("attachmentId");

            if (tacketBase == null)
                throw new ArgumentNullException("tacketBase", "tacketBase must be not null,please fix error");

            IAttachmentService attachmentService = ObjectContainer.ResolveService<IAttachmentService>();

            if (attachmentService == null)
            {
                throw new ArgumentNullException("IAttachmentService反射加载失败");
            }

            var attachmentItem = attachmentService.GetAttachmentItemById(attachmentItemId, true);

            switch (attachmentItem.UploadMode)
            {
                case UploadMode.FTP:

                    FtpTacket ftpTacket = tacketBase as FtpTacket;

                    if (ftpTacket == null)
                        throw new ArgumentNullException("tacketBase");

                    using (Ftp ftp = new Ftp(ftpTacket.Host, ftpTacket.UserName, ftpTacket.PassWord, ftpTacket.Port.ToString()))
                    {
                        ftp.Directory = attachmentItem.FilePath.Replace(Path.GetFileName(attachmentItem.FilePath), string.Empty);
                        ftp.FileName = Path.GetFileName(attachmentItem.FilePath);

                        if (attachmentItem.Content == null)
                            attachmentItem.Content = new Common.Core.OMSFile.Model.AttachmentContent();
                        attachmentItem.Content.Content = ftp.Download();
                    }

                    break;
                case UploadMode.UNC:

                    using (Unc unc = new Unc(tacketBase.UserName, tacketBase.PassWord, tacketBase.Host))
                    {
                        string uncFullPath = tacketBase.Host + "\\" + attachmentItem.FilePath.TrimStart('/').Replace('/', '\\');

                        EnsureDirectory(uncFullPath);

                        if (!File.Exists(uncFullPath))
                        {
                            fileUnExists = true;
                            return null;
                        }

                        if (attachmentItem.Content == null)
                            attachmentItem.Content = new Common.Core.OMSFile.Model.AttachmentContent();
                        attachmentItem.Content.Content = File.ReadAllBytes(uncFullPath);
                    }

                    break;
                case UploadMode.LOC:
                default:
                    string localFullPath = tacketBase.Host + "\\" + attachmentItem.FilePath.TrimStart('/').Replace('/', '\\');

                    EnsureDirectory(localFullPath);

                    if (!File.Exists(localFullPath))
                    {
                        fileUnExists = true;
                        return null;
                    }

                    if (attachmentItem.Content == null)
                        attachmentItem.Content = new Common.Core.OMSFile.Model.AttachmentContent();
                    attachmentItem.Content.Content = File.ReadAllBytes(localFullPath);
                    break;
            }

            return attachmentItem;
        }
    }
}
