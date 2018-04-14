using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Caching
{
    ///// <summary>
    ///// 基于Appfabric的缓存提供者
    ///// </summary>
    //[ComponentRegistry(Lifetime.Singleton, "cache.provider","appfabric")]
    //public class AppFabricCacheProvider : ICacheProvider
    //{
    //    private Lazy<DataCache> dataCache = new Lazy<DataCache>(() =>
    //    {
    //        string cacheName = ConfigurationManager.AppSettings["cache.name"];

    //        DataCacheFactoryConfiguration cfg = new DataCacheFactoryConfiguration();
    //        DataCacheFactory factory = new DataCacheFactory(cfg);
    //        DataCache cache = factory.GetCache(cacheName);
            
    //        return cache;
    //    });

    //    /// <summary>
    //    /// 根据Key获取缓存实体
    //    /// </summary>
    //    /// <typeparam name="T">实体类型</typeparam>
    //    /// <param name="key">缓存存储的Key</param>
    //    /// <returns>缓存中的实体</returns>
    //    public T Get<T>(string key)
    //    {
    //        return (T)DataCache.Get(key);
    //    }

    //    /// <summary>
    //    /// 向缓存中按Key添加数据
    //    /// </summary>
    //    /// <param name="key">缓存存储的Key</param>
    //    /// <param name="value">存储与缓存中的实体</param>
    //    public void Insert(string key, object value)
    //    {
    //        DataCache.Put(key, value);
    //    }

    //    /// <summary>
    //    /// 向缓存中按Key添加数据,并设置缓存时间
    //    /// </summary>
    //    /// <param name="key">缓存存储的Key</param>
    //    /// <param name="value">存储与缓存中的实体</param>
    //    /// <param name="seconds">实体在无访问时的存在时间</param>
    //    public void Insert(string key, object value, double seconds)
    //    {
    //        DataCache.Put(key, value, TimeSpan.FromSeconds(seconds));
    //    }

    //    /// <summary>
    //    /// 从缓存中按Key移除缓存数据项
    //    /// </summary>
    //    /// <param name="key">缓存中的Key</param>
    //    public void Remove(string key)
    //    {
    //        DataCache.Remove(key);
    //    }

    //    private DataCache DataCache
    //    {
    //        get
    //        {
    //            return dataCache.Value;
    //        }
    //    }
    //}
}
