using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System.Data;
using System.Data.Common;

namespace ZGW.GMS.Core.Data
{
    /// <summary>
    /// 数据操作
    /// </summary>
    public class OracleDBHelperTest
    {
        /// <summary>
        /// 功能:对传入的SQL执行增、改、删等操作        
        /// 日期:2013-05-15
        /// </summary>
        /// <param name="sql">要执行的SQl</param>
        /// <param name="parList">参数列表</param>
        /// <param name="dbT">执行的数据库</param>
        /// <returns></returns>    
        public static int ExecuteNonQuery(string sql,List<ParameterEntity> parList,DataBaseType dbT)
        {
            //try
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
            //catch
            //{
            //    return 0;
            //}
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
            //try
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
            //catch
            //{
            //    return 0;
            //}
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
            //try
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
            //catch
            //{
            //    return null;
            //}
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
        /// 功能：对传入的SQL执行查询操作，返回DataTable
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>
        /// <param name="dbT">执行库</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, List<ParameterEntity> parList, DataBaseType dbT)
        {
            //try
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
            //catch
            //{
            //    return null;
            //}
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
        /// 功能：对传入的SQL执行查询操作，返回DataRow    
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <param name="parList">参数列表</param>
        /// <param name="dbT">执行库</param>
        /// <returns></returns>
        public static DataRow ExecuteDataRow(string sql, List<ParameterEntity> parList, DataBaseType dbT)
        {
            //try
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
            //catch 
            //{
            //    return null;
            //}
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
            //try
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
            //catch
            //{
            //    return null;
            //}
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
        /// 功能：根据序列名称查询主键
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="seq_Name">序列名</param>
        /// <param name="dbT">执行库</param>
        /// <returns></returns>
        public static int SelectTBID(string seq_Name, DataBaseType dbT)
        {
            //try
            {
                String sql = "SELECT " + seq_Name + ".NEXTVAL FROM DUAL";
                return Convert.ToInt32(ExecuteScalar(sql,null,dbT));
            }
            //catch 
            //{
            //    return 0;
            //}
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
            //try
            {
                String sql = "SELECT " + seq_Name + ".NEXTVAL FROM DUAL";
                return Convert.ToInt32(ExecuteScalar(sql, null, DataBaseType.DefaultDB));
            }
            //catch
            //{
            //    return 0;
            //}
        }

        /// <summary>
        /// 功能：执行事物 (返回结果List 0 影响行数  1 执行出现错误的SQL )  
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="dic">SQL列表</param>      
        /// <param name="dbT">库类型</param>
        /// <returns></returns>
        public static List<object> ExecuteDBTransaction(Dictionary<string, List<ParameterEntity>> dic,DataBaseType dbT)
        {
            List<object> obj = new List<object>();
            OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
            int result = 0;
            string errorSQL = "";
            using (DbConnection conn = oraDb.CreateConnection())
            {
                DbTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
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
                            if (oraDb.ExecuteNonQuery(cmd,tran) >= 0)
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
                    throw (e);
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
            String sqlData = @"SELECT * FROM (SELECT A.*, ROWNUM ROW_NUM FROM (" + sqlStr + ") A) WHERE ROW_NUM > :ROW_NUM_OLD  AND ROW_NUM <= :ROW_NUM_NEW";
            #endregion
            #region 查询数据总条数语句
            String sqlCount = "(SELECT COUNT(1) ROW_TOTAL " + sqlStr.Substring(sqlStr.IndexOf(falg) + falg.Length) + ") ROW_TOTAL ";
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

        public static DbTransaction BeginTransaction()
        {

            return BeginTransaction( DataBaseType.DefaultDB);
        }

        public static DbTransaction BeginTransaction( DataBaseType dbT)
        {
            try
            {
                OracleDatabase oraDb = DBManager.CreateDataBase(dbT);
                DbConnection conn = oraDb.CreateConnection(); 
                DbTransaction tran = null;
                conn.Open();
                tran = conn.BeginTransaction();
                return tran;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static void Rollback(DbTransaction tran)
        {
            if (tran == null)
                return;
            tran.Rollback();
        }

        public static void Commit(DbTransaction tran)
        {
            if (tran == null)
                return;
            tran.Commit();
        }


        /// <summary>
        /// 功能：执行事物 (返回结果List 0 影响行数  1 执行出现错误的SQL )  
        /// 日期：2013-05-15
        /// </summary>
        /// <param name="dic">SQL列表</param>      
        /// <param name="dbT">库类型</param>
        /// <returns></returns>
        public static List<object> ExecuteDBWithTransaction(Dictionary<string, List<ParameterEntity>> dic,DbTransaction tran)
        {
            List<object> obj = new List<object>();
    
            int result = 0;
            string errorSQL = "";
            
            try
            {
                  
                foreach (string item in dic.Keys)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        DbCommand cmd = tran.Connection.CreateCommand();
                        cmd.CommandText=item;
                        cmd.Transaction=tran; 
                        if (dic[item] != null && dic[item].Count > 0)
                        {
                            foreach (ParameterEntity par in dic[item])
                            {
                                DbParameter param = cmd.CreateParameter();
                                param.ParameterName = par.Name;
                                param.DbType = par.DbType;
                                param.Value = par.Value;
                                cmd.Parameters.Add(param);
                            }
                        }
                    
                        if (cmd.ExecuteNonQuery() >= 0)
                        {
                            result++;
                        }
                        else
                        {
                        
                            result = 0;
                            obj.Add(result);
                            obj.Add(errorSQL);
                            return obj;
                        }
                    }
                }
 
            }
            catch (Exception e)
            {
               
                throw (e);
              
            }
            obj.Add(result);
            obj.Add(errorSQL);
            return obj;
        }
  

    }
}
