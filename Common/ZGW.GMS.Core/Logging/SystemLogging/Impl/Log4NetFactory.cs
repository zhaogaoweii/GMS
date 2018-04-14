using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Logging
{
    /// <summary>
    /// Logg4Net的日志工厂
    /// </summary>
    [ComponentRegistry(Lifetime.Singleton,"logger","log4net")]
    public class Log4NetFactory : ILogFactory
    {
        static Log4NetFactory()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Log4NetFactory()
        {
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="name">日志名称</param>
        /// <returns>日志对象</returns>
        public ILog GetLogger(string name)
        {
            return new Log4NetLogger(log4net.LogManager.GetLogger(name));
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <returns>日志对象</returns>
        public ILog GetLogger(Type type)
        {
            return new Log4NetLogger(log4net.LogManager.GetLogger(type));
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <returns>日志对象</returns>
        public ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }
    }
}
