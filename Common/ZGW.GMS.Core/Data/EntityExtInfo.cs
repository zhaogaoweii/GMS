using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 实体的扩展信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class EntityExtInfo
    {
        /// <summary>
        /// 创建者
        /// </summary>
        [DataMember]
        public virtual int? CreatorId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后操作者
        /// </summary>
        [DataMember]
        public virtual int? LastOperatorId { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public virtual DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        public virtual bool IsDelete { get; set; }
    }
}
