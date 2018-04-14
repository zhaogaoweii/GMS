using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 消息收到队列的参数
    /// </summary>
    public class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentQueue">当前消息处理器</param>
        /// <param name="message">当前消息</param>
        public MessageReceivedEventArgs(IMessageQueueHandler currentQueue,IMessage message)
        {
            Handler = currentQueue;
            Message = message;
        }

        /// <summary>
        /// 当前消息处理器
        /// </summary>
        public IMessageQueueHandler Handler { get; set; }

        /// <summary>
        /// 当前消息
        /// </summary>
        public IMessage Message { get; set; }

        /// <summary>
        /// 当前数据
        /// </summary>
        public object Data
        {
            get { return Message.Data; }
        }

        /// <summary>
        /// 获取当前数据,不标识已经收到
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <returns>当前数据</returns>
        public T GetData<T>()
        {
            return Message.GetData<T>();
        }

        /// <summary>
        /// 获取当前数据,并标识已经收到
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <returns>当前数据</returns>
        public T RecevieData<T>()
        {
            return Message.RecevieData<T>();
        }

        /// <summary>
        /// 通知已经收到队列数据
        /// </summary>
        public void Acknowledge()
        {
            Message.Acknowledge();
        }
    }
}
