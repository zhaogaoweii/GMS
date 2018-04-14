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
    public interface IEventDispatcher
    {
        /// <summary>
        /// 添加事件监听器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理者</param>
        /// <param name="priority">优先级</param>
        void AddEventListener<TEvent>(Action<TEvent> handler, int priority = 0) where TEvent : Event;

        /// <summary>
        /// 添加事件监听程序
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="handler">事件处理者</param>
        /// <param name="priority">优先级</param>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void AddEventListener<TEvent>(string eventName, Action<TEvent> handler, int priority = 0) where TEvent : Event;

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void DispatchEvent<TEvent>() where TEvent : Event, new();

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        void DispatchEvent<TEvent>(string eventName) where TEvent : Event, new();

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件实例</param>
        void DispatchEvent<TEvent>(TEvent @event) where TEvent : Event;

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="event">事件实例</param>
        void DispatchEvent<TEvent>(string eventName, TEvent @event) where TEvent : Event;

        /// <summary>
        /// 移除事件监听器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventName">事件名称</param>
        /// <param name="handler">事件处理者</param>
        void RemoveEventListener<TEvent>(string eventName, Action<TEvent> handler) where TEvent : Event;

        /// <summary>
        /// 移除事件监听器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理者</param>
        void RemoveEventListener<TEvent>(Action<TEvent> handler) where TEvent : Event;
    }
}
