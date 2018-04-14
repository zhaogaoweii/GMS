using ZGW.GMS.Core.OMSFile.Helper;
using ZGW.GMS.Core;
using System;
using System.Runtime.Serialization;

namespace ZGW.GMS.Core.OMSFile.Model
{
    /// <summary>
    /// 附件项
    /// </summary>
    [Serializable]
    [DataContract]
    public class AttachmentItem : DBEntity
    {
        /// <summary>
        /// 附件编号
        /// </summary>
        [DataMember]
        public int AttachmentInfoId { get; set; }

        /// <summary>
        /// 文件存储相对路径
        /// </summary>
        [DataMember]
        public string FilePath { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// 上传模式
        /// </summary>
        [DataMember]
        public UploadMode UploadMode { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        [DataMember]
        public string FileExt { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [DataMember]
        public int FileSize { get; set; }

        /// <summary>
        /// 文件的mime-type
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        /// 是否存储在DB中
        /// </summary>
        [DataMember]
        public bool IsDB { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        [DataMember]
        public AttachmentContent Content { get; set; }

        private bool _isOld;

        /// <summary>
        /// 是否是旧附件
        /// </summary>
        [DataMember]
        public bool IsOld
        {
            get { return _isOld; }
            set { _isOld = value; }
        }
    }
}