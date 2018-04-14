using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CIIC.OMS.Core.Exceptions;

namespace CIIC.OMS.Core.Web.Mvc
{
    public class AttachmentFilter:IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var files=filterContext.HttpContext.Request.Files;
            foreach (HttpPostedFileBase file in files)
            {
                string fileExt = Path.GetExtension(file.FileName).ToLower();
                if (!SiteConfig.FileExts.Contains(fileExt))
                {
                    throw new OMSException(Path.GetFileName(file.FileName) + "请上传正确" + SiteConfig.MaxFileSize);
                }

                if (file.ContentLength > SiteConfig.MaxFileSize)
                {
                    throw new OMSException(Path.GetFileName(file.FileName)+"超过最大文件大小:"+SiteConfig.MaxFileSize);
                }
            }
        }
    }
}
