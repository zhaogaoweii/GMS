using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Logging
{
    /// <summary>
    /// 默认的日志实现
    /// </summary>
    [Serializable]
    internal sealed class NullLogger : ILog
    {
        public static readonly NullLogger Instance = new NullLogger();

        public NullLogger()
        {
        }

        public void Debug(object message)
        {
        }

        public void Debug(object message, Exception exception)
        {
        }

        public void DebugFormat(string format, params Object[] args)
        {
        }

        public bool IsDebugEnabled
        {
            get { return false; }
        }

        public void Info(object message)
        {
        }

        public void Info(object message, Exception exception)
        {
        }

        public void InfoFormat(string format, params Object[] args)
        {
        }

        public bool IsInfoEnabled
        {
            get { return false; }
        }

        public void Warn(object message)
        {
        }

        public void Warn(object message, Exception exception)
        {
        }

        public void WarnFormat(string format, params Object[] args)
        {
        }

        public bool IsWarnEnabled
        {
            get { return false; }
        }

        public void Error(object message)
        {
        }

        public void Error(object message, Exception exception)
        {
        }

        public void ErrorFormat(string format, params Object[] args)
        {
        }

        public bool IsErrorEnabled
        {
            get { return false; }
        }

        public void Fatal(object message)
        {
        }

        public void Fatal(object message, Exception exception)
        {
        }

        public void FatalFormat(string format, params Object[] args)
        {
        }

        public bool IsFatalEnabled
        {
            get { return false; }
        }

    }
}
