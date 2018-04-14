using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Events
{
    /// <summary>
    /// 事件处理者接口
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    public interface IEventHandler<TEvent> where TEvent:Event
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        string EventName { get; }

        /// <summary>
        /// 优先级
        /// </summary>
        int Priority { get;}

        /// <summary>
        /// 处理方法
        /// </summary>
        /// <param name="args">事件参数</param>
        void Handle(TEvent args);
    }
}
