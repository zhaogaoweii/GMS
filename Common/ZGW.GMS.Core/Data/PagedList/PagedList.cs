using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 分页数据列表
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    [Serializable]
    [DataContract]
    public class PagedList<T> :IPagedList
    {
        private List<T> items = new List<T>();
        /// <summary>
        /// 构造函数
        /// </summary>
        public PagedList() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">所有记录</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页记录条数</param>
        public PagedList(IList<T> items, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            TotalItemCount = items.Count;
            CurrentPageIndex = pageIndex;
            for (int i = StartRecordIndex - 1; i < EndRecordIndex; i++)
            {
                Items.Add(items[i]);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">分页后的数据</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页记录条数</param>
        /// <param name="totalItemCount">总记录数</param>
        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, long totalItemCount)
        {
            Items.AddRange(items);
            TotalItemCount = totalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
        /// 分页后的数据项
        /// </summary>
        [DataMember]
        public List<T> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        [DataMember]
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        [DataMember]
        public long TotalItemCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get { return (int)Math.Ceiling(TotalItemCount / (double)PageSize); } }

        /// <summary>
        /// 记录开始行
        /// </summary>
        public int StartRecordIndex { get { return (CurrentPageIndex - 1) * PageSize + 1; } }

        /// <summary>
        /// 记录结束行
        /// </summary>
        public int EndRecordIndex { get { return (int)(TotalItemCount > CurrentPageIndex * PageSize ? CurrentPageIndex * PageSize : TotalItemCount); } }
    }
}
