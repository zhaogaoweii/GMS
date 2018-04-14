using System;
using System.Runtime.Serialization;

namespace ZGW.GMS.Core.Exceptions
{
    /// <summary>
    /// ZGW.GMS.Core系统异常基类
    /// </summary>
    [Serializable]
    [FaultCode(Name = "OMSApplicationError")]
    public class OMSException : Exception, ISerializable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OMSException()
            :base()
        { 

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public OMSException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">内容异常</param>
        public OMSException(string message,System.Exception innerException)
            : base(message,innerException)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        public OMSException(SerializationInfo info, StreamingContext context)
            :base(info,context)
        { 

        }
    }
}
