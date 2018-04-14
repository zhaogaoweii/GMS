using System.Runtime.Serialization;

namespace ZGW.GMS.Core.Exceptions
{
    /// <summary>
    /// 邮件服务异常类
    /// </summary>
    [FaultCode(Name = "EmailServiceError")]
    public class EmailServiceException : OMSException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EmailServiceException()
            :base()
        { 

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public EmailServiceException(string message)
            : base(message)
        {

        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">内容异常</param>
        public EmailServiceException(string message,System.Exception innerException)
            : base(message,innerException)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        public EmailServiceException(SerializationInfo info, StreamingContext context)
            :base(info,context)
        { 

        }
    }
}
