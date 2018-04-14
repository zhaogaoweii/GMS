using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZGW.GMS.Core.DBLogging
{
    ///// <summary>
    ///// 基于消息队列的日志处理实现
    ///// </summary>
    //[ComponentRegistry]
    //public class MessageQueueHandler:IDBLogHandler
    //{
    //    private static readonly string MQCategory = "DBLog";

    //    private IMessageQueueHandlerFactory mesageQueryFactory;
        
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="mesageQueryFactory">产生消息队列的工厂</param>
    //    public MessageQueueHandler(IMessageQueueHandlerFactory mesageQueryFactory)
    //    {
    //        this.mesageQueryFactory = mesageQueryFactory;
    //    }

    //    /// <summary>
    //    /// 处理历史记录
    //    /// </summary>
    //    /// <param name="record">日志记录</param>
    //    public void Handle(HistoryRecord record)
    //    {
    //        IMessageQueueHandler mq=mesageQueryFactory.GetHandler(MQCategory);
    //        mq.Send(record);
    //    }
    //}
}
