using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core.PagedListUser;

namespace ZGW.GMS.Core.Mvc
{
    /// <summary>
    /// 用于对分页数据进行序列化的包装类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    [Serializable]
    public class PagedListJsonWrapper<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pagedList">分页数据</param>
        public PagedListJsonWrapper(PagedList<T> pagedList)
        {
            if (pagedList == null)
            {
                throw new ArgumentNullException("pagedList is null!");
            }
            page = pagedList.CurrentPageIndex;
            total = pagedList.TotalPageCount;
            records = pagedList.TotalItemCount;
            rows = pagedList.Items;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long records { get; set; }

        /// <summary>
        /// 记录项
        /// </summary>
        public IList<T> rows { get; set; }
    }
}
