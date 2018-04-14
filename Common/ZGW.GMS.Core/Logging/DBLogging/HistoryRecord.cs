using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.DBLogging
{
    /// <summary>
    /// 操作历史记录
    /// </summary>
    [Serializable]
    public class HistoryRecord
    {
        /// <summary>
        /// 操作人
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        public string Func { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public int Action { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 关联ID
        /// </summary>
        public int? RecordID { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
