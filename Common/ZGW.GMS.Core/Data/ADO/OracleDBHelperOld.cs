using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Oracle.DataAccess.Client;
using System.Text;

namespace CIIC.OMS.Core.Data
{
    /// <summary>
    /// 数据访问基础类(基于Oracle)
    /// 作者：单龙
    /// 创建时间：2012-02-09
    /// 修改记录：
    ///     (一).
    ///     修改人:刘彦鑫
    ///     修改时间：2012-03-05
    ///     修改内容：
    ///         (1).添加函数ExecuteProcedure(procedureName, commandParameters)
    ///         (2).添加函数PrepareCommand()
    ///     (二).
    ///     修改人：刘彦鑫
    ///     修改时间：2012-03-16
    ///     修改内容：
    ///         (1).添加方法“ExecuteScalar(,)”
    /// </summary>
    public static class OracleDBHelperOld
    {
        #region 公用方法

        //返回数据库连接
        public static OracleConnection GetConn(DBCatetory category)
        {
            try
            {
                OracleConnection objConn = new OracleConnection();
                objConn.ConnectionString = GetConnStr(category);
                return objConn;
            }
            catch (Exception e)
            {
               throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据表名和字段名获取最大ID值
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="TableName">表名</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns></returns>
        public static int GetMaxID(string FieldName, string TableName, DBCatetory dbCategory = DBCatetory.Production)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql, dbCategory);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 判断数据库是否存在某条记录
        /// </summary>
        /// <param name="strSql">sql</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns></returns>
        public static bool Exists(string strSql, DBCatetory dbCategory = DBCatetory.Production)
        {
            object obj = GetSingle(strSql, dbCategory);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
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
        /// 判断数据库是否存在某条记录
        /// </summary>
        /// <param name="strSql">sql</param>
        /// <param name="cmdParms">参数数组</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns></returns>
        public static bool Exists(string strSql, OracleParameter[] cmdParms, DBCatetory dbCategory = DBCatetory.Production)
        {
            object obj = GetSingle(strSql, cmdParms, dbCategory);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
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
        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        cmd.Dispose();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        /// <param name="dbCategory">数据库分类</param>
        public static void ExecuteSqlTran(ArrayList SQLStringList, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                connection.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connection;
                OracleTransaction tx = connection.BeginTransaction();
                //cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                OracleCommand cmd = new OracleCommand(SQLString, connection);
                OracleParameter myParameter = new OracleParameter("@content", OracleDbType.NVarchar2);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                OracleCommand cmd = new OracleCommand(strSQL, connection);
                OracleParameter myParameter = new OracleParameter("@fs", OracleDbType.LongRaw);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        cmd.Dispose();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader ExecuteReader(string strSQL, DBCatetory dbCategory = DBCatetory.Production)
        {
            OracleConnection connection = GetConn(dbCategory);
            OracleCommand cmd = new OracleCommand(strSQL, connection);
            try
            {
                connection.Open();
                OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Dispose();
                //if (connection.State == ConnectionState.Open)
                //    connection.Close(); // Commented by Yanxinliu@Feb 17th 2012
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>DataTable</returns>
        public static DataTable QueryTable(string SQLString, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {

                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                    if (ds.Tables == null || ds.Tables.Count < 1)
                    {
                        return null;
                    }
                    else
                    {
                        return ds.Tables[0];
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }

            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {

                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }

            }
        }

        public static DataSet QueryT(DBCatetory dbCategory, string SQLString, string vTName)
        {
            if (SQLString != null && SQLString.Trim() != "")
            {
                using (OracleConnection connection = GetConn(dbCategory))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        connection.Open();
                        OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                        command.Fill(ds, vTName);
                        return ds;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 执行sql语句，返回首记录的第一列，根据数据库连接
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="commandParameters">参数</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns></returns>
        public static object ExecuteScalar(string commandText, OracleParameter[] commandParameters, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, connection, CommandType.Text, commandText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val ?? "";
            }
        }

        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, OracleParameter[] cmdParms, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的OracleParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection conn = GetConn(dbCategory))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    OracleCommand cmd = new OracleCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            OracleParameter[] cmdParms = (OracleParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                           
                           
                        }
                        trans.Commit();
                    }
                    catch(Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, OracleParameter[] cmdParms, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        cmd.Dispose();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader ExecuteReader(string SQLString, OracleParameter[] cmdParms, DBCatetory dbCategory = DBCatetory.Production)
        {
            OracleConnection connection = GetConn(dbCategory);
            OracleCommand cmd = new OracleCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                //if (connection.State == ConnectionState.Open)
                //    connection.Close(); // Commented by Yanxinliu@March 16th 2012
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, OracleParameter[] cmdParms, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        cmd.Dispose();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }


        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                //cmd.Transaction = trans;
                cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程 返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader RunProcedure(string storedProcName, IDataParameter[] parameters, DBCatetory dbCategory = DBCatetory.Production)
        {
            OracleConnection connection = GetConn(dbCategory);
            OracleDataReader returnReader;
            connection.Open();
            OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                OracleDataAdapter sqlDA = new OracleDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }


        /// <summary>
        /// 构建 OracleCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OracleCommand</returns>
        private static OracleCommand BuildQueryCommand(OracleConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OracleCommand command = new OracleCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                int result;
                connection.Open();
                OracleCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 OracleCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OracleCommand 对象实例</returns>
        private static OracleCommand BuildIntCommand(OracleConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new OracleParameter("ReturnValue",
                OracleDbType.Int32, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        #region

        /// <summary>
        /// 数据库执行语句准备
        /// </summary>
        /// <param name="cmd">Sql语句命令</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="cmdType">Sql语句命令类型</param>
        /// <param name="cmdText">Sql语句串</param>
        /// <param name="commandParameters">Sql语句参数数组</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
        {

            //根据需要打开数据库链接
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //创建数据库执行命令
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //根据需要，绑定事务处理
            //if (trans != null)
            //    cmd.Transaction = trans;

            // 绑定参数
            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="commandParameters">存储过程参数</param>
        /// <returns></returns>
        public static int ExecuteProcedure(string procedureName, OracleParameter[] commandParameters, DBCatetory dbCategory = DBCatetory.Production)
        {
            using (OracleConnection connection = GetConn(dbCategory))
            {
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, connection, CommandType.StoredProcedure, procedureName, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }


        #endregion

        #endregion

        #region 由Object取值
        /// <summary>
        /// 取得Int值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetInt(object obj)
        {
            return obj != null ? obj.ToString().ToInt() : 0;
        }

        /// <summary>
        /// 获得int32值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int32 GetInt32(object obj)
        {
            return obj != null ? obj.ToString().ToInt() : 0;
        }

        /// <summary>
        /// 获得int64值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long GetInt64(object obj)
        {
            return obj != null ? obj.ToString().ToInt64() : 0;
        }

        /// <summary>
        /// 取得Decimal值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal GetDecimal(object obj)
        {
            decimal quantity = 0.0m;
            try
            {
                quantity = decimal.Parse(obj.ToString().Trim());
            }
            catch
            {
                quantity = 0.0m;
            }
            return quantity;
        }

        /// <summary>
        /// 取得Guid值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid GetGuid(object obj)
        {
            if (obj.ToString() != "")
                return new Guid(obj.ToString());
            else
                return Guid.Empty;
        }

        /// <summary>
        /// 取得Guid值
        /// </summary>
        public static string GetPimaryKey()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 取得DateTime值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object obj)
        {
            if (obj.ToString() != "")
                return DateTime.Parse(obj.ToString());
            else
                return DateTime.MinValue;
        }

        /// <summary>
        /// 取得bool值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetBool(object obj)
        {
            if (obj.ToString() == "1" || obj.ToString().ToLower() == "true")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 取得byte[]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Byte[] GetByte(object obj)
        {
            if (obj.ToString() != "")
            {
                return (Byte[])obj;
            }
            else
                return null;
        }

        /// <summary>
        /// 取得string值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetString(object obj)
        {
            return obj.ToString();
        }
        #endregion

        #region 序列化与反序列化
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>返回二进制</returns>
        public static byte[] SerializeModel(Object obj)
        {
            if (obj != null)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                byte[] b;
                binaryFormatter.Serialize(ms, obj);
                ms.Position = 0;
                b = new Byte[ms.Length];
                ms.Read(b, 0, b.Length);
                ms.Close();
                return b;
            }
            else
                return new byte[0];
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="b">要反序列化的二进制</param>
        /// <returns>返回对象</returns>
        public static object DeserializeModel(byte[] b, object SampleModel)
        {
            if (b == null || b.Length == 0)
                return SampleModel;
            else
            {
                object result = new object();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                try
                {
                    ms.Write(b, 0, b.Length);
                    ms.Position = 0;
                    result = binaryFormatter.Deserialize(ms);
                    ms.Close();
                }
                catch { }
                return result;
            }
        }
        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqlQuery">DataTable的查询语句</param>
        /// <param name="sqlCount">总记录数据查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="pageIndex">起始页,从1开始</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns></returns>
        public static PagedTable QueryPagedTable(string sqlQuery,string sqlCount ,OracleParameter[] parameters,int pageIndex, int pageSize, DBCatetory dbCategory = DBCatetory.Production)
        {
            int startIndex=(pageIndex-1)*pageSize+1;
            int endIndex=pageIndex*pageSize+1;
            
            string sqlPagedQuery =String.Format("SELECT * FROM ( SELECT A.*, ROWNUM RN FROM ({0}) A WHERE ROWNUM < {1} ) WHERE RN >= {2}",sqlQuery,endIndex,startIndex);

            long count = Convert.ToInt64(ExecuteScalar(sqlCount, parameters, dbCategory));
            DataTable table = Query(sqlPagedQuery, parameters, dbCategory).Tables[0];
            PagedTable pagedResult=new PagedTable(table, pageIndex, pageSize, count);
            return pagedResult;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqlQuery">DataTable的查询语句</param>
        /// <param name="sqlCount">总记录数据查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="pageIndex">起始页,从1开始</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="dbCategory">数据库分类</param>
        /// <returns></returns>
        public static PagedTable QueryPagedTable(string sqlQuery,string sqlCount,int pageIndex=1, int pageSize=int.MaxValue, DBCatetory dbCategory = DBCatetory.Production)
        {
            return QueryPagedTable(sqlQuery, sqlCount, null, pageIndex, pageSize, dbCategory);
        }

        #endregion

        #region Model与XML互相转换
        /// <summary>
        /// Model转化为XML的方法
        /// </summary>
        /// <param name="model">要转化的Model</param>
        /// <returns></returns>
        public static string ModelToXML(object model)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlElement ModelNode = xmldoc.CreateElement("Model");
            xmldoc.AppendChild(ModelNode);

            if (model != null)
            {
                foreach (PropertyInfo property in model.GetType().GetProperties())
                {
                    XmlElement attribute = xmldoc.CreateElement(property.Name);
                    if (property.GetValue(model, null) != null)
                        attribute.InnerText = property.GetValue(model, null).ToString();
                    else
                        attribute.InnerText = "[Null]";
                    ModelNode.AppendChild(attribute);
                }
            }

            return xmldoc.OuterXml;
        }

        /// <summary>
        /// XML转化为Model的方法
        /// </summary>
        /// <param name="xml">要转化的XML</param>
        /// <param name="SampleModel">Model的实体示例，New一个出来即可</param>
        /// <returns></returns>
        public static object XMLToModel(string xml, object SampleModel)
        {
            if (string.IsNullOrEmpty(xml))
                return SampleModel;
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml);

                XmlNodeList attributes = xmldoc.SelectSingleNode("Model").ChildNodes;
                foreach (XmlNode node in attributes)
                {
                    foreach (PropertyInfo property in SampleModel.GetType().GetProperties())
                    {
                        if (node.Name == property.Name)
                        {
                            if (node.InnerText != "[Null]")
                            {
                                if (property.PropertyType == typeof(System.Guid))
                                    property.SetValue(SampleModel, new Guid(node.InnerText), null);
                                else
                                    property.SetValue(SampleModel, Convert.ChangeType(node.InnerText, property.PropertyType), null);
                            }
                            else
                                property.SetValue(SampleModel, null, null);
                        }
                    }
                }
                return SampleModel;
            }
        }
        #endregion

        /// <summary>
        /// 根据数据库类别获取ConnectionStrings中的连接名称
        /// </summary>
        /// <returns></returns>
        public static string GetConnName(DBCatetory category)
        {
            string result = "";

            switch (category)
            {
                case DBCatetory.Production:
                    result = "DefaultDB";
                    break;
                case DBCatetory.History:
                    result = "HistoryDB";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 根据数据库类别获取ConnectionStrings中的数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConnStr(DBCatetory category)
        {
            var connName = GetConnName(category);
            return ConfigureHelper.GetConnStr(connName);
        }
    }
}
