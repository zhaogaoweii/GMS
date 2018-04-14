using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 消息接口
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// 通知已经收到队列数据
        /// </summary>
        void Acknowledge();

        /// <summary>
        /// 当前数据
        /// </summary>
        object Data { get; }

        /// <summary>
        /// 获取当前数据,不标识已经收到
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <returns>当前数据</returns>
        T GetData<T>();

        /// <summary>
        /// 获取当前数据,并标识已经收到
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <returns>当前数据</returns>
        T RecevieData<T>();
    }
}
