using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Events
{
    /// <summary>
    /// 事件数据项
    /// </summary>
    internal class EventItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="priority">优先级</param>
        /// <param name="handler">事件处理者</param>
        public EventItem(string eventName, Type eventType,int priority,object handler)
        {
            EventName = eventName;
            EventType = eventType;
            Priority = priority;
            Handler = handler;
        }

        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public Type EventType { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 事件的处理者
        /// </summary>
        public object Handler { get; set; }
    }
}
