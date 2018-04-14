using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace ZGW.GMS.Core.Mvc
{
    /// <summary>
    /// 分页查询参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class PagedListQuery
    {
        /// <summary>
        /// 当前页
        /// </summary>
        [DataMember]
        public int Page { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        [DataMember]
        public int Rows { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        [DataMember]
        public string SIDX { get; set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        [DataMember]
        public string Sord { get; set; }
    }
}
