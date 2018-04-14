using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Logging
{
    /// <summary>
    /// 日志工厂
    /// </summary>
    public interface ILogFactory
    {
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="name">日志名称</param>
        /// <returns>日志对象</returns>
        ILog GetLogger(string name);

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <returns>日志对象</returns>
        ILog GetLogger(Type type);

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <returns>日志对象</returns>
        ILog GetLogger<T>();
    }
}
