using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Events
{
    /// <summary>
    /// 事件分发器接口
    /// </summary>
    [ComponentRegistry]
    public class EventDispatcher : IEventDispatcher
    {
        /// <summary>
        /// 默认事件名称
        /// </summary>
        public static readonly string DefaultEventName = String.Empty;

        /// <summary>
        /// 事件集合
        /// </summary>
        private readonly IList<EventItem> Events = new List<EventItem>();

        /// <summary>
        /// 添加事件监听器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理者</param>
        /// <param name="priority">优先级</param>
        public void AddEventListener<TEvent>(Action<TEvent> handler, int priority = 0) where TEvent : Event
        {
            AddEventListener<TEvent>(DefaultEventName, handler, priority);
        }

        /// <summary>
        /// 添加事件监听程序
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="handler">事件处理者</param>
        /// <param name="priority">优先级</param>
        /// <typeparam name="TEvent">事件类型</typeparam>
        public void AddEventListener<TEvent>(string eventName, Action<TEvent> handler, int priority = 0) where TEvent : Event
        {
            Events.Add(new EventItem(eventName, typeof(TEvent), priority, handler));
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件实例</param>
        public void DispatchEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            DispatchEvent(DefaultEventName, @event);
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        public void DispatchEvent<TEvent>(string eventName) where TEvent : Event, new()
        {
            DispatchEvent(eventName, new TEvent());
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        public void DispatchEvent<TEvent>() where TEvent : Event, new()
        {
            DispatchEvent<TEvent>(DefaultEventName, new TEvent());
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="event">事件实例</param>
        public void DispatchEvent<TEvent>(string eventName, TEvent @event) where TEvent : Event
        {
            var eventItems = LoadEvents<TEvent>(eventName);

            foreach (var item in eventItems)
            {
                ((Action<TEvent>)item.Handler)(@event);
            }
        }

        /// <summary>
        /// 移除事件监听器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="handler">事件处理者</param>
        public void RemoveEventListener<TEvent>(string eventName, Action<TEvent> handler) where TEvent : Event
        {
            var eventItem = FindEventItem<TEvent>(eventName, handler);
            if (eventItem != null)
            {
                Events.Remove(eventItem);
            }
        }

        /// <summary>
        /// 移除事件监听器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理者</param>
        public void RemoveEventListener<TEvent>(Action<TEvent> handler) where TEvent : Event
        {
            RemoveEventListener(DefaultEventName, handler);
        }

        /// <summary>
        /// 查找事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventName"></param>
        /// <returns></returns>
        private EventItem FindEventItem<TEvent>(string eventName, Action<TEvent> handler)
        {
            return Events.FirstOrDefault(m => m.EventType == typeof(TEvent) && m.EventName == eventName && m.Handler.Equals(handler));
        }

        /// <summary>
        /// 加载事件处理类
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventName"></param>
        /// <returns></returns>
        private IList<EventItem> LoadEvents<TEvent>(string eventName) where TEvent : Event
        {
            //加载通过AddListener添加的监听器
            var eventItems = Events.Where(m => m.EventType == typeof(TEvent) && m.EventName == eventName);

            //加载通过IEventHandler注册的监听器
            var eventItemsFromHandler = ObjectContainer.ResolveServices<IEventHandler<TEvent>>();
            if (eventItemsFromHandler != null)
            {
                eventItems = eventItems.Concat(eventItemsFromHandler.Select(m => new EventItem(eventName, typeof(TEvent), m.Priority, new Action<TEvent>(m.Handle))));
            }

            return eventItems.OrderByDescending(m => m.Priority)
                            .ToList();
        }
    }
}
