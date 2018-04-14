using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections;

namespace ZGW.GMS.Core.Exceptions
{
    /// <summary>
    /// SQL注入的异常信息
    /// </summary>
    [FaultCode(Name = "SQLInjection")]
    public class SQLInjectionException : OMSException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SQLInjectionException()
            :base()
        { 

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public SQLInjectionException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">内容异常</param>
        public SQLInjectionException(string message,System.Exception innerException)
            : base(message,innerException)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        public SQLInjectionException(SerializationInfo info, StreamingContext context)
            :base(info,context)
        { 

        }
    }
}
