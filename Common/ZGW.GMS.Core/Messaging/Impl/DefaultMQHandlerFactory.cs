using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 默认Queue工厂
    /// </summary>
    [ComponentRegistry(Lifetime.Container,"messagequeue","queue")]
    public class DefaultMQHandlerFactory : IMessageQueueHandlerFactory
    {
        /// <summary>
        /// 获取MessageQueue
        /// </summary>
        /// <param name="category">消息的类别</param>
        /// <returns>消息队列</returns>
        public IMessageQueueHandler GetHandler(string category)
        {
            return new DefaultMQHandler(category);
        }

        public void Dispose()
        {
        }
    }
}
