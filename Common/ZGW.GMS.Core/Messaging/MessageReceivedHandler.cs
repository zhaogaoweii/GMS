using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 消息处理事件对应的委托
    /// </summary>
    /// <param name="sender">发起事件的对象</param>
    /// <param name="args">消息队列的参数</param>
    public delegate void MessageReceivedHandler(object sender,MessageReceivedEventArgs args);
}
