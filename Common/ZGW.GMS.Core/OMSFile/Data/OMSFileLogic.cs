using ZGW.GMS.Core.OMSFile.Model;
using ZGW.GMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZGW.GMS.Core.OMSFile.Data
{
    /// <summary>
    /// 文件上传数据访问逻辑处理类
    /// </summary>
    public class OMSFileLogic
    {
        private static readonly OMSFileRepository FILE_REPOSITORY = new OMSFileRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public AttachmentInfo GetAttachment(int attachmentId)
        {
            return attachmentId <= 0 ? null : FILE_REPOSITORY.GetById(attachmentId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="isIncludeChildren"></param>
        /// <returns></returns>
        public AttachmentItem GetAttachmentItem(int itemId, bool isIncludeChildren = false)
        {
            if (itemId <= 0)
                return null;

            AttachmentItem item = FILE_REPOSITORY.GetItemById(itemId);

            if (isIncludeChildren)
            {
                item.Content = FILE_REPOSITORY.GetContentByItemId(itemId);
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="isIncludeChildren"></param>
        /// <param name="isMutiple"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public AttachmentInfo SaveOrUpdate(AttachmentInfo info, bool isIncludeChildren = false, bool isMutiple = false)
        {
            if (info == null)
                return null;

            AttachmentInfo attachmentInfo;

            if (info.Id > 0)
            {
                bool isUpdated = FILE_REPOSITORY.UpdateExecute(info);
                IEnumerable<AttachmentItem> deleteItems = FILE_REPOSITORY.GetItemsByFileId(info.Id).AsEnumerable();
                if (isUpdated)
                {
                    if (!isMutiple && info.Items != null && info.Items.Any()) //单附件上传需要清空掉数据
                    {
                        deleteItems.ForEach(item => FILE_REPOSITORY.DeleteExecute(item));
                    }
                    else if (isMutiple)
                    {
                        if (info.Items != null && info.Items.Any())
                        {
                            //如果是多附件上传 则删除新的集合中不存在的 保留原有的 添加最新上传的
                            deleteItems.ForEach(o =>
                            {
                                if (info.Items.Count(n => n.Id == o.Id) == 0)
                                {
                                    FILE_REPOSITORY.DeleteExecute(o);
                                }
                            });
                        }
                        else
                        {
                            deleteItems.ForEach(o => FILE_REPOSITORY.DeleteExecute(o));
                        }
                    }
                }

                attachmentInfo = info;
            }
            else
            {
                int id = FILE_REPOSITORY.AddExecute(info);

                attachmentInfo = FILE_REPOSITORY.GetById(id);

                attachmentInfo.Items = info.Items;
            }

            List<AttachmentItem> newItems = new List<AttachmentItem>();

            if (!isIncludeChildren) return attachmentInfo;

            if (attachmentInfo.Items == null || !attachmentInfo.Items.Any()) return attachmentInfo;

            attachmentInfo.Items.ForEach(o => o.AttachmentInfoId = attachmentInfo.Id);

            foreach (var item in attachmentInfo.Items)
            {
                //如果是多附件上传 且当前的附件是旧附件 则不再保存 取出来实体即可
                if (item.Id > 0)
                {
                    AttachmentItem curItem = FILE_REPOSITORY.GetItemById(item.Id);

                    if (curItem != null)
                    {
                        curItem.IsOld = true;
                        newItems.Add(curItem);
                    }

                    continue;
                }

                AttachmentItem newItem = this.SaveOrUpdate(item);

                if (newItem == null)
                    throw new ArgumentNullException("newItem", "newItem must be not null,please fix error");

                newItem.Content = item.Content;
                newItem.Content.AttachmentItemId = newItem.Id;

                byte[] buffer = item.Content.Content;

                if (!item.IsDB)
                {
                    newItem.Content.Content = null;
                }

                newItem.Content = this.SaveOrUpdate(newItem.Content);
                newItem.Content.Content = buffer;

                newItems.Add(newItem);
            }

            attachmentInfo.Items = newItems;

            return attachmentInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public AttachmentItem SaveOrUpdate(AttachmentItem item)
        {
            if (item == null)
                return null;

            AttachmentItem attachmentItem = null;

            if (item.Id > 0)
            {
                bool isUpdate = FILE_REPOSITORY.UpdateExecute(item);
                if (isUpdate)
                    attachmentItem = item;
            }
            else
            {
                int id = FILE_REPOSITORY.AddExecute(item);

                attachmentItem = FILE_REPOSITORY.GetItemById(id);
            }

            return attachmentItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public AttachmentContent SaveOrUpdate(AttachmentContent content)
        {
            if (content == null)
                return null;

            AttachmentContent attachmentContent = null;

            if (content.Id > 0)
            {
                bool isUpdate = FILE_REPOSITORY.UpdateExecute(content);

                if (isUpdate)
                    attachmentContent = content;
            }
            else
            {
                int id = FILE_REPOSITORY.AddExecute(content);

                if (id > 0)
                    attachmentContent = FILE_REPOSITORY.GetContentByItemId(content.AttachmentItemId);
            }

            return attachmentContent;
        }

        #region SearchAttachmentItem:搜索附件项
        /// <summary>
        /// 搜索附件项
        /// </summary>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页数量</param>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public PagedTable SearchAttachmentItem(int pageIndex = 0, int pageSize = 50, int attachmentInfoId = 0, string fileName = "", string fileExt = "")
        {
            return FILE_REPOSITORY.SearchAttachmentItem(pageIndex, pageSize, attachmentInfoId, fileName, fileExt);
        }
        #endregion
    } 
}
