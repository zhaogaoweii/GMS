using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 消息的抽像类
    /// </summary>
    internal abstract class MessageBase:IMessage
    {
        public abstract void Acknowledge();

        public abstract object Data { get; }

        public T GetData<T>()
        {
            return (T)Data;
        }

        public T RecevieData<T>()
        {
            Acknowledge();
            return (T)Data;
        }
    }
}
