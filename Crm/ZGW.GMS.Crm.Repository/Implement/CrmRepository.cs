using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZGW.GMS.Core;
using ZGW.GMS.Core.Data.ADO;
using ZGW.GMS.Crm.BusinessEntity;


namespace ZGW.GMS.Crm.Repository.Implement
{
    [ComponentRegistry]
    public class CrmRepository : ICrmRepository
    {
        public CrmRepository() { }
        #region

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddPersonuser(ElectricityUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_OrdinaryUsers(");
            strSql.Append("Name,LinkPhone,ElectricityConsumption,OperatorID,OperatorTime,LastOperatorID,LastOperateTime,IsDel)");
            strSql.Append(" values (");
            strSql.Append("@Name,@LinkPhone,@ElectricityConsumption,@OperatorID,@OperatorTime,@LastOperatorID,@LastOperateTime,@IsDel)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@LinkPhone", SqlDbType.VarChar,50),
					new SqlParameter("@ElectricityConsumption", SqlDbType.Float,8),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorTime", SqlDbType.DateTime),
					new SqlParameter("@LastOperatorID", SqlDbType.Int,4),
					new SqlParameter("@LastOperateTime", SqlDbType.DateTime),
					new SqlParameter("@IsDel", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.linkPhone;
            parameters[2].Value = model.electricityConsumption;
            parameters[3].Value = model.operatorID;
            parameters[4].Value = model.operatorTime;
            parameters[5].Value = model.lastOperatorID;
            parameters[6].Value = model.lastOperateTime;
            parameters[7].Value = model.isDel;

            object obj = SQLHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdatePersonuser(ElectricityUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_OrdinaryUsers set ");
            strSql.Append("Name=@Name,");
            strSql.Append("LinkPhone=@LinkPhone,");
            strSql.Append("ElectricityConsumption=@ElectricityConsumption,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorTime=@OperatorTime,");
            strSql.Append("LastOperatorID=@LastOperatorID,");
            strSql.Append("LastOperateTime=@LastOperateTime,");
            strSql.Append("IsDel=@IsDel");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@LinkPhone", SqlDbType.VarChar,50),
					new SqlParameter("@ElectricityConsumption", SqlDbType.Float,8),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorTime", SqlDbType.DateTime),
					new SqlParameter("@LastOperatorID", SqlDbType.Int,4),
					new SqlParameter("@LastOperateTime", SqlDbType.DateTime),
					new SqlParameter("@IsDel", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.linkPhone;
            parameters[2].Value = model.electricityConsumption;
            parameters[3].Value = model.operatorID;
            parameters[4].Value = model.operatorTime;
            parameters[5].Value = model.lastOperatorID;
            parameters[6].Value = model.lastOperateTime;
            parameters[7].Value = model.isDel;
            parameters[8].Value = model.ID;

            int rows = SQLHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DelPersonuser(string ids)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_OrdinaryUsers ");
            strSql.Append(" where ID in (" + ids + ")  ");
            int rows = SQLHelper.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ElectricityUser GetPersonmodel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Name,LinkPhone,ElectricityConsumption,OperatorID,OperatorTime,LastOperatorID,LastOperateTime,IsDel from tb_OrdinaryUsers ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            ElectricityUser model = new ElectricityUser();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModelE(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ElectricityUser DataRowToModelE(DataRow row)
        {
            ElectricityUser model = new ElectricityUser();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.name = row["Name"].ToString();
                }
                if (row["LinkPhone"] != null)
                {
                    model.linkPhone = row["LinkPhone"].ToString();
                }
                if (row["ElectricityConsumption"] != null && row["ElectricityConsumption"].ToString() != "")
                {
                    model.electricityConsumption = row["ElectricityConsumption"].ToString();
                }
                if (row["OperatorID"] != null && row["OperatorID"].ToString() != "")
                {
                    model.operatorID = int.Parse(row["OperatorID"].ToString());
                }
                if (row["OperatorTime"] != null && row["OperatorTime"].ToString() != "")
                {
                    model.operatorTime = DateTime.Parse(row["OperatorTime"].ToString());
                }
                if (row["LastOperatorID"] != null && row["LastOperatorID"].ToString() != "")
                {
                    model.lastOperatorID = int.Parse(row["LastOperatorID"].ToString());
                }
                if (row["LastOperateTime"] != null && row["LastOperateTime"].ToString() != "")
                {
                    model.lastOperateTime = DateTime.Parse(row["LastOperateTime"].ToString());
                }
                if (row["IsDel"] != null && row["IsDel"].ToString() != "")
                {
                    model.isDel = int.Parse(row["IsDel"].ToString());
                }
            }
            return model;
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageE(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from tb_OrdinaryUsers T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            if (!isAll)
            {
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            }
            return SQLHelper.Query(strSql.ToString());
        }
        /// <summary>
        /// 一般客户
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<ElectricityUser> GetListElectricityUser(string strWhere, string orderby, int startIndex, int endIndex,bool isAll=true)
        {
            DataTable dt = GetListByPageE(strWhere, orderby, startIndex, endIndex,isAll).Tables[0];
            List<ElectricityUser> list = new List<ElectricityUser>();
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ElectricityUser model = DataRowToModelE(dr);
                    list.Add(model);

                }
            }
            return list;
        }
        #endregion
        #region 公司用电用户
        /// <summary>
        /// 新增公司客户
        /// </summary>
        public int AddCompany(Company model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Company(");
            strSql.Append("Name,LegalPerson,OfficeAddress,RegisterCode,LinkPhone,ElectricityConsumption,OperatorID,OperatorTime,LastOperatorID,LastOperateTime,IsDel)");
            strSql.Append(" values (");
            strSql.Append("@Name,@LegalPerson,@OfficeAddress,@RegisterCode,@LinkPhone,@ElectricityConsumption,@OperatorID,@OperatorTime,@LastOperatorID,@LastOperateTime,@IsDel)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@LegalPerson", SqlDbType.VarChar,50),
					new SqlParameter("@OfficeAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@RegisterCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LinkPhone", SqlDbType.VarChar,50),
					new SqlParameter("@ElectricityConsumption", SqlDbType.Float,8),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorTime", SqlDbType.DateTime),
					new SqlParameter("@LastOperatorID", SqlDbType.Int,4),
					new SqlParameter("@LastOperateTime", SqlDbType.DateTime),
					new SqlParameter("@IsDel", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.legalPerson;
            parameters[2].Value = model.officeAddress;
            parameters[3].Value = model.registerCode;
            parameters[4].Value = model.linkPhone;
            parameters[5].Value = model.electricityConsumption;
            parameters[6].Value = model.operatorID;
            parameters[7].Value = model.operatorTime != null ? model.operatorTime : DateTime.Now;
            parameters[8].Value = model.lastOperatorID;
            parameters[9].Value = model.lastOperateTime;
            parameters[10].Value = 0;

            object obj = SQLHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateCompany(Company model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Company set ");
            strSql.Append("Name=@Name,");
            strSql.Append("LegalPerson=@LegalPerson,");
            strSql.Append("OfficeAddress=@OfficeAddress,");
            strSql.Append("RegisterCode=@RegisterCode,");
            strSql.Append("LinkPhone=@LinkPhone,");
            strSql.Append("ElectricityConsumption=@ElectricityConsumption,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorTime=@OperatorTime,");
            strSql.Append("LastOperatorID=@LastOperatorID,");
            strSql.Append("LastOperateTime=@LastOperateTime,");
            strSql.Append("IsDel=@IsDel");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@LegalPerson", SqlDbType.VarChar,50),
					new SqlParameter("@OfficeAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@RegisterCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LinkPhone", SqlDbType.VarChar,50),
					new SqlParameter("@ElectricityConsumption", SqlDbType.Float,8),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorTime", SqlDbType.DateTime),
					new SqlParameter("@LastOperatorID", SqlDbType.Int,4),
					new SqlParameter("@LastOperateTime", SqlDbType.DateTime),
					new SqlParameter("@IsDel", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.legalPerson;
            parameters[2].Value = model.officeAddress;
            parameters[3].Value = model.registerCode;
            parameters[4].Value = model.linkPhone;
            parameters[5].Value = model.electricityConsumption;
            parameters[6].Value = model.operatorID;
            parameters[7].Value = model.operatorTime;
            parameters[8].Value = model.lastOperatorID;
            parameters[9].Value = model.lastOperateTime;
            parameters[10].Value = model.isDel;
            parameters[11].Value = model.ID;

            int rows = SQLHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除公司用户
        /// </summary>
        /// <returns></returns>
        public bool DelCompany(string ids)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Company ");
            strSql.Append(" where ID in (" + ids + ")  ");
            int rows = SQLHelper.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Company GetCompanymodel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Name,LegalPerson,OfficeAddress,RegisterCode,LinkPhone,ElectricityConsumption,OperatorID,OperatorTime,LastOperatorID,LastOperateTime,IsDel from tb_Company ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Company model = new Company();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Company DataRowToModel(DataRow row)
        {
            Company model = new Company();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.name = row["Name"].ToString();
                }
                if (row["LegalPerson"] != null)
                {
                    model.legalPerson = row["LegalPerson"].ToString();
                }
                if (row["OfficeAddress"] != null)
                {
                    model.officeAddress = row["OfficeAddress"].ToString();
                }
                if (row["RegisterCode"] != null)
                {
                    model.registerCode = row["RegisterCode"].ToString();
                }
                if (row["LinkPhone"] != null)
                {
                    model.linkPhone = row["LinkPhone"].ToString();
                }
                if (row["ElectricityConsumption"] != null && row["ElectricityConsumption"].ToString() != "")
                {
                    model.electricityConsumption = row["ElectricityConsumption"].ToString();
                }
                if (row["OperatorID"] != null && row["OperatorID"].ToString() != "")
                {
                    model.operatorID = int.Parse(row["OperatorID"].ToString());
                }
                if (row["OperatorTime"] != null && row["OperatorTime"].ToString() != "")
                {
                    model.operatorTime = DateTime.Parse(row["OperatorTime"].ToString());
                }
                if (row["LastOperatorID"] != null && row["LastOperatorID"].ToString() != "")
                {
                    model.lastOperatorID = int.Parse(row["LastOperatorID"].ToString());
                }
                if (row["LastOperateTime"] != null && row["LastOperateTime"].ToString() != "")
                {
                    model.lastOperateTime = DateTime.Parse(row["LastOperateTime"].ToString());
                }
                if (row["IsDel"] != null && row["IsDel"].ToString() != "")
                {
                    model.isDel = int.Parse(row["IsDel"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获取公司客户数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="isAll">true获取全部；false分页获取</param>
        /// <returns></returns>
        public DataSet GetListCompanyByPage(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from tb_Company T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            if (!isAll)
            {
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            }
            return SQLHelper.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取公司分页数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="isAll">true:获取全部数据；false:分页获取数据</param>
        /// <returns></returns>
        public List<Company> GetListCompany(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true)
        {
            DataTable dt = GetListCompanyByPage(strWhere, orderby, startIndex, endIndex, isAll).Tables[0];
            List<Company> list = new List<Company>();
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Company model = DataRowToModel(dr);
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion
    }
}
