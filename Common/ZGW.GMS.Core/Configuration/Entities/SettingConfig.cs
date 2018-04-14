﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGW.GMS.Core.Configuration.Entities
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    [Serializable]
    public class SettingConfig 
    {
        public SettingConfig()
        {
        }

        #region 序列化属性
        public String WebSiteTitle { get; set; }
        public String WebSiteDescription { get; set; }
        public String WebSiteKeywords { get; set; }
        #endregion
    }
}
