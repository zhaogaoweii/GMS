using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using ZGW.GMS.Core.Logging;
using ZGW.GMS.Core.Exceptions;

namespace ZGW.GMS.Core.Exceptions
{
    /// <summary>
    /// WCF Exception Handler
    /// </summary>
    [ComponentRegistry]
    public class ErrorHandler : IErrorHandler
    {
        /// <summary>
        /// 记录Exception到系统日志
        /// </summary>
        /// <param name="error">Exception</param>
        /// <returns>是否处理</returns>
        public bool HandleError(Exception error)
        {
            if (error is FaultException) return false; 

            ILog logger = ObjectContainer.ResolveService<ILogFactory>().GetLogger<ILog>();
            logger.Error(error.Message, error);
            return true;
        }

        /// <summary>
        /// 将Exception转化为FaultException
        /// </summary>
        /// <param name="error">Exception</param>
        /// <param name="version">MessageVersion</param>
        /// <param name="fault">错误消息</param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException) return;
            
            FaultCodeAttribute attribute = error.GetType().GetCustomAttribute<FaultCodeAttribute>();
            var faultCode = attribute == null ? error.GetType().Name : attribute.Name;
            FaultException faultException = new FaultException(error.Message, new FaultCode(faultCode));
            MessageFault messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, faultException.Action);
        }

    }
}
