using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Configuration.Implement
{
    [ComponentRegistry(Lifetime.Singleton)]
    public class ConfigService : IConfigService
    {
        private readonly string configFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
        /// <summary>
        /// 获取配置文件的内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetConfig(string fileName)
        {
            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);

            var configPath = GetFilePath(fileName);
            if (!File.Exists(configPath))
                return null;
            else
                return File.ReadAllText(configPath);
        }
        /// <summary>
        /// 获取配置文件的路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFilePath(string fileName)
        {
            var configPath = string.Format(@"{0}\{1}.xml", configFolder, fileName);
            return configPath;
        }
    }
}
