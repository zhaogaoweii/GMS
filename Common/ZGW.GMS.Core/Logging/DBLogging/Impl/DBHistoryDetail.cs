using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core.Data;

namespace ZGW.GMS.Core.DBLogging
{
    /// <summary>
    /// 历史信息详细记录
    /// </summary>
    public class DBHistoryDetail:DomainEntity
    {
        /// <summary>
        /// 历史记录详细内容
        /// </summary>
        public virtual string Content { get; set; }
    }
}
