using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 与WebConfig有关的公共方法
    /// </summary>
    public class WebConfigHelper
    {
        /// <summary>
        /// 功能:取Webconfig中的appSetting字节        
        /// 日期:2013-03-25
        /// </summary>
        /// <param name="key">AppSetting的key</param>
        /// <returns>appSetting 中对应的值</returns>
        public static string GetConfigurationApp(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
