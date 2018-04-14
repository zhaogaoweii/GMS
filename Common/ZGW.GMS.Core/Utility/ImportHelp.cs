using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Web;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// Excel数据导入
    /// </summary>
    public class ImportHelp
    {
        public static ImportResultEntity GetExcelData(HttpPostedFileBase fileBase)
        {            
            HttpPostedFile file = HttpContext.Current.Request.Files["Files"];            
            ImportResultEntity model = new ImportResultEntity();
            string fileName = Path.GetFileName(file.FileName);
            int fileSize = file.ContentLength;//获取上传文件的大小单位为字节byte
            string fileEx = System.IO.Path.GetExtension(fileName);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(fileName);//获取无扩展名的文件名
            int maxSize = 5000 * 1024;//定义上传文件的最大空间大小为5M
            if (!string.IsNullOrEmpty(WebConfigHelper.GetConfigurationApp("importFileMaxSize")))
            {
                maxSize = Int32Helper.ToInt32(WebConfigHelper.GetConfigurationApp("importFileMaxSize")) * 1024 * 1000;
            }
            string FileType = ".xls,.xlsx";//定义上传文件的类型字符串
            fileName = NoFileName + "_"+ Guid.NewGuid().ToString().Replace("-","") + fileEx;
            if (!FileType.Contains(fileEx))
            {
                model.Status = 1;
                model.Message = "导入的文件必须是Excel文件。";
                return model;
            }
            if (fileSize > maxSize)
            {
                model.Status = 1;
                model.Message = "导入的文件Excel大小不能超过" + (maxSize / 1000 / 1024) + "MB.";
                return model;
            }
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/EXCEL/" + DateTime.Now.ToString("yyyyMMdd");
            if (!System.IO.File.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string savePath = Path.Combine(path, fileName);
            file.SaveAs(savePath);
            model.Status = 2;
            model.FileName = fileName;
            model.Message = "您的文件已上传成功。";
            return model;
        }
    }
}
