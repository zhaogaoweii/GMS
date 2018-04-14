using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 默认的消息队列
    /// </summary>
    internal class DefaultMQHandler : IMessageQueueHandler
    {
        public DefaultMQHandler(string messageCategory)
        {
        }

        public void Send(object message)
        {
        }

        public event MessageReceivedHandler MessageReceived;

        public void Dispose()
        {
        }
    }
}
