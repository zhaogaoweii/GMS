using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text;
using System.Data;
using System.Configuration;

public partial class OrderList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null || Session["UserName"].ToString().Length <= 0)
        {
            Response.Redirect("Login.aspx");
        }
        LitDate.Text = DateTime.Now.ToShortDateString() + "    " + "星期" + DateTime.Now.DayOfWeek.ToString(("D"));

        string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_excelorder.mdb";
        string sql = "";

        #region 删除
        if ("del" == Request.QueryString["op"])
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Length > 0)
            {
                int id = int.Parse(Request.QueryString["ID"]);
                sql = "delete from OrderMaster where ID=" + id;
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    try
                    {
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            sql = "delete from OrderDetail where OrderID=" + id;
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            Literal2.Text = "<scripr>alert('删除成功！');<script>";
                        }
                        else
                        {
                            Literal2.Text = "<scripr>alert('删除失败！');<script>";
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {

                        Literal2.Text = ex.Message;
                    }
                }

            }

        }
        #endregion

        #region 显示列表
        sql = "select * from OrderMaster order by id desc";
        DataSet ds = new DataSet();
        using (OleDbConnection connection = new OleDbConnection(connStr))
        {
            try
            {
                connection.Open();
                OleDbDataAdapter command = new OleDbDataAdapter(sql, connection);
                command.Fill(ds, "ds");
                connection.Close();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                Literal2.Text = ex.Message;
            }
        }

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder strHtmls = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strHtmls.Append("<tr>\n");
                strHtmls.Append("<td>" + dt.Rows[i]["OrderNum"] + "</td>");
                if (dt.Rows[i]["OrderDate"] != null && dt.Rows[i]["OrderDate"].ToString().Trim().Length > 0)
                {
                    strHtmls.Append("<td>" + DateTime.Parse(dt.Rows[i]["OrderDate"].ToString()).ToShortDateString() + "</td>\n");
                }
                else
                {
                    strHtmls.Append("<td>&nbsp;</td>\n");

                }
                strHtmls.Append("<td>" + dt.Rows[i]["CustName"] + "</td>\n");
                strHtmls.Append("<td>" + dt.Rows[i]["SalesName"] + "</td>\n");
                if (dt.Rows[i]["Amount"] != null && dt.Rows[i]["Amount"].ToString().Trim().Length > 0)
                {
                    strHtmls.Append(" <td style='text-align:right;padding-right:5px;'>" + string.Format("{0:C}", dt.Rows[i]["Amount"]) + "</td>\n");
                }
                else
                {
                    strHtmls.Append(" <td>&nbsp;</td>\n");
                }
                strHtmls.Append("<td>\n");
                strHtmls.Append("<div class='ul-page'>\n");
                strHtmls.Append("<a href='OpenOrder.aspx?ID=" + dt.Rows[i]["ID"] + "'>修改</a>|<a href='ViewOrder.aspx?ID=" + dt.Rows[i]["ID"] + "'>只读查看^打印</a>|<a  onclick='Delete(" + dt.Rows[i]["ID"] + ")' >删除</a>\n");
                strHtmls.Append("</div>\n");
                strHtmls.Append("</td>\n");
                strHtmls.Append("</tr>\n");
            }

            Literal1.Text = strHtmls.ToString();
        }
        #endregion


    }
}
