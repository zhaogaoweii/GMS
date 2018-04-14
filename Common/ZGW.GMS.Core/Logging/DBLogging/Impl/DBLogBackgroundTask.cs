using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core.Data;
using ZGW.GMS.Core.DBLogging;
using ZGW.GMS.Core.Tasks;

namespace ZGW.GMS.Core.DBLogging.Impl
{
    /// <summary>
    /// 数据日志的后台处理程序
    /// </summary>
    //[ComponentRegistry]
    //public class DBLogBackgroundTask : IBackgroundTask
    //{
    //    private readonly IMessageQueueHandlerFactory messageQueueFactory;
    //    private readonly IRepository<DBHistory> repository;
    //    private IMessageQueueHandler messageQueue;

    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="messageQueueFactory">messageQueue工厂</param>
    //    /// <param name="repository">DBHistory的资源库</param>
    //    public DBLogBackgroundTask(IMessageQueueHandlerFactory messageQueueFactory, IRepository<DBHistory> repository)
    //    {
    //        this.messageQueueFactory = messageQueueFactory;
    //        this.messageQueue = messageQueueFactory.GetHandler("DBLog");
    //        this.repository = repository;
    //    }

    //    /// <summary>
    //    /// 执行后台任务
    //    /// </summary>
    //    /// <param name="args">后台任务参数</param>
    //    public void Execute(BackgroundTaskArgs args)
    //    {
    //        messageQueue.MessageReceived += messageQueue_MessageReceived;
    //    }

    //    void messageQueue_MessageReceived(object sender, MessageReceivedEventArgs args)
    //    {
    //        args.Acknowledge();

    //        HistoryRecord record = args.Data as HistoryRecord;
    //        if (record != null)
    //        {
    //            SaveDBHistory(record);
    //        }
    //    }

    //    private void SaveDBHistory(HistoryRecord historyRecord)
    //    {
    //        DBHistory history = new DBHistory()
    //        {
    //            Module = historyRecord.Module,
    //            Func = historyRecord.Func,
    //            Action = historyRecord.Action,
    //            TableName = historyRecord.TableName,
    //            RecordID = historyRecord.RecordID,
    //            UserName = historyRecord.UserName,
    //            UpdateTime = DateTime.Now
    //        };

    //        repository.Create(history);

    //        AddDetails(historyRecord, history);

    //        repository.Flush();
    //    }

    //    private void AddDetails(HistoryRecord historyRecord, DBHistory history)
    //    {
    //        int startIndex = 0;
    //        int contentLength = 2000;
    //        while (startIndex < historyRecord.Content.Length)
    //        {
    //            DBHistoryDetail detail = new DBHistoryDetail();
    //            if (historyRecord.Content.Length - startIndex > contentLength)
    //            {
    //                detail.Content = historyRecord.Content.Substring(startIndex, contentLength);
    //            }
    //            else
    //            {
    //                detail.Content = historyRecord.Content.Substring(startIndex);
    //            }
    //            history.Details.Add(detail);
    //            repository.UnitOfWork.RegisterNew(detail);
    //            startIndex = startIndex + contentLength;
    //        }
    //    }
    //}
}
