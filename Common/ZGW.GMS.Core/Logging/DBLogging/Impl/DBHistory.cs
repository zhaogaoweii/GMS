using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core.Data;

namespace ZGW.GMS.Core.DBLogging
{
    /// <summary>
    /// 数据历史记录
    /// </summary>
    [Serializable]
    public class DBHistory : DomainEntity
    {
        private IList<DBHistoryDetail> details = new List<DBHistoryDetail>();

        /// <summary>
        /// 操作者
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 模块名
        /// </summary>
        public virtual string Module { get; set; }

        /// <summary>
        /// 功能名
        /// </summary>
        public virtual string Func { get; set; }

        /// <summary>
        /// 动作(0：INSERT，1：UPDATE，2：DELETE,3：QUERY)
        /// </summary>
        public virtual int Action { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public virtual string TableName { get; set; }

        /// <summary>
        /// 记录ID
        /// </summary>
        public virtual int? RecordID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }

        /// <summary>
        /// 历史记录详细信息
        /// </summary>
        public virtual IList<DBHistoryDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
    }
}
