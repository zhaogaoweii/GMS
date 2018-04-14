using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo.mdb";
            string sql = "select * from excel order by ID DESC";
            OleDbConnection conn = new OleDbConnection(connString);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader Reader = cmd.ExecuteReader();
            StringBuilder strGrid = new StringBuilder();
            if (!Reader.HasRows)
            {
                strGrid.Append("<tr >\r\n");
                strGrid.Append("<td colspan='5' width='100%'  align='center'>对不起，暂时没有可以操作的文档。\r\n");
                strGrid.Append("</td></tr>\r\n");
            }
            else
            {
                while (Reader.Read())
                {
                    
                    strGrid.Append("<tr  onmouseover='onColor(this)' onmouseout='offColor(this)' >\r\n");
                    strGrid.Append("<td><img src='images/office-2.jpg' /></td>\r\n");
                    strGrid.Append("<td>" + Reader["Subject"].ToString() + "</td>\r\n");
                    strGrid.Append("<td>" + DateTime.Parse(Reader["SubmitTime"].ToString()).ToShortDateString() + "</td>\r\n");
                    strGrid.Append("<td>" + " <a href='Excel.aspx?id=" + Reader["ID"].ToString() + "'>在线编辑</a> <a href='Excel2.aspx?id=" + Reader["ID"].ToString() + "'>只读打开</a>" + "</td>\r\n");
                    if (Reader["HtmlFile"] == DBNull.Value)
                        strGrid.Append("<td>Html</td>\r\n");
                    else
                        strGrid.Append("<td><a href=\"javascript:openHtml('" + Reader["HtmlFile"].ToString() + "');\">Html</a></td>\r\n");
                    
                    strGrid.Append("\r\n");
                }
            }
            Reader.Close();
            conn.Close();
            LiteralGrid.Text = strGrid.ToString();

        }
        else
        {
            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo.mdb";
            string sql = "select Max(ID) from excel";
            OleDbConnection conn = new OleDbConnection(connString);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader Reader = cmd.ExecuteReader();
            string newID = "1";
            if (Reader.Read())
            {
                newID = ((int)Reader[0] + 1).ToString();
            }
            Reader.Close();

            string fileName = "bbcc" + newID + ".xls";
            string FileSubject = "请输入文档主题";
            if (Request.Form["FileSubject"] != "")
                FileSubject = Request.Form["FileSubject"];
            string strsql = "Insert into excel(ID,FileName,Subject,SubmitTime) values(" + newID
                + ",'" + fileName + "','" + FileSubject + "','" + DateTime.Now.ToString() + "')";
            cmd.CommandText = strsql;
            cmd.ExecuteNonQuery();
            conn.Close();


            System.IO.File.Copy(Server.MapPath("doc/" + Request.Form["TemplateName"]),
                Server.MapPath("doc/" + fileName), true);
            Response.Redirect("Excel.aspx?ID=" + newID, true);
        }
    }
}
