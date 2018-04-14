﻿using System;
using System.Web.Caching;
using ZGW.GMS.Core.Caching;
using ZGW.GMS.Core.Configuration.Implement;

namespace ZGW.GMS.Core.Config
{
    public class CachedConfigContext : ConfigContext
    {
        /// <summary>
        /// 重写基类的取配置，加入缓存机制
        /// </summary>
        public override T Get<T>(string index = null)
        {
            var fileName = this.GetConfigFileName<T>(index);
            var key = "ConfigFile_" + fileName;
            var content = CacheHelper.Get<T>(key);
            if (content != null)
                return (T)content;

            var value = base.Get<T>(index);
            CacheHelper.Set(key, value, new CacheDependency(ConfigService.GetFilePath(fileName)));
            return value;
        }

        public static CachedConfigContext Current = new CachedConfigContext();

        public DaoConfig DaoConfig
        {
            get
            {
                return this.Get<DaoConfig>();
            }
        }

        public CacheConfig CacheConfig
        {
            get
            {
                return this.Get<CacheConfig>();
            }
        }

        public AdminMenuConfig AdminMenuConfig
        {
            get
            {
                return this.Get<AdminMenuConfig>();
            }
        }

        public SettingConfig SettingConfig
        {
            get
            {
                return this.Get<SettingConfig>();
            }
        }

        public SystemConfig SystemConfig
        {
            get
            {
                return this.Get<SystemConfig>();
            }
        }

        public UploadConfig UploadConfig
        {
            get
            {
                return this.Get<UploadConfig>();
            }
        }
    }
}
