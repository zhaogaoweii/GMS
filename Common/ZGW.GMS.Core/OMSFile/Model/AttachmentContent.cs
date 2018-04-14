using ZGW.GMS.Core;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace ZGW.GMS.Core.OMSFile.Model
{
    /// <summary>
    /// 附件内容
    /// </summary>
    [Serializable]
    [DataContract]
    public class AttachmentContent : DBEntity
    {
        /// <summary>
        /// 附件项编号
        /// </summary>
        [DataMember]
        public int AttachmentItemId { get; set; }

        /// <summary>
        /// 附件内容
        /// </summary>
        [DataMember]
        public byte[] Content { get; set; }

        public Stream GetStream()
        {
            if (this.Content == null)
                return null;

            #region MemoryStream memStream = new MemoryStream(this.Content);
            /*
                 * 唐鑫
                 * 2014-04-25
                 * 此段代码不可更改
                 * 否则会因为字节数组偏移量的问题引发读取失败的BUG
                 */
            var memStream = new MemoryStream(this.Content);
            memStream.Seek(0, SeekOrigin.Begin);
            #endregion

            return memStream;
        }
    }
}