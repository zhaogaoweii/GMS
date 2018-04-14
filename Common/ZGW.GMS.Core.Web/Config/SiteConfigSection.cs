using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 站点的配置信息
    /// </summary>
    public class SiteConfigSection : ConfigurationSection
    {
        /// <summary>
        /// OMSConfigSection的访问结口
        /// </summary>
        /// <returns>当前站点的配置信息</returns>
        public static SiteConfigSection GetConfig()
        {
             var config=ConfigurationManager.GetSection("oms.siteConfig") as SiteConfigSection;
             if (config == null)
             {
                 throw new NullReferenceException("没有对应siteConfig配置节点");
             }
             return config;
        }

        /// <summary>
        /// 应用程序配置
        /// </summary>
        [ConfigurationProperty("settings", IsDefaultCollection = true)]
        public KeyValueConfigurationCollection Settings
        {
            get { return (KeyValueConfigurationCollection)this["settings"]; }
        }
    }
}
