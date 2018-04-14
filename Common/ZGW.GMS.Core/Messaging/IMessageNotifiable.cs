using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 事件方式处理消息接口
    /// </summary>
    public interface IMessageNotifiable
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="action">消息处理回调</param>
        void AttachMessageHandler<T>(Action<T> handler, Action<Exception> errorCallback);
    }
}
