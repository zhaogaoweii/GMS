using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public static class AppContext
    {
        /// <summary>
        /// 配置文件的物理路径
        /// </summary>
        
        public static string ConfigsPhysicalPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs");
            }
        }
    }
}
