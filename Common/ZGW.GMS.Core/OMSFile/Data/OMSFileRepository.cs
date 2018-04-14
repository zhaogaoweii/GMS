using ZGW.GMS.Core.OMSFile.Helper;
using ZGW.GMS.Core.OMSFile.Model;
using ZGW.GMS.Core;
using ZGW.GMS.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace ZGW.GMS.Core.OMSFile.Data
{
    /// <summary>
    /// OMS文件数据访问层
    /// </summary>
    public class OMSFileRepository
    {
        //    {
        //        #region 附件相关查询操作

        //        #region GetById:根据附件编号获取附件信息
        //        /// <summary>
        //        /// 根据附件编号获取附件信息
        //        /// </summary>
        //        public AttachmentInfo GetById(int fileId)
        //        {
        //            string sql = @"SELECT * 
        //                        FROM TB_INFRA_ATTACHMENT_INFO ai 
        //                                WHERE ai.ISDELETE=0 AND ai.ATTACHMENT_INFO_ID = :V_Id ";

        //            List<ParameterEntity> parList = new List<ParameterEntity>()
        //            {
        //                new ParameterEntity{ DbType= DbType.Int32, Name=":V_Id", Value=fileId}
        //            };

        //            DataTable tbl = OracleDBHelper.ExecuteDataTable(sql, parList);
        //            if (null != tbl && tbl.Rows.Count > 0)
        //            {
        //                AttachmentInfo attachment = DataRowToAttachmentInfo(tbl.Rows[0]);
        //                if (null != attachment)
        //                {
        //                    attachment.Items = GetItemsByFileId(fileId);
        //                }
        //                return attachment;
        //            }
        //            else
        //                return null;
        //        }
        //        #endregion

        //        #region DataRowToAttachmentInfo:文件行转换成实体
        //        private AttachmentInfo DataRowToAttachmentInfo(DataRow dr)
        //        {
        //            if (null == dr)
        //                return null;

        //            AttachmentInfo entity = new AttachmentInfo();
        //            if (dr["ATTACHMENT_INFO_ID"] != DBNull.Value)
        //                entity.Id = Convert.ToInt32(dr["ATTACHMENT_INFO_ID"]);
        //            if (dr["FUNCTION_NAME"] != DBNull.Value)
        //                entity.FunctionName = dr["FUNCTION_NAME"].ToString();
        //            if (dr["MODULE_NAME"] != DBNull.Value)
        //                entity.ModuleName = dr["MODULE_NAME"].ToString();
        //            if (dr["CREATOR"] != DBNull.Value)
        //                entity.CreatorId = Convert.ToInt32(dr["CREATOR"]);
        //            if (dr["CREATE_TIME"] != DBNull.Value)
        //                entity.CreateTime = Convert.ToDateTime(dr["CREATE_TIME"]);
        //            if (dr["LAST_OPERATOR"] != DBNull.Value)
        //                entity.LastOperatorId = Convert.ToInt32(dr["LAST_OPERATOR"]);
        //            if (dr["LAST_UPDATE_TIME"] != DBNull.Value)
        //                entity.LastUpdateTime = Convert.ToDateTime(dr["LAST_UPDATE_TIME"]);
        //            if (dr["ISDELETE"] != DBNull.Value)
        //                entity.IsDelete = Convert.ToBoolean(dr["ISDELETE"]);
        //            if (dr["VERSION"] != DBNull.Value)
        //                entity.Version = Convert.ToInt32(dr["VERSION"]);

        //            return entity;
        //        }
        //        #endregion

        //        #region GetItemsByFileId:根据文件编号获取文件项列表
        //        /// <summary>
        //        /// 根据文件编号获取文件项列表
        //        /// </summary>
        //        public IList<AttachmentItem> GetItemsByFileId(int fileId)
        //        {
        //            string sql = @"SELECT * 
        //                        FROM TB_INFRA_ATTACHMENT_ITEM ait 
        //                                WHERE ait.ISDELETE=0 AND ait.ATTACHMENT_INFO_ID = :V_Id ";

        //            List<ParameterEntity> parList = new List<ParameterEntity>()
        //            {
        //                new ParameterEntity{ DbType= DbType.Int32, Name=":V_Id", Value=fileId}
        //            };

        //            DataTable tbl = OracleDBHelper.ExecuteDataTable(sql, parList);
        //            if (null != tbl && tbl.Rows.Count > 0)
        //            {
        //                IList<AttachmentItem> attachmentItems = new List<AttachmentItem>();
        //                foreach (DataRow dr in tbl.Rows)
        //                {
        //                    AttachmentItem item = DataRowToAttachmentItem(dr);

        //                    if (item != null)
        //                    {
        //                        item.Content = GetContentByItemId(item.Id);

        //                        attachmentItems.Add(item);
        //                    }
        //                }
        //                return attachmentItems;
        //            }
        //            else
        //                return null;
        //        }

        //        public AttachmentItem GetItemById(int itemId)
        //        {
        //            string sql = @"SELECT * 
        //                        FROM TB_INFRA_ATTACHMENT_ITEM ait 
        //                                WHERE ait.ISDELETE=0 AND ait.ATTACHMENT_ITEM_ID = :V_Id ";

        //            List<ParameterEntity> parList = new List<ParameterEntity>()
        //            {
        //                new ParameterEntity{ DbType= DbType.Int32, Name=":V_Id", Value=itemId}
        //            };

        //            DataTable tbl = OracleDBHelper.ExecuteDataTable(sql, parList);
        //            if (null != tbl && tbl.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in tbl.Rows)
        //                {
        //                    return DataRowToAttachmentItem(dr);
        //                }
        //            }

        //            return null;
        //        }
        //        #endregion

        //        #region DataRowToRecipient:文件项行行转换成实体
        //        private AttachmentItem DataRowToAttachmentItem(DataRow dr)
        //        {
        //            if (null == dr)
        //                return null;

        //            AttachmentItem entity = new AttachmentItem();
        //            if (dr["ATTACHMENT_ITEM_ID"] != DBNull.Value)
        //                entity.Id = Convert.ToInt32(dr["ATTACHMENT_ITEM_ID"]);
        //            if (dr["ATTACHMENT_INFO_ID"] != DBNull.Value)
        //                entity.AttachmentInfoId = Convert.ToInt32(dr["ATTACHMENT_INFO_ID"]);
        //            if (dr["FILE_PATH"] != DBNull.Value)
        //                entity.FilePath = dr["FILE_PATH"].ToString();
        //            if (dr["FILE_NAME"] != DBNull.Value)
        //                entity.FileName = dr["FILE_NAME"].ToString();
        //            if (dr["FILE_EXT"] != DBNull.Value)
        //                entity.FileExt = dr["FILE_EXT"].ToString();
        //            if (dr["FILE_SIZE"] != DBNull.Value)
        //                entity.FileSize = Convert.ToInt32(dr["FILE_SIZE"]);
        //            if (dr["IS_DB"] != DBNull.Value && "1".Equals(dr["IS_DB"].ToString()))
        //                entity.IsDB = true;
        //            else
        //                entity.IsDB = false;
        //            if (dr["CONTENT_TYPE"] != DBNull.Value)
        //                entity.ContentType = dr["CONTENT_TYPE"].ToString();

        //            if (dr["CREATOR"] != DBNull.Value)
        //                entity.CreatorId = Convert.ToInt32(dr["CREATOR"]);
        //            if (dr["CREATE_TIME"] != DBNull.Value)
        //                entity.CreateTime = Convert.ToDateTime(dr["CREATE_TIME"]);
        //            if (dr["LAST_OPERATOR"] != DBNull.Value)
        //                entity.LastOperatorId = Convert.ToInt32(dr["LAST_OPERATOR"]);
        //            if (dr["LAST_UPDATE_TIME"] != DBNull.Value)
        //                entity.LastUpdateTime = Convert.ToDateTime(dr["LAST_UPDATE_TIME"]);
        //            if (dr["ISDELETE"] != DBNull.Value)
        //                entity.IsDelete = Convert.ToBoolean(dr["ISDELETE"]);
        //            if (dr["VERSION"] != DBNull.Value)
        //                entity.Version = Convert.ToInt32(dr["VERSION"]);
        //            if (dr["UPLOAD_MODE"] != DBNull.Value)
        //                entity.UploadMode = (UploadMode)Convert.ToInt32(dr["UPLOAD_MODE"]);

        //            return entity;
        //        }
        //        #endregion

        //        #region GetContentByItemId:根据文件项编号获取内容
        //        /// <summary>
        //        /// 根据文件项编号获取内容
        //        /// </summary>
        //        public AttachmentContent GetContentByItemId(int itemId)
        //        {
        //            string sql = @"SELECT * 
        //                        FROM TB_INFRA_ATTACHMENT_CONTENT ac 
        //                                WHERE ac.ISDELETE=0 AND ac.ATTACHMENT_ITEM_ID = :V_Id ";

        //            List<ParameterEntity> parList = new List<ParameterEntity>()
        //            {
        //                new ParameterEntity{ DbType= DbType.Int32, Name=":V_Id", Value=itemId}
        //            };

        //            DataTable tbl = OracleDBHelper.ExecuteDataTable(sql, parList);
        //            if (null != tbl && tbl.Rows.Count > 0)
        //                return DataRowToAttachmentContent(tbl.Rows[0]);
        //            else
        //                return null;
        //        }
        //        #endregion

        //        #region DataRowToAttachmentContent:文件项内容行转换成实体
        //        private AttachmentContent DataRowToAttachmentContent(DataRow dr)
        //        {
        //            if (null == dr)
        //                return null;

        //            AttachmentContent entity = new AttachmentContent();
        //            if (dr["ATTACHMENT_CONTENT_ID"] != DBNull.Value)
        //                entity.Id = Convert.ToInt32(dr["ATTACHMENT_CONTENT_ID"]);
        //            if (dr["ATTACHMENT_ITEM_ID"] != DBNull.Value)
        //                entity.AttachmentItemId = Convert.ToInt32(dr["ATTACHMENT_ITEM_ID"]);

        //            if (dr["CONTENT"] != DBNull.Value)
        //                entity.Content = (byte[])dr["CONTENT"];

        //            if (dr["CREATOR"] != DBNull.Value)
        //                entity.CreatorId = Convert.ToInt32(dr["CREATOR"]);
        //            if (dr["CREATE_TIME"] != DBNull.Value)
        //                entity.CreateTime = Convert.ToDateTime(dr["CREATE_TIME"]);
        //            if (dr["LAST_OPERATOR"] != DBNull.Value)
        //                entity.LastOperatorId = Convert.ToInt32(dr["LAST_OPERATOR"]);
        //            if (dr["LAST_UPDATE_TIME"] != DBNull.Value)
        //                entity.LastUpdateTime = Convert.ToDateTime(dr["LAST_UPDATE_TIME"]);
        //            if (dr["ISDELETE"] != DBNull.Value)
        //                entity.IsDelete = Convert.ToBoolean(dr["ISDELETE"]);
        //            if (dr["VERSION"] != DBNull.Value)
        //                entity.Version = Convert.ToInt32(dr["VERSION"]);

        //            return entity;
        //        }
        //        #endregion

        //        #region FormatDictionaryKey:格式化返回的数据字典的Key
        //        private string FormatDictionaryKey(string key)
        //        {
        //            return string.Format(key + "/*{0}*/", Guid.NewGuid().ToString());
        //        }
        //        #endregion

        //        #region
        //        /// <summary>
        //        /// 搜索附件项
        //        /// </summary>
        //        /// <param name="pageIndex">分页索引</param>
        //        /// <param name="pageSize">分页数量</param>
        //        /// <param name="fileName">文件名称</param>
        //        /// <returns></returns>
        //        public PagedTable SearchAttachmentItem(int pageIndex = 1, int pageSize = 50, int attachmentInfoId = 0, string fileName = "", string fileExt = "")
        //        {
        //            if (pageIndex <= 0)
        //                pageIndex = 1;

        //            if (pageSize <= 0)
        //                pageSize = 50;

        //            string sqlString = "SELECT ATTACHMENT_ITEM_ID, " +
        //            "       FILE_PATH, " +
        //            "       FILE_NAME, " +
        //            "       FILE_EXT, " +
        //            "       FILE_SIZE, " +
        //            "       IS_DB, " +
        //            "       ATTACHMENT_INFO_ID, " +
        //            "       VERSION, " +
        //            "       CREATOR, " +
        //            "       CREATE_TIME, " +
        //            "       LAST_OPERATOR, " +
        //            "       LAST_UPDATE_TIME, " +
        //            "       ISDELETE, " +
        //            "       CONTENT_TYPE, " +
        //            "       UPLOAD_MODE " +
        //            "  FROM TB_INFRA_ATTACHMENT_ITEM I " +
        //            " WHERE 1 = 1 ";

        //            if (!string.IsNullOrEmpty(fileName))
        //            {
        //                sqlString += "   AND I.FILE_NAME LIKE '%" + fileName + "%'";
        //            }

        //            if (!string.IsNullOrEmpty(fileExt))
        //            {
        //                sqlString += "   AND I.FILE_EXT = '" + fileExt + "'";
        //            }

        //            if (attachmentInfoId > 0)
        //            {
        //                sqlString += "   AND I.ATTACHMENT_INFO_ID = " + attachmentInfoId + "  ";
        //            }

        //            sqlString += "   ORDER BY I.ATTACHMENT_ITEM_ID DESC";

        //            return OracleDBHelper.GetDataByPage(sqlString, pageIndex, pageSize, null);
        //        }
        //        #endregion

        //        #endregion

        //        #region 附件相关添加操作

        //        /// <summary>
        //        /// 添加附件信息
        //        /// </summary>
        //        /// <param name="info">附件信息</param>
        //        /// <param name="infoId">附件信息ID</param>
        //        /// <returns>执行SQL</returns>
        //        public KeyValuePair<string, List<ParameterEntity>> Add(AttachmentInfo info, out int infoId)
        //        {
        //            Int32 identity = OracleDBHelper.SelectTBID("SEQ_INFRA_ATTACHMENT_INFO", DataBaseType.DefaultDB);
        //            info.Id = infoId = identity;


        //            string sqlString = "INSERT INTO TB_INFRA_ATTACHMENT_INFO" +
        //            "  (ATTACHMENT_INFO_ID," +
        //            "   FUNCTION_NAME," +
        //            "   MODULE_NAME," +
        //            "   CREATOR," +
        //            "   CREATE_TIME," +
        //            "   LAST_OPERATOR," +
        //            "   LAST_UPDATE_TIME," +
        //            "   ISDELETE," +
        //            "   VERSION)" +
        //            "VALUES" +
        //            "  (:V_ATTACHMENT_INFO_ID," +
        //            "   :V_FUNCTION_NAME," +
        //            "   :V_MODULE_NAME," +
        //            "   :V_CREATOR," +
        //            "   :V_CREATE_TIME," +
        //            "   :V_LAST_OPERATOR," +
        //            "   :V_LAST_UPDATE_TIME," +
        //            "   :V_ISDELETE," +
        //            "   :V_VERSION)";


        //            List<ParameterEntity> paras = new List<ParameterEntity>
        //            {
        //                       new ParameterEntity(){ Name =":V_ATTACHMENT_INFO_ID", Value =info.Id,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_FUNCTION_NAME", Value =info.FunctionName,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_MODULE_NAME", Value =info.ModuleName,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_CREATOR", Value =info.CreatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_CREATE_TIME", Value =info.CreateTime,DbType =DbType.Date},
        //                       new ParameterEntity(){ Name =":V_LAST_OPERATOR", Value =info.LastOperatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_LAST_UPDATE_TIME", Value =info.LastUpdateTime,DbType =DbType.Date},
        //                       new ParameterEntity(){ Name =":V_ISDELETE", Value =info.IsDelete,DbType =DbType.Boolean},
        //                       new ParameterEntity(){ Name =":V_VERSION", Value =info.Version,DbType =DbType.Int32},
        //            };

        //            KeyValuePair<string, List<ParameterEntity>> keyVale = new KeyValuePair<string, List<ParameterEntity>>(sqlString, paras);

        //            return keyVale;
        //        }

        //        /// <summary>
        //        /// 添加附件项
        //        /// </summary>
        //        /// <param name="item">附件项</param>
        //        /// <param name="itemId">附件项ID</param>
        //        /// <returns>执行SQL</returns>
        //        public KeyValuePair<string, List<ParameterEntity>> Add(AttachmentItem item, out int itemId)
        //        {
        //            Int32 identity = OracleDBHelper.SelectTBID("SEQ_INFRA_ATTACHMENT_ITEM", DataBaseType.DefaultDB);
        //            item.Id = itemId = identity;


        //            string sqlString = "INSERT INTO TB_INFRA_ATTACHMENT_ITEM" +
        //            "  (ATTACHMENT_ITEM_ID," +
        //            "   FILE_PATH," +
        //            "   FILE_NAME," +
        //            "   FILE_EXT," +
        //            "   FILE_SIZE," +
        //            "   IS_DB," +
        //            "   ATTACHMENT_INFO_ID," +
        //            "   VERSION," +
        //            "   CREATOR," +
        //            "   CREATE_TIME," +
        //            "   LAST_OPERATOR," +
        //            "   LAST_UPDATE_TIME," +
        //            "   ISDELETE," +
        //            "   CONTENT_TYPE," +
        //            "   UPLOAD_MODE)" +
        //            "VALUES" +
        //            "  (:V_ATTACHMENT_ITEM_ID," +
        //            "   :V_FILE_PATH," +
        //            "   :V_FILE_NAME," +
        //            "   :V_FILE_EXT," +
        //            "   :V_FILE_SIZE," +
        //            "   :V_IS_DB," +
        //            "   :V_ATTACHMENT_INFO_ID," +
        //            "   :V_VERSION," +
        //            "   :V_CREATOR," +
        //            "   :V_CREATE_TIME," +
        //            "   :V_LAST_OPERATOR," +
        //            "   :V_LAST_UPDATE_TIME," +
        //            "   :V_ISDELETE," +
        //            "   :V_CONTENT_TYPE," +
        //            "   :V_UPLOAD_MODE) ";


        //            List<ParameterEntity> paras = new List<ParameterEntity>
        //            {
        //                       new ParameterEntity(){ Name =":V_ATTACHMENT_ITEM_ID", Value =item.Id,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_ATTACHMENT_INFO_ID", Value =item.AttachmentInfoId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_FILE_PATH", Value =item.FilePath,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_FILE_NAME", Value =item.FileName,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_CONTENT_TYPE", Value =item.ContentType,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_FILE_EXT", Value =item.FileExt,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_VERSION", Value =item.Version,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_IS_DB", Value =item.IsDB,DbType =DbType.Boolean},
        //                       new ParameterEntity(){ Name =":V_FILE_SIZE", Value =item.FileSize,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_ISDELETE", Value =item.IsDelete,DbType =DbType.Boolean},
        //                       new ParameterEntity(){ Name =":V_CREATOR", Value =item.CreatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_LAST_OPERATOR", Value =item.LastOperatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_CREATE_TIME", Value =item.CreateTime,DbType =DbType.Date},
        //                       new ParameterEntity(){ Name =":V_LAST_UPDATE_TIME", Value =item.LastUpdateTime,DbType =DbType.Date},
        //                       new ParameterEntity(){ Name =":V_UPLOAD_MODE", Value = (int)item.UploadMode,DbType =DbType.Int32},
        //            };

        //            KeyValuePair<string, List<ParameterEntity>> keyVale = new KeyValuePair<string, List<ParameterEntity>>(sqlString, paras);

        //            return keyVale;
        //        }

        //        /// <summary>
        //        /// 添加附件内容
        //        /// </summary>
        //        /// <param name="content">附件内容</param>
        //        /// <param name="contentId">附件内容ID</param>
        //        /// <returns>执行SQL</returns>
        //        public KeyValuePair<string, List<ParameterEntity>> Add(AttachmentContent content, out int contentId)
        //        {
        //            Int32 identity = OracleDBHelper.SelectTBID("SEQ_INFRA_ATTACHMENT_CONTENT", DataBaseType.DefaultDB);
        //            content.Id = contentId = identity;


        //            string sqlString = "INSERT INTO TB_INFRA_ATTACHMENT_CONTENT" +
        //            "  (ATTACHMENT_CONTENT_ID," +
        //            "   CONTENT," +
        //            "   ATTACHMENT_ITEM_ID," +
        //            "   VERSION," +
        //            "   CREATOR," +
        //            "   CREATE_TIME," +
        //            "   LAST_OPERATOR," +
        //            "   LAST_UPDATE_TIME," +
        //            "   ISDELETE)" +
        //            "VALUES" +
        //            "  (:V_ATTACHMENT_CONTENT_ID," +
        //            "   :V_CONTENT," +
        //            "   :V_ATTACHMENT_ITEM_ID," +
        //            "   :V_VERSION," +
        //            "   :V_CREATOR," +
        //            "   :V_CREATE_TIME," +
        //            "   :V_LAST_OPERATOR," +
        //            "   :V_LAST_UPDATE_TIME," +
        //            "   :V_ISDELETE)";


        //            List<ParameterEntity> paras = new List<ParameterEntity>
        //            {
        //                       new ParameterEntity(){ Name =":V_ATTACHMENT_CONTENT_ID", Value =content.Id,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_ATTACHMENT_ITEM_ID", Value =content.AttachmentItemId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_CONTENT", Value =content.Content,DbType =DbType.Binary},
        //                       new ParameterEntity(){ Name =":V_VERSION", Value =content.Version,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_ISDELETE", Value =content.IsDelete,DbType =DbType.Boolean},
        //                       new ParameterEntity(){ Name =":V_CREATOR", Value =content.CreatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_LAST_OPERATOR", Value =content.LastOperatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_CREATE_TIME", Value =content.CreateTime,DbType =DbType.Date},
        //                       new ParameterEntity(){ Name =":V_LAST_UPDATE_TIME", Value =content.LastUpdateTime,DbType =DbType.Date}
        //            };

        //            KeyValuePair<string, List<ParameterEntity>> keyVale = new KeyValuePair<string, List<ParameterEntity>>(sqlString, paras);

        //            return keyVale;
        //        }

        //        /// <summary>
        //        /// 执行添加附件信息
        //        /// </summary>
        //        /// <param name="info">附件信息</param>
        //        /// <returns>附件信息ID</returns>
        //        public int AddExecute(AttachmentInfo info)
        //        {
        //            int infoId = 0;

        //            var kv = this.Add(info, out infoId);

        //            OracleDBHelper.ExecuteNonQuery(kv.Key, kv.Value);

        //            return infoId;
        //        }

        //        /// <summary>
        //        /// 执行添加附件项
        //        /// </summary>
        //        /// <param name="item">附件项</param>
        //        /// <returns>附件项ID</returns>
        //        public int AddExecute(AttachmentItem item)
        //        {
        //            int itemId = 0;

        //            var kv = this.Add(item, out itemId);

        //            OracleDBHelper.ExecuteNonQuery(kv.Key, kv.Value);

        //            return itemId;
        //        }

        //        /// <summary>
        //        /// 执行添加附件内容
        //        /// </summary>
        //        /// <param name="content">附件内容</param>
        //        /// <returns>附件内容ID</returns>
        //        public int AddExecute(AttachmentContent content)
        //        {
        //            int contentId = 0;

        //            var kv = this.Add(content, out contentId);

        //            OracleDBHelper.ExecuteNonQuery(kv.Key, kv.Value);

        //            return contentId;
        //        }
        //        #endregion

        //        #region 附件相关更新操作

        //        /// <summary>
        //        /// 更新附件内容
        //        /// </summary>
        //        /// <param name="content">附件内容</param>
        //        /// <returns>执行SQL</returns>
        //        public KeyValuePair<string, List<ParameterEntity>> Update(AttachmentContent content)
        //        {
        //            if (content == null)
        //                throw new ArgumentNullException("content");

        //            string sql =
        //                @"
        //                 UPDATE TB_INFRA_ATTACHMENT_CONTENT
        //                   SET 
        //                       CONTENT = :V_CONTENT,
        //                       ATTACHMENT_ITEM_ID = :V_ATTACHMENT_ITEM_ID,
        //                       VERSION = :V_VERSION,
        //                       CREATOR = :V_CREATOR,
        //                       CREATE_TIME = :V_CREATE_TIME,
        //                       LAST_OPERATOR = :V_LAST_OPERATOR,
        //                       LAST_UPDATE_TIME = :V_LAST_UPDATE_TIME,
        //                       ISDELETE = :V_ISDELETE
        //                 WHERE ATTACHMENT_CONTENT_ID  = :V_ATTACHMENT_CONTENT_ID ";

        //            List<ParameterEntity> paras = new List<ParameterEntity>
        //            {
        //                       new ParameterEntity(){ Name =":V_ATTACHMENT_CONTENT_ID", Value =content.Id,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_ATTACHMENT_ITEM_ID", Value =content.AttachmentItemId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_CONTENT", Value =content.Content,DbType =DbType.Binary},
        //                       new ParameterEntity(){ Name =":V_VERSION", Value =content.Version,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_ISDELETE", Value =content.IsDelete,DbType =DbType.Boolean},
        //                       new ParameterEntity(){ Name =":V_CREATOR", Value =content.CreatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_LAST_OPERATOR", Value =content.LastOperatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_CREATE_TIME", Value =content.CreateTime,DbType =DbType.Date},
        //                       new ParameterEntity(){ Name =":V_LAST_UPDATE_TIME", Value =content.LastUpdateTime,DbType =DbType.Date}
        //            };

        //            KeyValuePair<string, List<ParameterEntity>> keyVale = new KeyValuePair<string, List<ParameterEntity>>(sql, paras);

        //            return keyVale;
        //        }

        //        /// <summary>
        //        /// 更新附件信息
        //        /// </summary>
        //        /// <param name="info">附件信息</param>
        //        /// <returns>执行SQL</returns>
        //        public KeyValuePair<string, List<ParameterEntity>> Update(AttachmentInfo info)
        //        {
        //            if (info == null)
        //                throw new ArgumentNullException("info");

        //            const string sql = @"UPDATE TB_INFRA_ATTACHMENT_INFO
        //                           SET
        //                               FUNCTION_NAME = :V_FUNCTION_NAME,
        //                               MODULE_NAME = :V_MODULE_NAME,
        //                               CREATOR = :V_CREATOR,
        //                               CREATE_TIME = :V_CREATE_TIME,
        //                               LAST_OPERATOR = :V_LAST_OPERATOR,
        //                               LAST_UPDATE_TIME = :V_LAST_UPDATE_TIME,
        //                               ISDELETE = :V_ISDELETE,
        //                               VERSION = :V_VERSION
        //                         WHERE ATTACHMENT_INFO_ID = :V_ATTACHMENT_INFO_ID";

        //            List<ParameterEntity> paras = new List<ParameterEntity>
        //            {
        //                       new ParameterEntity(){ Name =":V_ATTACHMENT_INFO_ID", Value =info.Id,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_FUNCTION_NAME", Value =info.FunctionName,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_MODULE_NAME", Value =info.ModuleName,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_VERSION", Value =info.Version,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_ISDELETE", Value =info.IsDelete,DbType =DbType.Boolean},
        //                       new ParameterEntity(){ Name =":V_CREATOR", Value =info.CreatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_LAST_OPERATOR", Value =info.LastOperatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_CREATE_TIME", Value =info.CreateTime,DbType =DbType.Date},
        //                       new ParameterEntity(){ Name =":V_LAST_UPDATE_TIME", Value =info.LastUpdateTime,DbType =DbType.Date},
        //            };

        //            KeyValuePair<string, List<ParameterEntity>> keyVale = new KeyValuePair<string, List<ParameterEntity>>(sql, paras);

        //            return keyVale;
        //        }

        //        /// <summary>
        //        /// 更新附件项
        //        /// </summary>
        //        /// <param name="item">附件项</param>
        //        /// <returns>执行SQL</returns>
        //        public KeyValuePair<string, List<ParameterEntity>> Update(AttachmentItem item)
        //        {
        //            if (item == null)
        //                throw new ArgumentNullException("item");

        //            const string sqlString = "" +
        //                                     "UPDATE TB_INFRA_ATTACHMENT_ITEM" +
        //                                     "   SET FILE_PATH          = :V_FILE_PATH," +
        //                                     "       FILE_NAME          = :V_FILE_NAME," +
        //                                     "       FILE_EXT           = :V_FILE_EXT," +
        //                                     "       FILE_SIZE          = :V_FILE_SIZE," +
        //                                     "       IS_DB              = :V_IS_DB," +
        //                                     "       ATTACHMENT_INFO_ID = :V_ATTACHMENT_INFO_ID," +
        //                                     "       VERSION            = :V_VERSION," +
        //                                     "       CREATOR            = :V_CREATOR," +
        //                                     "       CREATE_TIME        = :V_CREATE_TIME," +
        //                                     "       LAST_OPERATOR      = :V_LAST_OPERATOR," +
        //                                     "       LAST_UPDATE_TIME   = :V_LAST_UPDATE_TIME," +
        //                                     "       ISDELETE           = :V_ISDELETE," +
        //                                     "       UPLOAD_MODE           = :V_UPLOAD_MODE," +
        //                                     "       CONTENT_TYPE       = :V_CONTENT_TYPE" +
        //                                     " WHERE ATTACHMENT_ITEM_ID = :V_ATTACHMENT_ITEM_ID;";

        //            List<ParameterEntity> paras = new List<ParameterEntity>
        //            {
        //                       new ParameterEntity(){ Name =":V_ATTACHMENT_ITEM_ID", Value =item.Id,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_ATTACHMENT_INFO_ID", Value =item.AttachmentInfoId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_FILE_PATH", Value =item.FilePath,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_FILE_NAME", Value =item.FileName,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_CONTENT_TYPE", Value =item.ContentType,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_FILE_EXT", Value =item.FileExt,DbType =DbType.String},
        //                       new ParameterEntity(){ Name =":V_VERSION", Value =item.Version,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_IS_DB", Value =item.IsDB,DbType =DbType.Boolean},
        //                       new ParameterEntity(){ Name =":V_FILE_SIZE", Value =item.FileSize,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_ISDELETE", Value =item.IsDelete,DbType =DbType.Boolean},
        //                       new ParameterEntity(){ Name =":V_CREATOR", Value =item.CreatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_LAST_OPERATOR", Value =item.LastOperatorId,DbType =DbType.Int32},
        //                       new ParameterEntity(){ Name =":V_CREATE_TIME", Value =item.CreateTime,DbType =DbType.Date},
        //                       new ParameterEntity(){ Name =":V_LAST_UPDATE_TIME", Value =item.LastUpdateTime,DbType =DbType.Date},
        //                       new ParameterEntity(){ Name =":V_UPLOAD_MODE", Value = (int)item.UploadMode,DbType =DbType.Int32},
        //            };

        //            KeyValuePair<string, List<ParameterEntity>> keyVale = new KeyValuePair<string, List<ParameterEntity>>(sqlString, paras);

        //            return keyVale;
        //        }

        //        /// <summary>
        //        /// 执行更新附件内容
        //        /// </summary>
        //        /// <param name="content">附件内容</param>
        //        /// <returns>是否成功</returns>
        //        public bool UpdateExecute(AttachmentContent content)
        //        {
        //            var kv = this.Update(content);

        //            return OracleDBHelper.ExecuteNonQuery(kv.Key, kv.Value) > 0;
        //        }

        //        /// <summary>
        //        /// 执行更新附件信息
        //        /// </summary>
        //        /// <param name="info">附件信息</param>
        //        /// <returns>是否成功</returns>
        //        public bool UpdateExecute(AttachmentInfo info)
        //        {
        //            var kv = this.Update(info);

        //            return OracleDBHelper.ExecuteNonQuery(kv.Key, kv.Value) > 0;
        //        }

        //        /// <summary>
        //        /// 执行更新附件项
        //        /// </summary>
        //        /// <param name="item">附件项</param>
        //        /// <returns>是否成功</returns>
        //        public bool UpdateExecute(AttachmentItem item)
        //        {
        //            var kv = this.Update(item);

        //            return OracleDBHelper.ExecuteNonQuery(kv.Key, kv.Value) > 0;
        //        }

        //        #endregion

        //        #region  附件相关删除操作

        //        /// <summary>
        //        /// 删除附件内容
        //        /// </summary>
        //        /// <param name="content">附件内容</param>
        //        /// <returns>执行SQL</returns>
        //        public KeyValuePair<string, List<ParameterEntity>> Delete(AttachmentContent content)
        //        {
        //            if (content == null)
        //                throw new ArgumentNullException("content");

        //            string sqlString = "DELETE TB_INFRA_ATTACHMENT_CONTENT" +
        //            " WHERE ATTACHMENT_CONTENT_ID = :V_ATTACHMENT_CONTENT_ID;";

        //            List<ParameterEntity> paras = new List<ParameterEntity>
        //            {
        //                new ParameterEntity(){ Name =":V_ATTACHMENT_CONTENT_ID", Value =content.Id,DbType =DbType.Int32}
        //            };

        //            KeyValuePair<string, List<ParameterEntity>> keyVale = new KeyValuePair<string, List<ParameterEntity>>(sqlString, paras);

        //            return keyVale;
        //        }

        //        /// <summary>
        //        /// 删除附件信息
        //        /// </summary>
        //        /// <param name="info">附件信息</param>
        //        /// <returns>执行SQL</returns>
        //        public KeyValuePair<string, List<ParameterEntity>> Delete(AttachmentInfo info)
        //        {
        //            if (info == null)
        //                throw new ArgumentNullException("info");


        //            string sqlString = "DELETE TB_INFRA_ATTACHMENT_INFO" +
        //            " WHERE ATTACHMENT_INFO_ID = :V_ATTACHMENT_INFO_ID;";

        //            List<ParameterEntity> paras = new List<ParameterEntity>
        //            {
        //                new ParameterEntity(){ Name =":V_ATTACHMENT_INFO_ID", Value =info.Id,DbType =DbType.Int32}
        //            };

        //            KeyValuePair<string, List<ParameterEntity>> keyVale = new KeyValuePair<string, List<ParameterEntity>>(sqlString, paras);

        //            return keyVale;
        //        }

        //        /// <summary>
        //        /// 删除附件项
        //        /// </summary>
        //        /// <param name="item">附件项</param>
        //        /// <returns>执行SQL</returns>
        //        public KeyValuePair<string, List<ParameterEntity>> Delete(AttachmentItem item)
        //        {
        //            if (item == null)
        //                throw new ArgumentNullException("item");

        //            string sqlString = "DELETE TB_INFRA_ATTACHMENT_ITEM" +
        //            " WHERE ATTACHMENT_ITEM_ID = :V_ATTACHMENT_ITEM_ID ";

        //            List<ParameterEntity> paras = new List<ParameterEntity>
        //            {
        //                new ParameterEntity(){ Name =":V_ATTACHMENT_ITEM_ID", Value =item.Id,DbType =DbType.Int32}
        //            };

        //            KeyValuePair<string, List<ParameterEntity>> keyVale = new KeyValuePair<string, List<ParameterEntity>>(sqlString, paras);

        //            return keyVale;
        //        }

        //        /// <summary>
        //        /// 执行删除附件内容
        //        /// </summary>
        //        /// <param name="content">附件内容</param>
        //        /// <returns>是否成功</returns>
        //        public bool DeleteExecute(AttachmentContent content)
        //        {
        //            var kv = this.Delete(content);

        //            return OracleDBHelper.ExecuteNonQuery(kv.Key, kv.Value) > 0;
        //        }

        //        /// <summary>
        //        /// 执行删除附件信息
        //        /// </summary>
        //        /// <param name="info">附件信息</param>
        //        /// <returns>是否成功</returns>
        //        public bool DeleteExecute(AttachmentInfo info)
        //        {
        //            var kv = this.Delete(info);

        //            return OracleDBHelper.ExecuteNonQuery(kv.Key, kv.Value) > 0;
        //        }

        //        /// <summary>
        //        /// 执行删除附件项
        //        /// </summary>
        //        /// <param name="item">附件项</param>
        //        /// <returns>是否成功</returns>
        //        public bool DeleteExecute(AttachmentItem item)
        //        {
        //            var kv = this.Delete(item);

        //            return OracleDBHelper.ExecuteNonQuery(kv.Key, kv.Value) > 0;
        //        }

        //        #endregion
    }
}