using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Caching
{
    /// <summary>
    /// 缓存关键字业务接口
    /// </summary>
    public interface ICacheKeyService
    {
        /// <summary>
        /// 加载所有的CacheKey.
        /// </summary>
        /// <returns>所有的CacheKey的Attribute</returns>
        IList<CacheKeyAttribute> LoadAllCacheKeys();
    }
}
