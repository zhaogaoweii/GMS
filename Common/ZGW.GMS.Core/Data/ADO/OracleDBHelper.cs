using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System.Data;
using System.Data.Common;
using ZGW.GMS.Core.Mvc;
using Oracle.DataAccess.Types;

namespace ZGW.GMS.Core.Data
{
    /// <summary>
    /// 数据操作
    /// </summary>
    public class OracleDBHelper
    {


        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            DataTable dt = OracleDBHelper.ExecuteDataTable(strSql, null, DataBaseType.DefaultDB);

            int cmdresult = 0;
            if ((Object.Equals(dt, null)) || (Object.Equals(dt, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = dt.Rows.Count;
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 功能:对传入的SQL执行增、改、删等操作        
        /// 日期:2013-05-15
        /// </summary>
        /// <param name="sql">要执行的SQl</param>
        /// <param name="parList">参数列表</param>
        /// <param name="dbT">执行的数据库</param>
        /// <returns></returns>    
        public static int ExecuteNonQuery(string sql, List<ParameterEntity> parList, DataBaseType dbT)
        {
            try
            {
                OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
                DbCommand cmd = oraDb.GetSqlStringCommand(sql);
                if (parList != null && parList.Count > 0)
                {
                    foreach (ParameterEntity par in parList)
                    {
                        oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                    }
                }
                return oraDb.ExecuteNonQuery(cmd);
            }
            catch (Exception e)
            {
                //return 0;
                throw e;
            }
        }

        /// <summary>
        /// 功能:对传入的SQL执行增、改、删等操作        
        /// 日期:2013-05-15
        /// </summary>
        /// <param name="sql">要执行的SQl</param>
        /// <param name="parList">参数列表</param>        
        /// <returns></returns>    
        public static int ExecuteNonQuery(string sql, List<ParameterEntity> parList)
        {
            return OracleDBHelper.ExecuteNonQuery(sql, parList, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能:对传入的SQL执行增、改、删等操作        
        /// 日期:2013-05-15
        /// </summary>
        /// <param name="sql">要执行的SQl</param>
        /// <param name="parList">参数列表</param>        
        /// <returns></returns>    
        public static int ExecuteNonQuery(string sql)
        {
            return OracleDBHelper.ExecuteNonQuery(sql, null, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能:对传入的SQL执行增、改、删等操作【附加事物】     
        /// 日期:2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>
        /// <param name="tran">事物</param>
        /// <param name="dbT">执行的库</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, List<ParameterEntity> parList, DbTransaction tran, DataBaseType dbT)
        {
            try
            {
                if (string.IsNullOrEmpty(sql)) { return 1; }
                OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
                DbCommand cmd = oraDb.GetSqlStringCommand(sql);
                if (parList != null && parList.Count > 0)
                {
                    foreach (ParameterEntity par in parList)
                    {
                        oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                    }
                }
                return oraDb.ExecuteNonQuery(cmd, tran);
            }
            catch (Exception e)
            {
                //return 0;
                throw e;
            }
        }
        /// <summary>
        /// 执行数据库语句支持存储过程 2013-07-01
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parList"></param>
        /// <param name="dbT"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, List<ParameterEntity> parList, DataBaseType dbT, CommandType cmdType)
        {
            try
            {
                DbCommand cmd;
                OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
                if (cmdType == CommandType.StoredProcedure) cmd = oraDb.GetStoredProcCommand(sql);
                else cmd = oraDb.GetSqlStringCommand(sql);
                cmd.CommandType = cmdType;
                if (parList != null && parList.Count > 0)
                {
                    foreach (ParameterEntity par in parList)
                    {
                        oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                    }
                }
                return oraDb.ExecuteNonQuery(cmd);
            }
            catch (Exception e)
            {
                //return 0;
                throw e;
            }
        }

        /// <summary>
        /// 执行数据库语句支持存储过程,含输出参数 2013-07-01
        /// </summary>
        public static bool ExecuteNonQueryWithOutResult(string sql, List<ParameterEntity> parList, string outParName, out string outParValue, DataBaseType dbT, CommandType cmdType)
        {
            try
            {
                DbCommand cmd;
                OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
                if (cmdType == CommandType.StoredProcedure) cmd = oraDb.GetStoredProcCommand(sql);
                else cmd = oraDb.GetSqlStringCommand(sql);
                cmd.CommandType = cmdType;
                if (parList != null && parList.Count > 0)
                {
                    foreach (ParameterEntity par in parList)
                    {
                        oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                    }
                }
                if (!string.IsNullOrEmpty(outParName))
                {
                    oraDb.AddOutParameter(cmd, outParName, DbType.String, 1024);
                }
                oraDb.ExecuteNonQuery(cmd);
                if (!string.IsNullOrEmpty(outParName))
                {
                    outParValue = cmd.Parameters[outParName].Value.ToString();
                }
                else
                {
                    outParValue = "";
                }
                return true;
            }
            catch (Exception e)
            {
                outParValue = e.Message;
                throw e;
            }
        }

        /// <summary>
        /// 执行数据库语句支持存储过程,含输出参数 2013-07-01
        /// </summary>
        /// <param name="sql">存储过程名称</param>
        /// <param name="parList">参数</param>
        /// <param name="outParIdName">out 参数1名称</param>
        /// <param name="outParIdValue">out 参数1值（必须是int/long）</param>
        /// <param name="outParName">out 参数2名称</param>
        /// <param name="outParValue">out 参数2值 </param>
        /// <param name="dbT">数据库</param>
        /// <param name="cmdType">sql类型</param>
        /// <returns></returns>
        public static bool ExecuteNonQueryWithOutResult(string sql, List<ParameterEntity> parList, string outParIdName, out string outParIdValue, string outParName, out string outParValue, DataBaseType dbT, CommandType cmdType)
        {
            try
            {
                DbCommand cmd;
                OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
                if (cmdType == CommandType.StoredProcedure) cmd = oraDb.GetStoredProcCommand(sql);
                else cmd = oraDb.GetSqlStringCommand(sql);
                cmd.CommandType = cmdType;
                if (parList != null && parList.Count > 0)
                {
                    foreach (ParameterEntity par in parList)
                    {
                        oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                    }
                }
                if (!string.IsNullOrEmpty(outParIdName))
                {
                    oraDb.AddOutParameter(cmd, outParIdName, DbType.Int64, 1024);
                }
                if (!string.IsNullOrEmpty(outParName))
                {
                    oraDb.AddOutParameter(cmd, outParName, DbType.String, 1024);
                }
                oraDb.ExecuteNonQuery(cmd);
                if (!string.IsNullOrEmpty(outParIdName))
                {
                    outParIdValue = cmd.Parameters[outParIdName].Value.ToString();
                }
                else
                {
                    outParIdValue = "";
                }
                if (!string.IsNullOrEmpty(outParName))
                {
                    outParValue = cmd.Parameters[outParName].Value.ToString();
                }
                else
                {
                    outParValue = "";
                }
                return true;
            }
            catch (Exception e)
            {
                outParValue = e.Message;
                throw e;
            }
        }



        /// <summary>
        /// 功能:对传入的SQL执行增、改、删等操作【附加事物】     
        /// 日期:2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>
        /// <param name="tran">事物</param>      
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, List<ParameterEntity> parList, DbTransaction tran)
        {
            return OracleDBHelper.ExecuteNonQuery(sql, parList, tran, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能：对传入的SQL执行查询操作，返回DataSet  
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>
        /// <param name="dbT">执行库</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql, List<ParameterEntity> parList, DataBaseType dbT)
        {
            try
            {
                OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
                DbCommand cmd = oraDb.GetSqlStringCommand(sql);
                if (parList != null && parList.Count > 0)
                {
                    foreach (ParameterEntity par in parList)
                    {
                        oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                    }
                }
                return oraDb.ExecuteDataSet(cmd);
            }
            catch (Exception e)
            {
                //return null;
                throw e;
            }
        }

        /// <summary>
        /// 功能：对传入的SQL执行查询操作，返回DataSet  
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>       
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql, List<ParameterEntity> parList)
        {
            return OracleDBHelper.ExecuteDataSet(sql, parList, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能：对传入的SQL执行查询操作，返回DataSet  
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>       
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql)
        {
            return OracleDBHelper.ExecuteDataSet(sql, null, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能：对传入的SQL执行查询操作，返回DataTable
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>
        /// <param name="dbT">执行库</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, List<ParameterEntity> parList, DataBaseType dbT)
        {
            try
            {
                OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
                DbCommand cmd = oraDb.GetSqlStringCommand(sql);
                if (parList != null && parList.Count > 0)
                {
                    foreach (ParameterEntity par in parList)
                    {
                        oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                    }
                }
                return oraDb.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception e)
            {
                //return null;
                throw e;
            }
        }

        /// <summary>
        /// 功能：对传入的SQL执行查询操作，返回DataTable
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>       
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, List<ParameterEntity> parList)
        {
            return OracleDBHelper.ExecuteDataTable(sql, parList, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能：对传入的SQL执行查询操作，返回DataTable
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql)
        {
            return OracleDBHelper.ExecuteDataTable(sql, null, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能：对传入的SQL执行查询操作，返回DataRow    
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>
        /// <param name="dbT">执行库</param>
        /// <returns></returns>
        public static DataRow ExecuteDataRow(string sql, List<ParameterEntity> parList, DataBaseType dbT)
        {
            try
            {
                OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
                DbCommand cmd = oraDb.GetSqlStringCommand(sql);
                if (parList != null && parList.Count > 0)
                {
                    foreach (ParameterEntity par in parList)
                    {
                        oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                    }
                }
                DataSet ds = oraDb.ExecuteDataSet(cmd);
                return ValidationHelper.CheckDs(ds) ? ds.Tables[0].Rows[0] : null;
            }
            catch (Exception e)
            {
                //return null;
                throw e;
            }
        }

        /// <summary>
        /// 功能：对传入的SQL执行查询操作，返回DataRow    
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>        
        /// <returns></returns>
        public static DataRow ExecuteDataRow(string sql, List<ParameterEntity> parList)
        {
            return OracleDBHelper.ExecuteDataRow(sql, parList, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能：对传入的SQL执行查询操作,返回单个值     
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的数据库</param>
        /// <param name="parList">参数</param>
        /// <param name="dbT">执行库</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, List<ParameterEntity> parList, DataBaseType dbT)
        {
            try
            {
                OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
                DbCommand cmd = oraDb.GetSqlStringCommand(sql);
                if (parList != null && parList.Count > 0)
                {
                    foreach (ParameterEntity par in parList)
                    {
                        oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                    }
                }
                object obj = oraDb.ExecuteScalar(cmd);
                if (Convert.IsDBNull(obj))
                {
                    return null;
                }
                return obj;
            }
            catch (Exception e)
            {
                //return null;
                throw e;
            }
        }


        /// <summary>
        /// 功能：对传入的SQL执行查询操作,返回单个值     
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的数据库</param>
        /// <param name="parList">参数</param>      
        /// <returns></returns>
        public static object ExecuteScalar(string sql, List<ParameterEntity> parList)
        {
            return OracleDBHelper.ExecuteScalar(sql, parList, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能：对传入的SQL执行查询操作,返回单个值     
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的数据库</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {
            return OracleDBHelper.ExecuteScalar(sql, null, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能：根据序列名称查询主键
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="seq_Name">序列名</param>
        /// <param name="dbT">执行库</param>
        /// <returns></returns>
        public static int SelectTBID(string seq_Name, DataBaseType dbT)
        {
            try
            {
                String sql = "SELECT " + seq_Name + ".NEXTVAL FROM DUAL";
                return Convert.ToInt32(ExecuteScalar(sql, null, dbT));
            }
            catch (Exception e)
            {
                //return 0;
                throw e;
            }
        }

        /// <summary>
        /// 功能：根据序列名称查询主键
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="seq_Name">序列名</param>
        /// <param name="dbT">执行库</param>
        /// <returns></returns>
        public static int SelectTBID(string seq_Name)
        {
            try
            {
                String sql = "SELECT " + seq_Name + ".NEXTVAL FROM DUAL";
                return Convert.ToInt32(ExecuteScalar(sql, null, DataBaseType.DefaultDB));
            }
            catch (Exception e)
            {
                //return 0;
                throw e;
            }
        }

        /// <summary>
        /// 功能：执行事物 (返回结果List 0 影响行数  1 执行出现错误的SQL )  
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="dic">SQL列表</param>      
        /// <param name="dbT">库类型</param>
        /// <returns></returns>
        public static List<object> ExecuteDBTransaction(Dictionary<string, List<ParameterEntity>> dic, DataBaseType dbT)
        {
            List<object> obj = new List<object>();
            OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
            int result = 0;
            string errorSQL = "";
            using (DbConnection conn = oraDb.CreateConnection())
            {
                conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                try
                {
                    foreach (string item in dic.Keys)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            errorSQL = item;
                            DbCommand cmd = oraDb.GetSqlStringCommand(item);
                            if (dic[item] != null && dic[item].Count > 0)
                            {
                                foreach (ParameterEntity par in dic[item])
                                {
                                    oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                                }
                            }
                            if (oraDb.ExecuteNonQuery(cmd, tran) >= 0)
                            {
                                result++;
                            }
                            else
                            {
                                tran.Rollback();
                                result = 0;
                                obj.Add(result);
                                obj.Add(errorSQL);
                                return obj;
                            }
                        }
                    }
                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    result = 0;
                    obj.Add(result);
                    obj.Add(errorSQL);
                    return obj;
                }
                obj.Add(result);
                obj.Add(errorSQL);
                return obj;
            }
        }

        /// <summary>
        /// 功能：执行事物 (返回结果List 0 影响行数  1 执行出现错误的SQL )  
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="dic">SQL列表</param>             
        /// <returns></returns>
        public static List<object> ExecuteDBTransaction(Dictionary<string, List<ParameterEntity>> dic)
        {
            return OracleDBHelper.ExecuteDBTransaction(dic, DataBaseType.DefaultDB);
        }

        /// <summary>
        /// 功能：查询分页数据
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sqlStr">查询SQL</param>
        /// <param name="pi">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="parList">参数类型</param>
        /// <param name="dbT">执行库</param>
        /// <returns></returns>
        public static PagedTable GetDataByPage(string sqlStr, int pi, int pageSize, List<ParameterEntity> parList, DataBaseType dbT)
        {
            string falg = DataPageFalg;
            #region 查询数据语句
            //String sqlData = @"SELECT * FROM (SELECT A.*, ROWNUM ROW_NUM FROM (" + sqlStr + ") A) WHERE ROW_NUM > :ROW_NUM_OLD  AND ROW_NUM <= :ROW_NUM_NEW";
            String sqlData = @"SELECT * FROM (SELECT A.*, ROWNUM ROW_NUM FROM (" + sqlStr + ") A WHERE ROWNUM<= :ROW_NUM_NEW) WHERE ROW_NUM > :ROW_NUM_OLD";
            #endregion
            #region 查询数据总条数语句
            String sqlCount = "(SELECT COUNT(1) ROW_TOTAL from (" + sqlStr + ")) ROW_TOTAL ";
            #endregion
            String SQL = "SELECT * FROM " + sqlCount + " ,(" + sqlData + ")";
            OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
            DbCommand cmd = oraDb.GetSqlStringCommand(SQL);
            if (parList != null && parList.Count > 0)
            {
                foreach (ParameterEntity par in parList)
                {
                    oraDb.AddInParameter(cmd, par.Name, par.DbType, par.Value);
                }
            }
            oraDb.AddInParameter(cmd, ":ROW_NUM_OLD", DbType.Int32, (pi - 1) * pageSize);
            oraDb.AddInParameter(cmd, ":ROW_NUM_NEW", DbType.Int32, pageSize * pi);
           DataSet ds = oraDb.ExecuteDataSet(cmd);
            PagedTable pt = new PagedTable(ds.Tables[0], pi, pageSize, BusinessHelper.GetRowTotal(ds.Tables[0]));
            return pt;
        }

        /// <summary>
        /// 功能：查询分页数据
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sqlStr">查询SQL</param>
        /// <param name="pi">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="parList">参数类型</param>        
        /// <returns></returns>
        public static PagedTable GetDataByPage(string sqlStr, int pi, int pageSize, List<ParameterEntity> parList)
        {
            return OracleDBHelper.GetDataByPage(sqlStr, pi, pageSize, parList, DataBaseType.DefaultDB);
        }
        public static readonly string DataPageFalg = "/*SELECT COLUMN*/";

        /// <summary>
        /// 功能：动态追加排序字段
        /// 日期：2014-1-23
        /// </summary>
        /// <param name="sqlStr">查询数据SQL，必须明确定义要查询的列</param>
        /// <param name="listPrefix">查询数据中SQL中所使用的前缀，以“A.”型式</param>
        /// <param name="SIDX">排序字段</param>
        /// <param name="Sord">排序方式</param>
        /// <returns></returns>
        public static string AddOrderBy(string sqlStr, List<string> listPrefix, string SIDX, string Sord)
        {
            if (listPrefix == null)
            {
                return sqlStr;
            }
            foreach (string s in listPrefix)
            {
                if (!string.IsNullOrEmpty(SIDX))
                {
                    if (sqlStr.ToUpper().IndexOf(s + SIDX.ToUpper()) != -1)
                    {
                        sqlStr += " ORDER BY " + s + SIDX + " " + Sord;
                        break;
                    }
                    else if (SIDX.IndexOf(".") != -1 && sqlStr.ToUpper().IndexOf(SIDX.ToUpper()) != -1)
                    {
                        sqlStr += " ORDER BY " + SIDX + " " + Sord;
                        break;
                    }
                }
            }
            return sqlStr;
        }

        /// <summary>
        /// 功能：动态追加排序字段
        /// 日期：2014-1-23
        /// </summary>
        /// <param name="sqlStr">查询数据SQL，必须明确定义要查询的列</param>
        /// <param name="listPrefix">查询数据中SQL中所使用的前缀，以“A.”型式</param>
        /// <param name="SIDX">排序字段</param>
        /// <param name="Sord">排序方式</param>
        /// <returns></returns>
        public static string GetOrderBy(string sqlStr, List<string> listPrefix, string SIDX, string Sord)
        {
            if (listPrefix == null || string.IsNullOrEmpty(sqlStr) || listPrefix.Count == 0)
            {
                return "";
            }
            foreach (string s in listPrefix)
            {
                if (!string.IsNullOrEmpty(SIDX))
                {
                    if (sqlStr.ToUpper().IndexOf(s + SIDX.ToUpper()) != -1)
                    {
                        return " ORDER BY " + s + SIDX + " " + Sord;
                    }
                    else if (SIDX.IndexOf(".") != -1 && sqlStr.ToUpper().IndexOf(SIDX.ToUpper()) != -1)
                    {
                        return " ORDER BY " + SIDX + " " + Sord;
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// 更新大文本字符串保存到数据库
        /// 王方圆添加 2017-5-16
        /// </summary>
        /// <param name="tableName">表明</param>
        /// <param name="id">唯一标识ID</param>
        /// <param name="longTextFieldName">字段名称</param>
        /// <param name="longTextContent">更新文本内容</param>
        public static  void UpdateLongText(string tableName, long id, string longTextFieldName, string longTextContent)
        {
            string connectStr =System.Configuration.ConfigurationManager.ConnectionStrings["DefaultDB"].ConnectionString;
            using (Oracle.DataAccess.Client.OracleConnection conn = new Oracle.DataAccess.Client.OracleConnection(connectStr))
            {
                conn.Open();
                //OracleTransaction trans = conn.BeginTransaction();
                Oracle.DataAccess.Client.OracleCommand cmd = conn.CreateCommand();
                //cmd.Transaction = trans;
                cmd.CommandText = "declare xx clob; begin dbms_lob.createtemporary(xx, false, 0); :templob := xx; end;";
                cmd.Parameters.Add(new Oracle.DataAccess.Client.OracleParameter(":templob", Oracle.DataAccess.Client.OracleDbType.Clob)).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                OracleClob tmplob = (OracleClob)cmd.Parameters[0].Value;
                byte[] buffer = System.Text.Encoding.Unicode.GetBytes(longTextContent);
                tmplob.Write(buffer, 0, buffer.Length);
                tmplob.Position = 0;

                cmd.Parameters.Clear();
                string cmdText = "update {0} set {1} = :lob where EMAIL_ID= :id";
                cmdText = string.Format(cmdText, tableName, longTextFieldName);
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new Oracle.DataAccess.Client.OracleParameter(":lob", Oracle.DataAccess.Client.OracleDbType.Clob)).Value = tmplob;
                cmd.Parameters.Add(new Oracle.DataAccess.Client.OracleParameter(":id", id));
                cmd.ExecuteNonQuery();
            }
        }
    }
}
