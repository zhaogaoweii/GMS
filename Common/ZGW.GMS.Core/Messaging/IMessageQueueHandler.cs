using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 消息队列处理器
    /// </summary>
    public interface IMessageQueueHandler:IDisposable
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息对象</param>
        void Send(object message);

        /// <summary>
        /// 消息收到的事件
        /// </summary>
        event MessageReceivedHandler MessageReceived;
    }
}
