using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Caching
{
    /// <summary>
    /// 用于标记可定义缓存Key的类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CacheKeyDefinitionAttribute : Attribute
    {
    }

    /// <summary>
    /// 用于标记可作为缓存Key的字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class CacheKeyAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CacheKeyAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="module">模块</param>
        public CacheKeyAttribute(string module)
        {
            Module = module;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="module">模块</param>
        /// <param name="function">功能</param>
        public CacheKeyAttribute(string module, string function)
        {
            Module = module;
            Function = function;
        }

        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// 具体的Key值
        /// </summary>
        public string Key
        {
            get;
            private set;
        }
    }
}
