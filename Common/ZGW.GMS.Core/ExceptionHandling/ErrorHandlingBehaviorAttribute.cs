using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ZGW.GMS.Core.Exceptions
{
    /// <summary>
    /// 自定义Attribute，注册ErrorHandler
    /// </summary>
    public class ErrorHandlingBehaviorAttribute : Attribute, IServiceBehavior
    {
        /// <summary>
        /// 添加绑定参数
        /// </summary>
        /// <param name="serviceDescription">Service描述</param>
        /// <param name="serviceHostBase">Service Host</param>
        /// <param name="endpoints">终结点集合</param>
        /// <param name="bindingParameters">绑定参数</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            return;
        }

        /// <summary>
        /// 注册自定义ErrorHandler到Dispatcher
        /// </summary>
        /// <param name="serviceDescription">Service描述</param>
        /// <param name="serviceHostBase">Service Host</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                if (channelDispatcher == null) continue;
                channelDispatcher.ErrorHandlers.Add(new ErrorHandler());
            }
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="serviceDescription">Service的描述</param>
        /// <param name="serviceHostBase">Service Host</param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            return;
        }
    }
}
