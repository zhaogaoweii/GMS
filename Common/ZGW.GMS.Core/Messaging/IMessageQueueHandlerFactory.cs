using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 取得消息队列处理器的工厂
    /// </summary>
    public interface IMessageQueueHandlerFactory:IDisposable
    {
        /// <summary>
        /// 获取MessageQueue处理器
        /// </summary>
        /// <param name="category">消息的类别</param>
        /// <returns>消息队列</returns>
        IMessageQueueHandler GetHandler(string category);
    }
}
