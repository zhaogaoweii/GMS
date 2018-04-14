using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using ZGW.GMS.Core;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 创建DotNetMQ的工厂
    /// </summary>
    [ComponentRegistry(Lifetime.Container, "messagequeue", "dotnetmq", IsDefault = true)]
    public class DotNetMQFactory : IMessageQueueHandlerFactory
    {
        private readonly Dictionary<string, IMessageQueueHandler> dicQueue = new Dictionary<string, IMessageQueueHandler>();

        /// <summary>
        /// 创建消息队列
        /// </summary>
        /// <param name="category">消息队列类别</param>
        /// <returns>消息队列</returns>
        public IMessageQueueHandler GetHandler(string category)
        {
            if (!dicQueue.ContainsKey(category))
            {
                lock (dicQueue)
                {
                    if (!dicQueue.ContainsKey(category))
                    {
                        IDictionary dotNetMQConfig = (IDictionary)ConfigurationManager.GetSection("dotnetmq");
                        string ipAddress = dotNetMQConfig["ipAddress"] as string;
                        int port = int.Parse(dotNetMQConfig["port"] as string);

                        var queue = new DotNetMQHandler(category, ipAddress, port);
                        dicQueue.Add(category, queue);
                    }
                }
            }

            return dicQueue[category];
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            foreach (var item in dicQueue)
            {
                try
                {
                    item.Value.Dispose();
                }
                catch { }
            }
        }
    }
}
