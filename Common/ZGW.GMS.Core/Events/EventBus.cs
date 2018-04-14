using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Events
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public static class EventBus
    {
        /// <summary>
        /// 默认的事件分发器
        /// </summary>
        private readonly static IEventDispatcher eventDispatcher = new EventDispatcher();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">处理者</param>
        /// <param name="priority">优先级</param>
        public static void AddEventListener<TEvent>(Action<TEvent> handler, int priority = 0) where TEvent : Event
        {
            eventDispatcher.AddEventListener<TEvent>(handler, priority);
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="handler">处理者</param>
        /// <param name="priority">优先级</param>
        public static void AddEventListener<TEvent>(string eventName, Action<TEvent> handler, int priority = 0) where TEvent : Event
        {
            eventDispatcher.AddEventListener<TEvent>(eventName, handler, priority);
        }

        /// <summary>
        /// 分发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件实例</param>
        public static void DispatchEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            eventDispatcher.DispatchEvent<TEvent>(@event);
        }

        /// <summary>
        /// 分发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="event">事件实例</param>
        public static void DispatchEvent<TEvent>(string eventName, TEvent @event) where TEvent : Event
        {
            eventDispatcher.DispatchEvent<TEvent>(eventName,@event);
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="handler">处理者</param>
        public static void RemoveEventListener<TEvent>(string eventName, Action<TEvent> handler) where TEvent : Event
        {
            eventDispatcher.RemoveEventListener<TEvent>(eventName, handler);
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">处理者</param>
        public static void RemoveEventListener<TEvent>(Action<TEvent> handler) where TEvent : Event
        {
            eventDispatcher.RemoveEventListener<TEvent>(handler);
        }
    }
}
