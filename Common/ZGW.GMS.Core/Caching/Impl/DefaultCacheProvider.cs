using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace ZGW.GMS.Core.Caching
{
    /// <summary>
    /// 基于Asp.net的缓存提供者
    /// </summary>
    [ComponentRegistry(Lifetime.Singleton, "cache.provider", "aspnet", IsDefault = true)]
    public class DefaultCacheProvider : ICacheProvider
    {
        private Cache Cache
        {
            get { return HttpRuntime.Cache ?? HttpContext.Current.Cache; }
        }

        /// <summary>
        /// 根据Key获取缓存实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">缓存存储的Key</param>
        /// <returns>缓存中的实体</returns>
        public T Get<T>(string key)
        {
            return (T)Cache.Get(key);
        }

        /// <summary>
        /// 向缓存中按Key添加数据
        /// </summary>
        /// <param name="key">缓存存储的Key</param>
        /// <param name="instance">存储与缓存中的实体</param>
        public void Insert(string key, object instance)
        {
            Cache.Insert(key, instance, null, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// 向缓存中按Key添加数据,并设置缓存时间
        /// </summary>
        /// <param name="key">缓存存储的Key</param>
        /// <param name="instance">存储与缓存中的实体</param>
        /// <param name="seconds">实体在无访问时的存在时间</param>
        public void Insert(string key, object instance, double seconds)
        {
            Cache.Insert(key, instance, null, DateTime.Now.AddSeconds(seconds), TimeSpan.Zero, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// 从缓存中按Key移除缓存数据项
        /// </summary>
        /// <param name="key">缓存中的Key</param>
        public void Remove(string key)
        {
            Cache.Remove(key);
        }
        public  void Set(string name, object value, CacheDependency cacheDependency)
        {
            Cache.Insert(name, value, cacheDependency, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
        }

    }
}
