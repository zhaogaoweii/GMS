
using System;
using System.Data;
using System.Runtime.Serialization;
namespace ZGW.GMS.Core
{
    /// <summary>
    /// 统计查询分页数据对象
    /// </summary>
    [Serializable]
    [DataContract]
    public class StatisticsTable : PagedTable
    {
        /// <summary>
        /// 统计数据对象
        /// </summary>
        [DataMember]
        public DataTable Statistics { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public StatisticsTable()
            : base()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataTable">分页后的DataTable</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        public StatisticsTable(DataTable dataTable, int pageIndex, int pageSize)
            : base(dataTable, pageIndex, pageSize)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataTable">分页后的DataTable</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="totalItemCount">总记录数</param>
        public StatisticsTable(DataTable dataTable, int pageIndex, int pageSize, long totalItemCount)
            : base(dataTable, pageIndex, pageSize, totalItemCount)
        {
        }
    }
}
