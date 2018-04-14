using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace ZGW.GMS.Core.Data
{
    public class DBManager
    {
        /// <summary>
        /// 根据连接不同访问不同的数据库对象
        /// </summary>
        /// <param name="dbT"></param>
        /// <returns></returns>
        public static OracleDatabase CreateDataBase(DataBaseType dbT)
        {
            string connectStr = "";
            switch (dbT)
            {
                case DataBaseType.DefaultDB:
                    connectStr = ConfigurationManager.ConnectionStrings["DefaultDB"].ConnectionString;
                    break;
                case DataBaseType.FileDB:
                    connectStr = ConfigurationManager.ConnectionStrings["FileDB"].ConnectionString;
                    break;
                case DataBaseType.LogDB:
                    connectStr = ConfigurationManager.ConnectionStrings["LogDB"].ConnectionString;
                    break;
                case DataBaseType.HistoryDB:
                    connectStr = ConfigurationManager.ConnectionStrings["HistoryDB"].ConnectionString;
                    break;
                case DataBaseType.OMSOldDB:
                    connectStr = ConfigurationManager.ConnectionStrings["OMSOldDB"].ConnectionString;
                    break;

                case DataBaseType.OMSNewHealthDB:
                    connectStr = ConfigurationManager.ConnectionStrings["OMSNewHealthDB"].ConnectionString;
                    break;
            }
            try
            {
                OracleDatabase db = new OracleDatabase(connectStr);
                return db;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("数据库连接错误");
            }
        }
    }

    /// <summary>
    /// 连接数据库类型
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// 链接默认数据库
        /// </summary>
        DefaultDB = 1,

        /// <summary>
        /// 链接文件数据库
        /// </summary>
        FileDB = 2,

        /// <summary>
        /// 链接日志数据库
        /// </summary>
        LogDB = 3,

        /// <summary>
        /// 历史数据库
        /// </summary>
        HistoryDB = 4,

        /// <summary>
        /// 原有OMS数据库
        /// </summary>
        OMSOldDB = 5,

        /// <summary>
        /// OMS新的健康中心数据库
        /// </summary>
        OMSNewHealthDB = 6,



    }
}
