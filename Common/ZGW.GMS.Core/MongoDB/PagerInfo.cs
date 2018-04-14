using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.MongoDB
{
    [Serializable]
    [DataContract]
    public class PagerInfo 
    {
        /// <summary>
        /// 页码
        /// </summary>
        [DataMember]
        public int CurrenetPageIndex { get; set; }


        /// <summary>
        /// 每页记录数
        /// </summary>
         [DataMember]
        public int PageSize { get; set; }


        /// <summary>
        /// 记录总数
        /// </summary>
         [DataMember]
        public int RecordCount { get; set; }
    }
}
