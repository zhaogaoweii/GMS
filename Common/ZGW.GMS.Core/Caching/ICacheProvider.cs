using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;

namespace ZGW.GMS.Core.Caching
{
    /// <summary>
    /// 缓存提供者
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// 根据Key获取缓存实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">缓存存储的Key</param>
        /// <returns>缓存中的实体</returns>
        T Get<T>(string key);

        /// <summary>
        /// 向缓存中按Key添加数据
        /// </summary>
        /// <param name="key">缓存存储的Key</param>
        /// <param name="instance">存储与缓存中的实体</param>
        void Insert(string key, object instance);

        /// <summary>
        /// 向缓存中按Key添加数据,并设置缓存时间
        /// </summary>
        /// <param name="key">缓存存储的Key</param>
        /// <param name="instance">存储与缓存中的实体</param>
        /// <param name="seconds">实体在无访问时的存在时间</param>
        void Insert(string key, object instance, double seconds);

        /// <summary>
        /// 从缓存中按Key移除缓存数据项
        /// </summary>
        /// <param name="key">缓存中的Key</param>
        void Remove(string key);
        void Set(string name, object value, CacheDependency cacheDependency);
        
    }
}
