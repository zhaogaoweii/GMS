using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.DBLogging
{
    /// <summary>
    /// 数据日志处理接口
    /// </summary>
    public interface IDBLogHandler
    {
        /// <summary>
        /// 处理历史记录
        /// </summary>
        /// <param name="record">日志记录</param>
        void Handle(HistoryRecord record);
    }
}
