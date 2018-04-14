using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.DBLogging
{
    /// <summary>
    /// 数据操作类型
    /// </summary>
    public enum DBOperation
    {
        /// <summary>
        /// 添加
        /// </summary>
        INSERT = 0,
        /// <summary>
        /// 更新
        /// </summary>
        UPDATE = 1,
        /// <summary>
        /// 删除
        /// </summary>
        DELETE = 2,
        /// <summary>
        /// 查询
        /// </summary>
        QUERY = 3
    }
}
