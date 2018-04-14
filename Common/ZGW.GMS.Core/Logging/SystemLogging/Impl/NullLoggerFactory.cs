using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Logging
{
    /// <summary>
    /// 默认日志工厂
    /// </summary>
    [ComponentRegistry(Lifetime.Singleton, "logger","null",IsDefault=true)]
    public class NullLoggerFactory:ILogFactory
    {
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="name">日志名称</param>
        /// <returns>日志对象</returns>
        public ILog GetLogger(string name)
        {
            return new NullLogger();
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <returns>日志对象</returns>
        public ILog GetLogger(Type type)
        {
            return new NullLogger();
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <returns>日志对象</returns>
        public ILog GetLogger<T>()
        {
            return new NullLogger();
        }
    }
}
