using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Modules.Impl
{
    /// <summary>
    /// 模块服务
    /// </summary>
    [ComponentRegistry(Lifetime.Singleton)]
    public class ModuleService : IModuleService
    {
        private IList<ModuleInfo> modules;

        /// <summary>
        /// 根据模块名称加载模块
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns>模块信息</returns>
        public ModuleInfo LoadModuleInfo(string moduleName)
        {
            return modules.FirstOrDefault(m => m.Name == moduleName);
        }

        /// <summary>
        /// 加载所有的模块配置信息
        /// </summary>
        /// <returns>系统中所有的模块信息</returns>
        public IEnumerable<ModuleInfo> LoadAllModuleInfo()
        {
            if (modules == null)
            {
                string moduleRootPath = AppDomain.CurrentDomain.BaseDirectory + "/Areas";
                DirectoryInfo dir = new DirectoryInfo(moduleRootPath);
                IList<FileInfo> files = dir.GetDirectories().SelectMany(m => m.GetFiles("module.config")).ToList();
                modules = files.Select(m => new ModuleInfo(m.FullName)).ToList();
            }
            return modules;
        }
    }
}
