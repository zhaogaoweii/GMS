using ZGW.GMS.Core.OMSFile.Data;
using ZGW.GMS.Core.OMSFile.Model;
using System.Collections.Generic;

namespace ZGW.GMS.Core.OMSFile
{
    public class AttachmentHelper
    {
        private OMSFileRepository omsFileRepository = new OMSFileRepository();

        #region GetById:根据附件编号获取附件信息
        /// <summary>
        /// 根据附件编号获取附件信息
        /// </summary>
        public AttachmentInfo GetById(int fileId)
        {
            return omsFileRepository.GetById(fileId);
        }
        #endregion

        #region GetItemsByFileId:根据文件编号获取文件项列表
        /// <summary>
        /// 根据文件编号获取文件项列表
        /// </summary>
        public IList<AttachmentItem> GetItemsByFileId(int fileId)
        {
            return omsFileRepository.GetItemsByFileId(fileId);
        }
        #endregion

        #region GetContentByItemId:根据文件项编号获取内容
        /// <summary>
        /// 根据文件项编号获取内容
        /// </summary>
        public AttachmentContent GetContentByItemId(int itemId)
        {
            return omsFileRepository.GetContentByItemId(itemId);
        }
        #endregion
    }
}
