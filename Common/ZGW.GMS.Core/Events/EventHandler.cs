using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Events
{
    /// <summary>
    /// 事件处理者的抽象类
    /// </summary>
    /// <typeparam name="TEvent">实体类型</typeparam>
    [ComponentRegistry]
    public abstract class EventHandler<TEvent>:IEventHandler<TEvent> where TEvent:Event
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public virtual string EventName
        {
            get { return EventDispatcher.DefaultEventName; }
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// 事件处理方法
        /// </summary>
        /// <param name="args">事件参数</param>
        public abstract void Handle(TEvent args);
    }
}
