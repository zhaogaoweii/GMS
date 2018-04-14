using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ZGW.GMS.Core.Caching
{
    /// <summary>
    /// 缓存关键字业务类
    /// </summary>
    [ComponentRegistry]
    public class CacheKeyService : ICacheKeyService
    {
        private Lazy<IList<CacheKeyAttribute>> cacheKeyAttributes = new Lazy<IList<CacheKeyAttribute>>(() =>
        {
            Type cacheKeyDefinitionAttribute = typeof(CacheKeyDefinitionAttribute);
            Type cacheKeyAttribute = typeof(CacheKeyAttribute);

            //加载定义了缓存关键字的类型
            IList<Type> definitionTypes = SystemHelper.LoadAppAssemblies().SelectMany(m => m.GetTypes().Where(t => t.IsDefined(cacheKeyDefinitionAttribute, false))).ToList();
            //加载所有的缓存关键字的Attribute
            IList<CacheKeyAttribute> cacheKeyAttributes = definitionTypes
                .SelectMany(m => m.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static))
                .Where(m => m.IsDefined(cacheKeyAttribute))
                .Select(m =>
                {
                    var cacheKey = m.GetCustomAttribute(cacheKeyAttribute) as CacheKeyAttribute;
                    var key = m.GetValue(null);
                    typeof(CacheKeyAttribute).GetProperty("Key").SetValue(cacheKey, m.GetValue(null));

                    return cacheKey;
                })
                .ToList();

            return cacheKeyAttributes;
        });

        /// <summary>
        /// 加载所有的CacheKey.
        /// </summary>
        /// <returns>所有的CacheKey的Attribute</returns>
        public IList<CacheKeyAttribute> LoadAllCacheKeys()
        {
            return cacheKeyAttributes.Value;
        }
    }
}
