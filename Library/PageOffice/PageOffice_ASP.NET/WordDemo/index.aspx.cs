using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Data.OleDb;
using System.Data;

public partial class word_lists : System.Web.UI.Page
{
    protected string strHtmls = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_word.mdb";
        string strSql = "select * from word order by id desc";
        if (!IsPostBack)
        {
            #region 显示列表

            DataSet ds = new DataSet();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbDataAdapter command = new OleDbDataAdapter(strSql, connection);
                command.Fill(ds, "ds");
                connection.Close();
            }    

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                StringBuilder strHtml = new StringBuilder();

                //流转跳转到本页
                bool flg = false;
                string DocID = "";
                if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim().Length > 0)
                {
                    DocID = Request.QueryString["ID"];
                    if (Request.QueryString["flag"] != null && Request.QueryString["flag"].Length > 0)
                    {
                        flg = true;
                    }
                }


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //流转，高亮显示流转操作的文档记录
                    if (dt.Rows[i]["ID"].ToString().Equals(DocID) && flg)
                    {
                        strHtml.Append("<tr style=' background-color:#D7FFEE' onmouseover='onColor(this)' onmouseout='offColor(this)'>\n");
                        strHtml.Append("<td ><img src='images/office-1.jpg' /></td>\n");
                        strHtml.Append("<td >" + dt.Rows[i]["Subject"] + "</td>\n");
                        
                    }
                    //非流转
                    else
                    {
                        strHtml.Append("<tr onmouseover='onColor(this)' onmouseout='offColor(this)'>\n");
                        strHtml.Append("<td><img src='images/office-1.jpg' /></td>\n");
                        strHtml.Append("<td>" + dt.Rows[i]["Subject"] + "</td>\n");
                        
                    }


                    strHtml.Append("<td>" + DateTime.Parse(dt.Rows[i]["SubmitTime"].ToString()).ToString("yyyy/MM/dd") + "</td>\n");

                    switch (dt.Rows[i]["Status"].ToString())
                    {
                        case "在线编辑": strHtml.Append(" <td colspan=4><a href = 'word2.aspx?ID=" + dt.Rows[i]["ID"] + "'><span style=' color:Blue;'>在线编辑</span></a>" +
                            " → <a href = 'word.aspx?ID=" + dt.Rows[i]["ID"] + "&user=张三'>张三批阅</a> " +
                            " → <a href = 'word.aspx?ID=" + dt.Rows[i]["ID"] + "&user=李四'>李四批阅</a> " +
                            " → <a href = 'word1.aspx?ID=" + dt.Rows[i]["ID"] + "'>文员清稿</a> " +
                            " → <a href = 'word3.aspx?ID=" + dt.Rows[i]["ID"] + "'>正式发文</a></td>\n");
                            break;
                        case "张三批阅": strHtml.Append(" <td colspan=4><a href = 'word2.aspx?ID=" + dt.Rows[i]["ID"] + "'><span style=' color:Green;'>在线编辑</span></a>" +
                            " → <a href = 'word.aspx?ID=" + dt.Rows[i]["ID"] + "&user=张三'><span style=' color:Blue;'>张三批阅</span></a>" +
                            " → <a href = 'word.aspx?ID=" + dt.Rows[i]["ID"] + "&user=李四'>李四批阅</a>" +
                            " → <a href = 'word1.aspx?ID=" + dt.Rows[i]["ID"] + "'>文员清稿</a>" +
                            " → <a href = 'word3.aspx?ID=" + dt.Rows[i]["ID"] + "'>正式发文</a></td>\n");
                            break;
                        case "李四批阅": strHtml.Append(" <td colspan=4><a href = 'word2.aspx?ID=" + dt.Rows[i]["ID"] + "'><span style=' color:Green;'>在线编辑</span></a>" +
                            " → <a href = 'word.aspx?ID=" + dt.Rows[i]["ID"] + "&user=张三'><span style=' color:Green;'>张三批阅</span></a>" +
                            " → <a href = 'word.aspx?ID=" + dt.Rows[i]["ID"] + "&user=李四'><span style=' color:Blue;'>李四批阅</span></a>" +
                            " → <a href = 'word1.aspx?ID=" + dt.Rows[i]["ID"] + "'>文员清稿</a>" +
                            " → <a href = 'word3.aspx?ID=" + dt.Rows[i]["ID"] + "'>正式发文</a></td>\n");
                            break;
                        case "文员清稿": strHtml.Append(" <td colspan=4><a href = 'word2.aspx?ID=" + dt.Rows[i]["ID"] + "'><span style=' color:Green;'>在线编辑</span></a>" +
                            " → <a href = 'word.aspx?ID=" + dt.Rows[i]["ID"] + "&user=张三'><span style=' color:Green;'>张三批阅</span></a>" +
                            " → <a href = 'word.aspx?ID=" + dt.Rows[i]["ID"] + "&user=李四'><span style=' color:Green;'>李四批阅</span></a>" +
                            " → <a href = 'word1.aspx?ID=" + dt.Rows[i]["ID"] + "'><span style=' color:Blue;'>文员清稿</span></a>" +
                            " → <a href = 'word3.aspx?ID=" + dt.Rows[i]["ID"] + "'>正式发文</a></td>\n");
                            break;
                        case "正式发文": strHtml.Append(" <td colspan=4><a href = 'word2.aspx?ID=" + dt.Rows[i]["ID"] + "'><span style=' color:Green;'>在线编辑</span></a>" +
                            " → <a href = 'word.aspx?ID=" + dt.Rows[i]["ID"] + "&user=张三'><span style=' color:Green;'>张三批阅</span></a>" +
                            " → <a href = 'word.aspx?ID=" + dt.Rows[i]["ID"] + "&user=李四'><span style=' color:Green;'>李四批阅</span></a>" +
                            " → <a href = 'word1.aspx?ID=" + dt.Rows[i]["ID"] + "'><span style=' color:Green;'>文员清稿</span></a>" +
                            " → <a href = 'word3.aspx?ID=" + dt.Rows[i]["ID"] + "'><span style=' color:Blue;'>正式发文</a></span></td>\n");
                            break;
                    }

                    if (dt.Rows[i]["HtmlFile"] != null && dt.Rows[i]["HtmlFile"].ToString() != "")
                        strHtml.Append(" <td><a href='doc/" + dt.Rows[i]["HtmlFile"].ToString() + "'><span style=' color:Green;'>Html</span></a></td>\n");
                    else
                        strHtml.Append(" <td>Html</td>\n");
                    strHtml.Append(" </tr>\n");

                }

                strHtmls = strHtml.ToString();
            }
            #endregion
        }
        else
        {
            #region 新建文件
            strSql = "select Max(ID) from word";
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand(strSql, conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader Reader = cmd.ExecuteReader();
            string newID = "1";
            if (Reader.Read())
            {
                newID = ((int)Reader[0] + 1).ToString();
            }
            Reader.Close();

            string fileName = "aabb" + newID + ".doc";
            string FileSubject = "请输入文档主题";
            if (Request.Form["FileSubject"] != "")
                FileSubject = Request.Form["FileSubject"];
            string strsql = "Insert into word(ID,FileName,Subject,SubmitTime,Status) values(" + newID
                + ",'" + fileName + "','" + FileSubject + "','" + DateTime.Now.ToString() + "','在线编辑')";
            cmd.CommandText = strsql;
            cmd.ExecuteNonQuery();
            conn.Close();

            System.IO.File.Copy(Server.MapPath("doc/" + Request.Form["TemplateName"]),
                Server.MapPath("doc/" + fileName), true);
            Response.Redirect("word2.aspx?ID=" + newID, true);
            #endregion
        }
    }
}
