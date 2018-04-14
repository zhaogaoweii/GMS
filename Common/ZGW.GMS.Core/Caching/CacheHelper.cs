using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Caching;
using System.Web;

namespace ZGW.GMS.Core.Caching
{
    /// <summary>
    /// 缓存访问Helper
    /// </summary>
    public static class CacheHelper
    {
        private static ICacheProvider CacheProvider
        {
            get
            {
                return ObjectContainer.ResolveService<ICacheProvider>();
            }
        }

        /// <summary>
        /// 根据Key获取缓存实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">缓存存储的Key</param>
        /// <returns>缓存中的实体</returns>
        public static T Get<T>(string key)
        {
            return CacheProvider.Get<T>(key);
        }

        /// <summary>
        /// 根据Key获取缓存实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">缓存存储的Key</param>
        /// <param name="func">创建实体珠Lambada表达示</param>
        /// <returns>缓存中的实体</returns>
        public static T Get<T>(string key, Func<T> func)
        {
            T obj = CacheProvider.Get<T>(key); ;
            if (obj == null)
            {
                obj = func();
                if (obj != null)
                {
                    CacheProvider.Insert(key, obj);
                }
            }
            return obj;
        }

        /// <summary>
        /// 从缓存中获取对象，如果对象不存，则用func创建对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">缓存存储的Key</param>
        /// <param name="func">创建实体的Lambada表达示</param>
        /// <param name="seconds">对明存在的时间</param>
        /// <returns>缓存中的实体</returns>
        public static T Get<T>(string key, Func<T> func, double seconds)
        {
            T obj = CacheProvider.Get<T>(key); ;
            if (obj == null)
            {
                obj = func();
                if (obj != null)
                {
                    CacheProvider.Insert(key, obj, seconds);
                }
            }
            return obj;
        }

        /// <summary>
        /// 向缓存中添对象
        /// </summary>
        /// <param name="key">缓存存储的Key</param>
        /// <param name="instance">缓存中的实体</param>
        public static void Insert(string key, object instance)
        {
            CacheProvider.Insert(key, instance);
        }

        /// <summary>
        /// 向缓存中添加对象
        /// </summary>
        /// <param name="key">缓存存储的Key</param>
        /// <param name="instance">缓存中的实体</param>
        /// <param name="seconds">实体存在的时间</param>
        public static void Insert(string key, object instance, double seconds)
        {
            CacheProvider.Insert(key, instance, seconds);
        }

        /// <summary>
        /// 从缓存中移取对象
        /// </summary>
        /// <param name="key">缓存中的Key</param>
        public static void Remove(string key)
        {
            CacheProvider.Remove(key);
        }
        public static void Set(string name, object value, CacheDependency cacheDependency)
        {
            HttpRuntime.Cache.Insert(name, value, cacheDependency, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
        }

    }
}
