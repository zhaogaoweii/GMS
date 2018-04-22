using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core;
using ZGW.GMS.Core.Data.ADO;
using ZGW.GMS.Platform.BusinessEntity;

namespace ZGW.GMS.Platform.Repository.Implement
{
    [ComponentRegistry]
    public class MembershipRepository : IMembershipRepository
    {
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return SQLHelper.GetMaxID("ID", "tb_User");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ValidationStatus Login(string userName, string password)
        {
            if (GetRecordCount(string.Format(" LoginName='{0}' and  Password='{1}'", userName, password)) > 0)
            {
                return ValidationStatus.Authenticated;
            }
            return ValidationStatus.WrongPassword;

        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_User");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_User(");
            strSql.Append("LoginName,Password,CreateTime,IsActive,Email,Mobile)");
            strSql.Append(" values (");
            strSql.Append("@LoginName,@Password,@CreateTime,@IsActive,@Email,@Mobile)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LoginName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@IsActive", SqlDbType.Bit,1),
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.LoginName;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.IsActive;
            parameters[4].Value = model.Email;
            parameters[5].Value = model.Mobile;

            object obj = SQLHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                if (model.RoleIds.Count != 0)
                {
                    foreach (var item in model.RoleIds)
                    {
                        UserRole ur = new UserRole();
                        ur.RoleID = item;
                        ur.UserID = Convert.ToInt32(obj);
                        ur.CreateTime = DateTime.Now;
                        AddUserRole(ur);
                    }
                }
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_User set ");
            strSql.Append("LoginName=@LoginName,");
            strSql.Append("Password=@Password,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("IsActive=@IsActive,");
            strSql.Append("Email=@Email,");
            strSql.Append("Mobile=@Mobile");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@LoginName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@IsActive", SqlDbType.Bit,1),
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.LoginName;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.IsActive;
            parameters[4].Value = model.Email;
            parameters[5].Value = model.Mobile;
            parameters[6].Value = model.ID;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_User ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_User ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public User GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,LoginName,Password,CreateTime,IsActive,Email,Mobile from tb_User ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            User model = new User();
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
        public User DataRowToModel(DataRow row)
        {
            User model = new User();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["LoginName"] != null)
                {
                    model.LoginName = row["LoginName"].ToString();
                }
                if (row["Password"] != null)
                {
                    model.Password = row["Password"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["IsActive"] != null && row["IsActive"].ToString() != "")
                {
                    if ((row["IsActive"].ToString() == "1") || (row["IsActive"].ToString().ToLower() == "true"))
                    {
                        model.IsActive = true;
                    }
                    else
                    {
                        model.IsActive = false;
                    }
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["Mobile"] != null)
                {
                    model.Mobile = row["Mobile"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,LoginName,Password,CreateTime,IsActive,Email,Mobile ");
            strSql.Append(" FROM tb_User ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SQLHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,LoginName,Password,CreateTime,IsActive,Email,Mobile ");
            strSql.Append(" FROM tb_User ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SQLHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tb_User ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = SQLHelper.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true)
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
            strSql.Append(")AS Row, T.*  from tb_User T ");
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
        /// 分页获取数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<User> GetListuser(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true)
        {
            DataTable dt = GetListByPage(strWhere, orderby, startIndex, endIndex, isAll).Tables[0];
            List<User> list = new List<User>();
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    User model = DataRowToModel(dr);
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public User GetModelByStrWhere(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,LoginName,Password,CreateTime,IsActive,Email,Mobile from tb_User ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);
            User model = new User();
            DataSet ds = SQLHelper.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }

        }
        #endregion  BasicMethod


        #region


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddRole(Role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Role(");
            strSql.Append("Name,CreateTime,Info,BusinessPermissionString)");
            strSql.Append(" values (");
            strSql.Append("@Name,@CreateTime,@Info,@BusinessPermissionString)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Info", SqlDbType.NVarChar,300),
					new SqlParameter("@BusinessPermissionString", SqlDbType.NVarChar,4000)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.Info;
            parameters[3].Value = model.BusinessPermissionString;

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
        public bool UpdateRole(Role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Role set ");
            strSql.Append("Name=@Name,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("Info=@Info,");
            strSql.Append("BusinessPermissionString=@BusinessPermissionString");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Info", SqlDbType.NVarChar,300),
					new SqlParameter("@BusinessPermissionString", SqlDbType.NVarChar,4000),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.Info;
            parameters[3].Value = model.BusinessPermissionString;
            parameters[4].Value = model.ID;

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
        /// 删除一条数据
        /// </summary>
        public bool DeleteRole(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Role ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        public bool DeleteListRole(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Role ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public Role GetModelRole(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Name,CreateTime,Info,BusinessPermissionString from tb_Role ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Role model = new Role();
            DataSet ds = SQLHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModelRole(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Role DataRowToModelRole(DataRow row)
        {
            Role model = new Role();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["Info"] != null)
                {
                    model.Info = row["Info"].ToString();
                }
                if (row["BusinessPermissionString"] != null)
                {
                    model.BusinessPermissionString = row["BusinessPermissionString"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageByRole(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = false)
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
            strSql.Append(")AS Row, T.*  from tb_Role T ");
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
        ///角色 分页获取数据
        /// </summary>
        /// <returns></returns>
        public List<Role> GetListByPageToList(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = false)
        {
            DataTable dt = GetListByPageByRole(strWhere, orderby, startIndex, endIndex, isAll).Tables[0];
            List<Role> list = new List<Role>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    Role model = DataRowToModelRole(dr);
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        ///  根据用户id获取角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Role> GetListByUserID(string userId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"
select TR.ID,tr.Name,tr.BusinessPermissionString,tr.CreateTime,tr.Info from tb_UserRole TUR
inner join tb_User TU on TUR.UserID=tu.ID and TU.ID={0}
inner join tb_Role TR on tur.RoleID=tr.ID
", userId);
            DataTable dt = SQLHelper.GetDataTable(sb.ToString());
            List<Role> list = new List<Role>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    Role model = DataRowToModelRole(dr);
                    list.Add(model);
                }
            }
            return list;

        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddUserRole(UserRole model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_UserRole(");
            strSql.Append("UserID,RoleID,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@RoleID,@CreateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleID", SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
                                        };

            parameters[0].Value = model.UserID;
            parameters[1].Value = model.RoleID;
            parameters[2].Value = model.CreateTime;
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
        #endregion
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
