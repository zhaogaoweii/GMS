using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Modules
{
    /// <summary>
    /// 模块服务接口
    /// </summary>
    public interface IModuleService
    {
        /// <summary>
        /// 根据模块名称加载模块
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns>模块信息</returns>
        ModuleInfo LoadModuleInfo(string moduleName);

        /// <summary>
        /// 加载所有的模块配置信息
        /// </summary>
        /// <returns>系统中所有的模块信息</returns>
        IEnumerable<ModuleInfo> LoadAllModuleInfo();
    }
}
