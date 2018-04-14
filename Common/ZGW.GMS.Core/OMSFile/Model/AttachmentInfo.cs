using ZGW.GMS.Core.OMSFile.Helper;
using ZGW.GMS.Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZGW.GMS.Core.OMSFile.Model
{
    /// <summary>
    /// 附件信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class AttachmentInfo : DBEntity
    {
        /// <summary>
        /// 模块信息
        /// </summary>
        [DataMember]
        public string ModuleName { get; set; }

        /// <summary>
        /// 功能信息
        /// </summary>
        [DataMember]
        public string FunctionName { get; set; }
         
        /// <summary>
        /// 附件项
        /// </summary>
        [DataMember]
        public IList<AttachmentItem> Items { get; set; }
    }
}
