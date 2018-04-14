using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ZGW.GMS.Core.Mvc;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// Result的扩展方法
    /// </summary>
    public static class PagedResultExtension
    {
        /// <summary>
        /// 将PagedList数据转化为JsonResult.
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="data">分页数据</param>
        /// <returns>JsonResult对象</returns>
        public static JsonResult ToJsonResult<T>(this PagedList<T> data)
        {
            JsonResult result = new JsonResult();
            result.Data = new PagedListJsonWrapper<T>(data);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        /// <summary>
        /// 将IEnumarable数据转化为JsonResult.
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="data">列表数据</param>
        /// <returns>JsonResult对象</returns>
        public static JsonResult ToJsonResult<T>(this IEnumerable<T> data)
        {
            var pagedDate = new PagedList<T>(data, 1, data.Count(), data.Count());

            JsonResult result = new JsonResult();
            result.Data = new PagedListJsonWrapper<T>(pagedDate);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        /// <summary>
        /// 将PagedTable的数据转化为JsonResult.
        /// </summary>
        /// <param name="data">按DataTable的分页数据</param>
        /// <returns>JsonResult对象</returns>
        public static JsonResult ToJsonResult(this PagedTable data)
        {
            JsonResult result = new TextJsonResult();

            StringBuilder sbJson = new StringBuilder();
            StringBuilder sbRows = new StringBuilder();
            sbRows.Append("[");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (DataRow row in data.Table.Rows)
            {
                sbRows.Append("{");
                foreach (DataColumn column in data.Table.Columns)
                {
                    sbRows.AppendFormat("\"{0}\":{1},", column.ColumnName, serializer.Serialize(row[column]));
                }
                sbRows.TrimEnd(',');
                sbRows.Append("},");
            }
            sbRows.TrimEnd(',');
            sbRows.Append("]");

            sbJson.AppendFormat("{{\"page\":{0},\"total\":{1},\"records\":{2},\"rows\":{3}}}",
                data.CurrentPageIndex,
                data.TotalPageCount,
                data.TotalItemCount,
                sbRows);

            result.Data = sbJson;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        /// <summary>
        /// 将PagedTable的数据转化为JsonResult.
        /// </summary>
        /// <param name="data">按DataTable的分页数据</param>
        /// <returns>JsonResult对象</returns>
        /// <param name="isHhMmDd">是否需要时分秒 </param>
        public static JsonResult ToJsonResultWithDateTime(this PagedTable data, bool isHhMmDd)
        {
            JsonResult result = new TextJsonResult();

            StringBuilder sbJson = new StringBuilder();
            StringBuilder sbRows = new StringBuilder();
            sbRows.Append("[");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (DataRow row in data.Table.Rows)
            {
                sbRows.Append("{");
                foreach (DataColumn column in data.Table.Columns)
                {
                    if (column.DataType == typeof(DateTime))
                    {
                        DateTime time = DateTime.Now;
                        string strData = string.Empty;
                        if (DateTime.TryParse(row[column] + string.Empty, out time))
                        {
                            //日期是最大值 或最小值 时，设置为空
                            if (time.Year == 9999 || time.Year == default(DateTime).Year || time.Year == DateTime.MinValue.Year)
                            {
                                strData = string.Empty;
                            }
                            else
                            {
                                if (isHhMmDd)
                                {
                                    strData = time.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    strData = time.ToString("yyyy-MM-dd");
                                }
                            }
                        }
                        else
                        {
                            strData = row[column] + string.Empty;
                        }

                        sbRows.AppendFormat("\"{0}\":{1},", column.ColumnName, serializer.Serialize(strData));
                    }
                    else
                    {
                        sbRows.AppendFormat("\"{0}\":{1},", column.ColumnName, serializer.Serialize(row[column]));
                    }
                }
                sbRows.TrimEnd(',');
                sbRows.Append("},");
            }
            sbRows.TrimEnd(',');
            sbRows.Append("]");

            sbJson.AppendFormat("{{\"page\":{0},\"total\":{1},\"records\":{2},\"rows\":{3}}}",
                data.CurrentPageIndex,
                data.TotalPageCount,
                data.TotalItemCount,
                sbRows);

            result.Data = sbJson;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        /// <summary>
        /// 将PagedList数据转化为JsonResult.
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="data">分页数据</param>
        /// <returns>JsonResult对象</returns>
        public static JsonResult ToJsonResultWithDateTime<T>(this PagedList<T> data)
        {
            NewtonJsonResultWithDateTime result = new NewtonJsonResultWithDateTime();
            result.Data = new PagedListJsonWrapper<T>(data);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        /// <summary>
        /// 将IEnumarable数据转化为JsonResult.
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="data">列表数据</param>
        /// <returns>JsonResult对象</returns>
        public static JsonResult ToJsonResultWithDateTime<T>(this IEnumerable<T> data)
        {
            var pagedDate = new PagedList<T>(data, 1, data.Count(), data.Count());

            NewtonJsonResultWithDateTime result = new NewtonJsonResultWithDateTime();
            result.Data = new PagedListJsonWrapper<T>(pagedDate);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        /// <summary>
        /// 将Object对象转化为JsonResult
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static JsonResult ToJsonResultWithObject(this object obj)
        {
            JsonResult result = new JsonResult();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);

            result.Data = Encoding.UTF8.GetString(stream.ToArray());
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }
    }
}
